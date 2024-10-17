using System;
using System.Linq;
using System.Reflection;
using GameFrameX.Runtime;
using HybridCLR;

namespace Unity.Startup.Procedure
{
    public static class HotfixHelper
    {
        const string HotfixName = "Unity.Hotfix";

        public static async void StartHotfix()
        {
            if (ApplicationHelper.IsEditor)
            {
                var assemblies = Utility.Assembly.GetAssemblies();
                foreach (var assembly in assemblies)
                {
                    if (assembly.GetName().Name.Equals(HotfixName, StringComparison.OrdinalIgnoreCase))
                    {
                        Run(assembly);
                        break;
                    }
                }

                return;
            }

            Log.Info("开始加载AOT DLL");

            var aotDlls = AOTGenericReferences.PatchedAOTAssemblyList.ToArray();
            foreach (var aotDll in aotDlls)
            {
                Log.Info("开始加载AOT DLL ==> " + aotDll);
                var assetHandle = await GameApp.Asset.LoadAssetAsync<UnityEngine.Object>(Utility.Asset.Path.GetAOTCodePath(aotDll));
                var aotBytes = assetHandle.GetAssetObject<UnityEngine.TextAsset>().bytes;
                RuntimeApi.LoadMetadataForAOTAssembly(aotBytes, HomologousImageMode.SuperSet);
            }

            Log.Info("结束加载AOT DLL");
            Log.Info("开始加载Unity.Hotfix.dll");
            var assetHotfixDllPath = Utility.Asset.Path.GetCodePath(HotfixName + Utility.Const.FileNameSuffix.DLL);
            var assetHotfixDllOperationHandle = await GameApp.Asset.LoadAssetAsync<UnityEngine.Object>(assetHotfixDllPath);
            var assemblyDataHotfixDll = assetHotfixDllOperationHandle.GetAssetObject<UnityEngine.TextAsset>().bytes;
            Log.Info("开始加载Unity.Hotfix.pdb");
            var assetHotfixPdbPath = Utility.Asset.Path.GetCodePath(HotfixName + Utility.Const.FileNameSuffix.PDB);
            var assetHotfixPdbOperationHandle = await GameApp.Asset.LoadAssetAsync<UnityEngine.Object>(assetHotfixPdbPath);
            var assemblyDataHotfixPdb = assetHotfixPdbOperationHandle.GetAssetObject<UnityEngine.TextAsset>().bytes;
            Log.Info("开始加载程序集Hotfix");
            var hotfixAssembly = Assembly.Load(assemblyDataHotfixDll, assemblyDataHotfixPdb);
            Run(hotfixAssembly);
        }

        private static void Run(Assembly assembly)
        {
            Log.Info("加载程序集Hotfix 结束 Assembly " + assembly.FullName);
            var entryType = assembly.GetType("Hotfix.HotfixLauncher");
            Log.Info("加载程序集Hotfix 结束 EntryType " + entryType.FullName);
            var method = entryType.GetMethod("Main");
            Log.Info("加载程序集Hotfix 结束 EntryType=>method " + method?.Name);
            method?.Invoke(null, null);
        }
    }
}