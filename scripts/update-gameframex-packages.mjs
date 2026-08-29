#!/usr/bin/env node
// 查询 scoped registry 中 gameframex 包的最新版本，更新 Packages/manifest.json。
// 事实源：manifest.json 自身（dependencies + scopedRegistries），无硬编码包清单。
// 输出：变更列表（markdown），无变更输出 NO_CHANGES；任何包查询失败则退出码 1，不写回。

import { readFileSync, writeFileSync } from "node:fs";

const manifestPath = new URL("../Packages/manifest.json", import.meta.url);
const manifest = JSON.parse(readFileSync(manifestPath, "utf8"));

const registry = (manifest.scopedRegistries ?? []).find((r) => r.name === "GameFrameX")?.url;
if (!registry) {
  console.error("scoped registry \"GameFrameX\" not found in manifest.json");
  process.exit(1);
}

const packages = Object.keys(manifest.dependencies ?? {}).filter((name) => name.startsWith("com.gameframex."));

// semver 比较（容忍 v 前缀），返回 >0 / 0 / <0
const compare = (a, b) => {
  const pa = a.replace(/^v/, "").split(".").map(Number);
  const pb = b.replace(/^v/, "").split(".").map(Number);
  for (let i = 0; i < 3; i += 1) {
    const x = pa[i] ?? 0;
    const y = pb[i] ?? 0;
    if (x !== y) {
      return x - y;
    }
  }
  return 0;
};

const changes = [];
const errors = [];
for (const name of packages) {
  const current = manifest.dependencies[name];
  let latest;
  try {
    const res = await fetch(`${registry}/${name}`);
    if (!res.ok) {
      throw new Error(`HTTP ${res.status}`);
    }
    latest = (await res.json())["dist-tags"]?.latest;
  } catch (e) {
    errors.push(`${name}: ${e.message}`);
    continue;
  }
  if (latest && compare(latest, current) > 0) {
    manifest.dependencies[name] = latest;
    changes.push(`- ${name} ${current} → ${latest}`);
  }
}

if (errors.length > 0) {
  console.error("Registry query failures:\n" + errors.join("\n"));
  process.exit(1);
}

if (changes.length === 0) {
  console.log("NO_CHANGES");
  process.exit(0);
}

writeFileSync(manifestPath, JSON.stringify(manifest, null, 2) + "\n");
console.log(changes.join("\n"));
