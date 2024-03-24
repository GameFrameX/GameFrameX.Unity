using System;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 管理所有UI 包
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/FUIPackage")]
    public sealed class FUIPackageComponent : GameFrameworkComponent
    {
        private readonly Dictionary<string, FairyGUI.UIPackage> _uiPackages = new Dictionary<string, UIPackage>(32);

        public void AddPackage(string descFilePath)
        {
            if (!_uiPackages.TryGetValue(descFilePath, out var package))
            {
                if (descFilePath.IndexOf(Utility.Asset.Path.BundlesDirectoryName, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    package = UIPackage.AddPackage(descFilePath, LoadPackageInternal);
                }
                else
                {
                    package = UIPackage.AddPackage(descFilePath);
                }

                package.LoadAllAssets();
                _uiPackages.Add(descFilePath, package);
            }
        }

        public void RemovePackage(string descFilePath)
        {
            if (_uiPackages.TryGetValue(descFilePath, out var package))
            {
                UIPackage.RemovePackage(descFilePath);
                _uiPackages.Remove(descFilePath);
            }
        }

        public void RemoveAllPackages()
        {
            UIPackage.RemoveAllPackages();
            _uiPackages.Clear();
        }


        public bool Has(string uiPackageName)
        {
            return Get(uiPackageName) != null;
        }

        public UIPackage Get(string uiPackageName)
        {
            if (_uiPackages.TryGetValue(uiPackageName, out var package))
            {
                return package;
            }

            return null;
        }

        protected override void Awake()
        {
            base.Awake();
            UIPackage.SetAsyncLoadResource(new FUILoadAsyncResourceHelper());
            // UIPackage.LoadResource
        }


        public object LoadPackageInternal(string assetName, string extension, Type type, out DestroyMethod method)
        {
            method = DestroyMethod.Unload;
            string uiNamePath = $"{assetName}{extension}";
            switch (extension)
            {
                case Utility.Const.FileNameSuffix.Binary:
                {
                    var req = _assetComponent.LoadAssetSync<UnityEngine.TextAsset>(uiNamePath);
                    return req.AssetObject;
                }
                case Utility.Const.FileNameSuffix.PNG: //如果FGUI导出时没有选择分离通明通道，会因为加载不到!a结尾的Asset而报错，但是不影响运行
                {
                    if (assetName.IndexOf("!a", StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        return null;
                    }

                    var req = _assetComponent.LoadAssetSync<UnityEngine.Texture>(uiNamePath);
                    return req.AssetObject;
                }
                case Utility.Const.FileNameSuffix.Wav:
                {
                    var req = _assetComponent.LoadAssetSync<AudioClip>(uiNamePath);

                    return req.AssetObject;
                }
                default:
                {
                    var req = _assetComponent.LoadAssetSync(uiNamePath, type);
                    return req.AssetObject;
                }
            }
        }

        private AssetComponent _assetComponent;

        private void Start()
        {
            _assetComponent = GameEntry.GetComponent<AssetComponent>();
            if (_assetComponent == null)
            {
                Log.Fatal("Asset component is invalid.");
                return;
            }
        }
    }
}