using System;
using FairyGUI;
using UnityEngine;

namespace GameFrameX.Runtime
{
    internal class FUILoadAsyncResourceHelper : IAsyncResource
    {
        public async void LoadResource(string assetName, Action<bool, object> action)
        {
           var assetComponent= GameEntry.GetComponent<AssetComponent>();
            var textAsset = await assetComponent.LoadAssetAsync<TextAsset>(assetName);
            Log.Info(assetName);
            action.Invoke(textAsset != null && textAsset.AssetObject != null, textAsset?.GetAssetObject<TextAsset>());
        }

        public void ReleaseResource(object obj)
        {
        }
    }
}