using GameFrameX.Runtime;
#if ENABLE_GAME_FRAME_X_HYBRID_CLR && !DISABLE_HYBRIDCLR
using System;
using System.Reflection;
using System.Linq;
using HybridCLR;
#endif

namespace Unity.Startup.Procedure
{
    public static class HotfixHelper
    {
#if (ENABLE_GAME_FRAME_X_HYBRID_CLR && !DISABLE_HYBRIDCLR)
        const string HotfixName = "Unity.Hotfix";
#endif
        public static
#if ENABLE_GAME_FRAME_X_HYBRID_CLR && !DISABLE_HYBRIDCLR
            async
#endif
            void StartHotfix()
        {
#if ENABLE_GAME_FRAME_X_HYBRID_CLR && !DISABLE_HYBRIDCLR
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
            Log.Info("开始加载程序集Hotfix");
            var hotfixAssembly = Assembly.Load(assemblyDataHotfixDll, null);
            Run(hotfixAssembly);

#else
            RunNative();
#endif
        }
#if ENABLE_GAME_FRAME_X_HYBRID_CLR && !DISABLE_HYBRIDCLR
        private static void Run(Assembly assembly)
        {
            Log.Info("加载程序集Hotfix 结束 Assembly " + assembly.FullName);
            var entryType = assembly.GetType("Hotfix.HotfixLauncher");
            Log.Info("加载程序集Hotfix 结束 EntryType " + entryType.FullName);
            var method = entryType.GetMethod("Main");
            Log.Info("加载程序集Hotfix 结束 EntryType=>method " + method?.Name);
            method?.Invoke(null, null);
        }
#else
        private static void RunNative()
        {
            var entryType = Utility.Assembly.GetType("Hotfix.HotfixLauncher");
            Log.Info("加载程序集Hotfix 结束 EntryType " + entryType.FullName);
            var method = entryType.GetMethod("Main");
            Log.Info("加载程序集Hotfix 结束 EntryType=>method " + method?.Name);
            method?.Invoke(null, null);
        }
#endif
    }
}