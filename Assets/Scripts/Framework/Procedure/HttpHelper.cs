using System.Collections.Generic;
using UnityEngine;
using GameFrameX.Runtime;


public static class HttpHelper
{
    private static readonly Dictionary<string, object> DictionaryParams = new Dictionary<string, object>();

    public static Dictionary<string, object> GetBaseParams()
    {
        DictionaryParams["Language"] = Application.systemLanguage.ToString();
        DictionaryParams["UserLanguage"] = GameApp.Localization.Language;
        DictionaryParams["AppVersion"] = Application.version;
        // 设备ID用于判断白名单和其他识别用途
        DictionaryParams["DeviceUniqueIdentifier"] = SystemInfo.deviceUniqueIdentifier;
#if UNITY_WEBGL
        DictionaryParams["PackageName"] = "com.smartdog.bbqgame";
#if ENABLE_WECHAT_MINI_GAME
        DictionaryParams["Channel"] = "WxMiniGame";
        DictionaryParams["SubChannel"] = "WxMiniGame";
        DictionaryParams["Platform"] = "WebGLWxMiniGame";
#elif ENABLE_DOUYIN_MINI_GAME
        DictionaryParams["Channel"] = "DouYinMiniGame";
        DictionaryParams["SubChannel"] = "DouYinMiniGame";
        DictionaryParams["Platform"] = "WebGLDouYinMiniGame";
#else
        DictionaryParams["Channel"] = "WebGL";
        DictionaryParams["SubChannel"] = "WebGL";
        DictionaryParams["Platform"] = ApplicationHelper.PlatformName;
#endif
#else
        DictionaryParams["Platform"] = ApplicationHelper.PlatformName;
#if UNITY_STANDALONE_WIN
        DictionaryParams["PackageName"] = Application.productName;
#else
        DictionaryParams["PackageName"] = Application.identifier;
#endif
        DictionaryParams["Channel"] = BlankGetChannel.GetChannelName();
        DictionaryParams["SubChannel"] = BlankGetChannel.GetChannelName();
#endif
        return DictionaryParams;
    }
}