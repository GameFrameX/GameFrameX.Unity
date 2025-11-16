#if ENABLE_UI_FAIRYGUI
using System.IO;
using FairyGUI;
using GameFrameX.Runtime;
using YooAsset;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Startup.Procedure
{
    public sealed class FairyGuiExtensionLoader : GLoader
    {
        private static string _cachePath;

        private sealed class LoaderItem
        {
            public readonly string Key;
            public readonly NTexture Texture;
            public readonly AssetHandle AssetHandle;

            public LoaderItem(string key, NTexture texture, AssetHandle assetHandle = default)
            {
                Key = key;
                Texture = texture;
                AssetHandle = assetHandle;
            }
        }

        private readonly Dictionary<string, LoaderItem> _cache = new Dictionary<string, LoaderItem>();

        public FairyGuiExtensionLoader()
        {
            _cachePath = PathHelper.AppHotfixResPath + "/cache/images/";
            if (!Directory.Exists(_cachePath))
            {
                Directory.CreateDirectory(_cachePath);
            }
        }

        protected override void FreeExternal(NTexture nTexture)
        {
            foreach (var loaderItem in _cache)
            {
                if (loaderItem.Value.Texture != nTexture)
                {
                    continue;
                }

                loaderItem.Value.AssetHandle?.Release();
                break;
            }

            base.FreeExternal(nTexture);
        }

        protected override async void LoadExternal()
        {
            if (url.IsNullOrWhiteSpace())
            {
                onExternalLoadFailed();
                return;
            }

            NTexture tempTexture = null;
            if (url.StartsWithFast("http://") || url.StartsWithFast("https://"))
            {
                var hash = Utility.Hash.MD5.Hash(url);

                var path = $"{_cachePath}{hash}{Utility.Const.FileNameSuffix.PNG}";
                var isExists = FileHelper.IsExists(path);
                var texture2D = new Texture2D(1, 1, TextureFormat.RGBA32, false, false);
                if (isExists)
                {
                    var buffer = FileHelper.ReadAllBytes(path);
                    if (texture2D.LoadImage(buffer))
                    {
                        Log.Debug("加载成功" + url + "\n " + path);
                    }
                }
                else
                {
                    var webBufferResult = await GameApp.Download.Download(path, url);
                    if (webBufferResult)
                    {
                        var buffer = FileHelper.ReadAllBytes(path);
                        if (texture2D.LoadImage(buffer))
                        {
                            // Log.Debug("加载成功" + url + "\n " + path);
                        }
                    }
                }

                tempTexture = new NTexture(texture2D);
                _cache[url] = new LoaderItem(url, tempTexture);
            }
            else
            {
                var assetInfo = GameApp.Asset.GetAssetInfo(url);
                if (assetInfo != null)
                {
                    // 包内文件
                    var assetHandle = await GameApp.Asset.LoadAssetAsync<Texture2D>(url);
                    if (assetHandle.IsSucceed())
                    {
                        tempTexture = new NTexture(assetHandle.GetAssetObject<Texture2D>());
                        _cache[url] = new LoaderItem(url, tempTexture, assetHandle);
                        // Cache.Put(url, tempTexture);
                    }
                }
                else
                {
                    if (FileHelper.IsExists(url))
                    {
                        // 本地文件
                        var buffer = FileHelper.ReadAllBytes(url);
                        var texture2D = new Texture2D(1, 1, TextureFormat.ARGB32, false, false);
                        if (texture2D.LoadImage(buffer))
                        {
                            tempTexture = new NTexture(texture2D);
                            _cache[url] = new LoaderItem(url, tempTexture);
                        }
                    }
                }
            }

            if (tempTexture.IsNotNull())
            {
                onExternalLoadSuccess(tempTexture);
            }
            else
            {
                onExternalLoadFailed();
            }
        }
    }
}
#endif