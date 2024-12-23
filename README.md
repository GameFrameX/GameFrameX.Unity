# 介绍：

GameFrameX for Unity是GameFrameX综合解决方案的重要组成部分，专为Unity客户端设计。它融合了众多实用的功能组件，形成了一个强大的模块库，旨在为游戏的前端开发、后端服务及管理界面提供一个一体化平台。这个解决方案促进了不同系统间的无缝协作，实现了高效的游戏开发和运维流程。

# 客户端

## `Unity` 集成功能

|        组件名称        |                介绍                |   来源   | 链接地址                                                                       |
|:------------------:|:--------------------------------:|:------:|:---------------------------------------------------------------------------|
|   GameFramework    |             客户端框架基础              | GitHub | https://github.com/gameframex/com.gameframex.unity                         |
|      YooAsset      |             定制的资源包管理             | GitHub | https://github.com/gameframex/com.gameframex.unity.tuyoogame.yooasset      |
|      UniTask       |         异步Await/Async的实现         | GitHub | https://github.com/gameframex/com.gameframex.unity.cysharp.unitask         |
|    FairyGUI UI     | FairyGUI编辑器。SDK已支持微信和抖音小游戏和WEBGL | GitHub | https://github.com/gameframex/com.gameframex.unity.fairygui.unity          |
|      ProtoBuf      |            数据序列化和通讯协议            | GitHub | https://github.com/gameframex/com.gameframex.unity.google.protobuf         |
|     HybridCLR      |               热更新                | GitHub | https://github.com/focus-creative-games/hybridclr                          |
|       Sentry       |            错误追踪和性能监控             | GitHub | https://github.com/gameframex/com.gameframex.unity.sentry.unity            |
|      LitJson       |        JSON序列化工具（马三修改版本）         | GitHub | https://github.com/gameframex/com.gameframex.unity.xincger.litjson         |
|     logViewer      |              日志查看器               | GitHub | https://github.com/gameframex/com.gameframex.unity.sharelib.logviewer      |
|      DoTween       |             强大的动画插件              | GitHub | https://github.com/gameframex/com.gameframex.unity.demigiant.dotween       |
|   Advertisement    |           广告播放组件（激励广告）           | GitHub | https://github.com/gameframex/com.gameframex.unity.advertisement           |
|   ObjectStorage    |          对象存储上传(打包后上传)           | GitHub | https://github.com/gameframex/com.gameframex.unity.objectstorage           |
| OperationClipBoard |          实现剪贴板数据的设置与获取           | GitHub | https://github.com/gameframex/com.gameframex.unity.blankoperationclipboard |
|     GetChannel     |         渠道获取及集成基础的渠道获取方式         | GitHub | https://github.com/gameframex/com.gameframex.unity.getchannel              |
|     ReadAssets     |       直接读取Android只读目录下的文件        | GitHub | https://github.com/gameframex/com.gameframex.unity.readassets              |
|     WebSocket      |         WebSocket 网络支持库          | GitHub | https://github.com/gameframex/com.gameframex.unity.psygames.unitywebsocket |
|   FindReference2   |           强大的资源引用查找插件            | GitHub | https://github.com/gameframex/com.vietlabs.fr2                             |
|   GameAnalytics    |            游戏数据分析和统计             | GitHub | https://github.com/gameframex/com.gameframex.unity.gameanalytics.git       |
|     Animancer      |           高度灵活的动画状态机插件           | GitHub | 还没上传                                                                       |

# 子库列表（按需获取）默认已全部带上。可以删除自己不需要的。由于默认大部分用户是`中国大陆`用户。故项目中`默认`为`镜像地址`

如果Github 下载有问题。可以更换`github.com` 为 `www.gitlink.org.cn` 镜像地址。注意镜像地址同步可能滞后几天。取决于镜像站的同步策略

例如："com.gameframex.unity.mono": "https://`github.com`/AlianBlank/com.gameframex.unity.mono.git"

更换为 "com.gameframex.unity.mono": "https://`www.gitlink.org.cn`/AlianBlank/com.gameframex.unity.mono.git"

```json
{
  "com.gameframex.unity": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.git",
  "com.gameframex.unity.asset": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.asset.git",
  "com.gameframex.unity.blankoperationclipboard": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.blankoperationclipboard.git",
  "com.gameframex.unity.config": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.config.git",
  "com.gameframex.unity.coroutine": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.coroutine.git",
  "com.gameframex.unity.cysharp.unitask": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.cysharp.unitask.git",
  "com.gameframex.unity.demigiant.dotween": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.demigiant.dotween.git",
  "com.gameframex.unity.download": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.download.git",
  "com.gameframex.unity.entity": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.entity.git",
  "com.gameframex.unity.entry": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.entry.git",
  "com.gameframex.unity.event": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.event.git",
  "com.gameframex.unity.fairygui": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.fairygui.git",
  "com.gameframex.unity.fairygui.unity": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.fairygui.unity.git",
  "com.gameframex.unity.focus-creative-games.luban": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.focus-creative-games.luban.git",
  "com.gameframex.unity.fsm": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.fsm.git",
  "com.gameframex.unity.getchannel": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.getchannel.git",
  "com.gameframex.unity.globalconfig": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.globalconfig.git",
  "com.gameframex.unity.google.protobuf": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.google.protobuf.git",
  "com.gameframex.unity.gwiazdorrr.betterstreamingassets": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.gwiazdorrr.betterstreamingassets.git",
  "com.gameframex.unity.json.simplejson": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.json.simplejson.git",
  "com.gameframex.unity.localization": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.localization.git",
  "com.gameframex.unity.mono": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.mono.git",
  "com.gameframex.unity.network": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.network.git",
  "com.gameframex.unity.procedure": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.procedure.git",
  "com.gameframex.unity.protobuff2cs": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.protobuff2cs.git",
  "com.gameframex.unity.psygames.unitywebsocket": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.psygames.unitywebsocket.git",
  "com.gameframex.unity.readassets": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.readassets.git",
  "com.gameframex.unity.scene": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.scene.git",
  "com.gameframex.unity.setting": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.setting.git",
  "com.gameframex.unity.sound": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.sound.git",
  "com.gameframex.unity.timer": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.timer.git",
  "com.gameframex.unity.web": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.web.git",
  "com.gameframex.unity.tuyoogame.yooasset": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.tuyoogame.yooasset.git",
  "com.gameframex.unity.xincger.litjson": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.xincger.litjson.git",
  "com.gameframex.unity.yasirkula.debugconsole": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.yasirkula.debugconsole.git",
  "com.gameframex.unity.weixin.minigame": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.weixin.minigame.git",
  "com.gameframex.unity.advertisement": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.advertisement.git",
  "com.gameframex.unity.advertisement.wechatminigame": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.advertisement.wechatminigame.git",
  "com.gameframex.unity.objectstorage": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.objectstorage.git",
  "com.gameframex.unity.objectstorage.qiniu": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.objectstorage.qiniu.git",
  "com.gameframex.unity.objectstorage.aliyun": "https://www.gitlink.org.cn/AlianBlank/com.gameframex.unity.objectstorage.aliyun.git",
  "com.gameframex.unity.gameanalytics": "https://github.com/AlianBlank/com.gameframex.unity.gameanalytics.git",
  "com.gameframex.unity.gameanalytics.gameanalytics": "https://github.com/AlianBlank/com.gameframex.unity.gameanalytics.gameanalytics.git",
  "com.gameanalytics.sdk": "https://github.com/AlianBlank/com.gameframex.unity.gameanalytics.gameanalytics.sdk.git",
  "com.gameframex.unity.gameanalytics.gravity-engine": "https://github.com/AlianBlank/com.gameframex.unity.gameanalytics.gravity-engine.git",
  "com.gameframex.unity.gravityinfinite.gravity-engine": "https://github.com/AlianBlank/com.gameframex.unity.gravityinfinite.gravity-engine.git"
}
```

# 交流方式(建议。需求。BUG)

QQ群：467608841

# Doc (已经在写了,别催了!-_-!)

`所有站点内容一致，不存在内容不一致的情况`

文档地址 : https://gameframex.doc.alianblank.com

备用文档地址 : https://gameframex-docs.pages.dev

备用文档地址 : https://gameframex.doc.cloudflare.alianblank.com

备用文档地址 : https://gameframex.doc.vercel.alianblank.com

# 免责声明

所有插件均来自互联网.请各位使用时自行付费.如果以上插件涉及侵权.请发email或提交issue.本人将移除.谢谢

# LICENSE

Apache License 2
