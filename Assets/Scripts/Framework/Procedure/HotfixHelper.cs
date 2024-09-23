using System.Linq;
using System.Reflection;
using GameFrameX.Runtime;
using HybridCLR;

namespace Unity.Startup.Procedure
{
    public static class HotfixHelper
    {
        public static async void StartHotfix()
        {
            Log.Info("开始加载AOT DLL");
            if (!ApplicationHelper.IsEditor)
            {
                var aotDlls = AOTGenericReferences.PatchedAOTAssemblyList.ToArray();
                foreach (var aotDll in aotDlls)
                {
                    Log.Info("开始加载AOT DLL ==> " + aotDll);
                    var assetHandle = await GameApp.Asset.LoadAssetAsync<UnityEngine.Object>(Utility.Asset.Path.GetAOTCodePath(aotDll));
                    var aotBytes = assetHandle.GetAssetObject<UnityEngine.TextAsset>().bytes;
                    RuntimeApi.LoadMetadataForAOTAssembly(aotBytes, HomologousImageMode.SuperSet);
                }
            }

            Log.Info("结束加载AOT DLL");
            Log.Info("开始加载Unity.Hotfix.dll");
            var assetHotfixDllPath = Utility.Asset.Path.GetCodePath("Unity.Hotfix.dll");
            var assetHotfixDllOperationHandle = await GameApp.Asset.LoadAssetAsync<UnityEngine.Object>(assetHotfixDllPath);
            var assemblyDataHotfixDll = assetHotfixDllOperationHandle.GetAssetObject<UnityEngine.TextAsset>().bytes;
            Log.Info("开始加载Unity.Hotfix.pdb");
            var assetHotfixPdbPath = Utility.Asset.Path.GetCodePath("Unity.Hotfix.pdb");
            var assetHotfixPdbOperationHandle = await GameApp.Asset.LoadAssetAsync<UnityEngine.Object>(assetHotfixPdbPath);
            var assemblyDataHotfixPdb = assetHotfixPdbOperationHandle.GetAssetObject<UnityEngine.TextAsset>().bytes;
            Log.Info("开始加载程序集Hotfix");
            var ass = Assembly.Load(assemblyDataHotfixDll, assemblyDataHotfixPdb);

            Log.Info("加载程序集Hotfix 结束 Assembly " + ass.FullName);
            var entryType = ass.GetType("Hotfix.HotfixLauncher");
            Log.Info("加载程序集Hotfix 结束 EntryType " + entryType.FullName);
            var method = entryType.GetMethod("Main");
            Log.Info("加载程序集Hotfix 结束 EntryType=>method " + method?.Name);
            method?.Invoke(null, null);
        }
    }
}