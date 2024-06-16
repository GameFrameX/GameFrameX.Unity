using System.Collections.Generic;
using UnityEngine;
using GameFrameX.Runtime;


public static class HttpHelper
{
    private static readonly Dictionary<string, string> DictionaryParams = new Dictionary<string, string>();

    public static Dictionary<string, string> GetBaseParams()
    {
        DictionaryParams["Language"] = Application.systemLanguage.ToString();
        DictionaryParams["AppVersion"] = Application.version;
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
        DictionaryParams["Platform"] = PathHelper.GetPlatformName;
#endif
#else
        DictionaryParams["Platform"] = PathHelper.GetPlatformName;
        DictionaryParams["PackageName"] = Application.identifier;
        DictionaryParams["Channel"] = BlankGetChannel.GetChannelName();
        DictionaryParams["SubChannel"] = BlankGetChannel.GetChannelName();
#endif
        return DictionaryParams;
    }
}