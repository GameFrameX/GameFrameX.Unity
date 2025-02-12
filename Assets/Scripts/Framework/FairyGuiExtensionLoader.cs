using System.IO;
using FairyGUI;
using GameFrameX.Runtime;
using UnityEngine;

namespace Unity.Startup
{
    using System.Collections.Generic;
    using UnityEngine;

    public class LRUCache
    {
        private class CacheItem
        {
            public string key;
            public NTexture texture;

            public CacheItem(string key, NTexture texture)
            {
                this.key = key;
                this.texture = texture;
            }
        }

        private readonly int maxSize;
        private Dictionary<string, CacheItem> cache;
        private LinkedList<CacheItem> lruList;

        public LRUCache(int maxSize)
        {
            this.maxSize = maxSize;
            cache = new Dictionary<string, CacheItem>();
            lruList = new LinkedList<CacheItem>();
        }

        public NTexture Get(string key)
        {
            if (cache.TryGetValue(key, out CacheItem item))
            {
                // 移动到最近使用的位置
                lruList.Remove(item);
                lruList.AddFirst(item);
                return item.texture;
            }

            return null; // 纹理未找到
        }

        public void Put(string key, NTexture texture)
        {
            if (key == null)
            {
                return;
            }

            if (cache.ContainsKey(key))
            {
                // 更新已有项并移动到最近使用位置
                CacheItem existingItem = cache[key];
                lruList.Remove(existingItem);
                existingItem.texture = texture; // 更新纹理
                lruList.AddFirst(existingItem);
            }
            else
            {
                // 如果超过最大数量，则移除最少使用的项
                if (cache.Count >= maxSize)
                {
                    RemoveLeastRecentlyUsed();
                }

                // 添加新项
                CacheItem newItem = new CacheItem(key, texture);
                cache[key] = newItem;
                lruList.AddFirst(newItem);
            }
        }

        private void RemoveLeastRecentlyUsed()
        {
            if (lruList.Count > 0)
            {
                CacheItem leastUsedItem = lruList.Last.Value;
                lruList.RemoveLast();
                cache.Remove(leastUsedItem.key);
                leastUsedItem.texture.Dispose();
                if (leastUsedItem.texture.nativeTexture.IsNotNull())
                {
                    Object.Destroy(leastUsedItem.texture.nativeTexture); // 释放纹理资源
                }

                if (leastUsedItem.texture.alphaTexture.IsNotNull())
                {
                    Object.Destroy(leastUsedItem.texture.alphaTexture); // 释放纹理资源
                }
            }
        }

        public void Clear()
        {
            foreach (var item in lruList)
            {
                item.texture.Dispose();
                if (item.texture.nativeTexture.IsNotNull())
                {
                    Object.Destroy(item.texture.nativeTexture); // 释放纹理资源
                }

                if (item.texture.alphaTexture.IsNotNull())
                {
                    Object.Destroy(item.texture.alphaTexture); // 释放纹理资源
                }
            }

            cache.Clear();
            lruList.Clear();
        }
    }


    public sealed class FairyGuiExtensionLoader : GLoader
    {
        private static LRUCache cache = new LRUCache(100);
        private static string _cachePath;

        public FairyGuiExtensionLoader()
        {
            _cachePath = PathHelper.AppHotfixResPath + "/cache/images/";
            if (!Directory.Exists(_cachePath))
            {
                Directory.CreateDirectory(_cachePath);
            }
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
                // 网络资源
                var nTexture = cache.Get(url);
                if (nTexture.IsNull())
                {
                    var hash = Utility.Hash.MD5.Hash(url);

                    var path = $"{_cachePath}{hash}.png";
                    var isExists = FileHelper.IsExists(path);
                    var texture2D = Texture2D.whiteTexture;
                    if (isExists)
                    {
                        var buffer = FileHelper.ReadAllBytes(path);
                        texture2D.LoadImage(buffer);
                    }
                    else
                    {
                        var webBufferResult = await GameApp.Web.GetToBytes(url, null);
                        FileHelper.WriteAllBytes(path, webBufferResult.Result);
                        texture2D.LoadImage(webBufferResult.Result);
                    }

                    tempTexture = new NTexture(texture2D);
                    cache.Put(url, tempTexture);
                }
                else
                {
                    tempTexture = nTexture;
                }
            }
            else if (url.StartsWithFast("ui://"))
            {
                // 包内资源
                LoadContent();
            }
            else
            {
                var nTexture = cache.Get(url);
                if (nTexture.IsNotNull())
                {
                    tempTexture = nTexture;
                }
                else
                {
                    var assetInfo = GameApp.Asset.GetAssetInfo(url);
                    if (assetInfo.IsInvalid == false)
                    {
                        var assetHandle = await GameApp.Asset.LoadAssetAsync<Texture2D>(url);
                        if (assetHandle.IsSucceed)
                        {
                            tempTexture = new NTexture(assetHandle.GetAssetObject<Texture2D>());
                            cache.Put(url, tempTexture);
                        }
                    }
                    else
                    {
                        if (FileHelper.IsExists(url))
                        {
                            var buffer = FileHelper.ReadAllBytes(url);

                            var texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false, false);
                            texture2D.LoadImage(buffer);
                            tempTexture = new NTexture(texture2D);
                            cache.Put(url, tempTexture);
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