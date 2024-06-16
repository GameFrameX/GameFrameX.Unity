# 介绍：

GameFrameX for Unity是GameFrameX综合解决方案的重要组成部分，专为Unity客户端设计。它融合了众多实用的功能组件，形成了一个强大的模块库，旨在为游戏的前端开发、后端服务及管理界面提供一个一体化平台。这个解决方案促进了不同系统间的无缝协作，实现了高效的游戏开发和运维流程。

> 正在开发中...

# `注意`!!! `注意` !!! `注意` !!!

开发期,随时可能变化结构。

# 客户端

## `Unity` 集成功能

|        组件名称        |                介绍                |   来源   | 链接地址                                                                                  |
|:------------------:|:--------------------------------:|:------:|:--------------------------------------------------------------------------------------|
|   GameFramework    |             客户端框架基础              | GitHub | https://github.com/AlianBlank/com.alianblank.gameframex.unity                         |
|      YooAsset      |             定制的资源包管理             | GitHub | https://github.com/AlianBlank/com.alianblank.gameframex.unity.tuyoogame.yooasset      |
|      UniTask       |         异步Await/Async的实现         | GitHub | https://github.com/AlianBlank/com.alianblank.gameframex.unity.cysharp.unitask         |
|    FairyGUI UI     | FairyGUI编辑器。SDK已支持微信和抖音小游戏和WEBGL | GitHub | https://github.com/AlianBlank/com.alianblank.gameframex.unity.fairygui.unity          |
|      ProtoBuf      |            数据序列化和通讯协议            | GitHub | https://github.com/AlianBlank/com.alianblank.gameframex.unity.google.protobuf         |
|     HybridCLR      |               热更新                | GitHub | https://github.com/focus-creative-games/hybridclr                                     |
|       Sentry       |            错误追踪和性能监控             | GitHub | https://github.com/AlianBlank/com.alianblank.gameframex.unity.sentry.unity            |
|      LitJson       |        JSON序列化工具（马三修改版本）         | GitHub | https://github.com/AlianBlank/com.alianblank.gameframex.unity.xincger.litjson         |
|     logViewer      |              日志查看器               | GitHub | https://github.com/AlianBlank/com.alianblank.gameframex.unity.sharelib.logviewer      |
|      DoTween       |             强大的动画插件              | GitHub | https://github.com/AlianBlank/com.alianblank.gameframex.unity.demigiant.dotween       |
|   Advertisement    |           广告播放组件（激励广告）           | GitHub | https://github.com/AlianBlank/com.alianblank.gameframex.unity.advertisement           |
|   ObjectStorage    |          对象存储上传(打包后上传)           | GitHub | https://github.com/AlianBlank/com.alianblank.gameframex.unity.objectstorage           |
| OperationClipBoard |          实现剪贴板数据的设置与获取           | GitHub | https://github.com/AlianBlank/com.alianblank.gameframex.unity.blankoperationclipboard |
|     GetChannel     |         渠道获取及集成基础的渠道获取方式         | GitHub | https://github.com/AlianBlank/com.alianblank.gameframex.unity.getchannel              |
|     ReadAssets     |       直接读取Android只读目录下的文件        | GitHub | https://github.com/AlianBlank/com.alianblank.gameframex.unity.readassets              |
|     WebSocket      |         WebSocket 网络支持库          | GitHub | https://github.com/AlianBlank/com.alianblank.gameframex.unity.psygames.unitywebsocket |
|   FindReference2   |           强大的资源引用查找插件            | GitHub | https://github.com/AlianBlank/com.vietlabs.fr2                                        |
|   GameAnalytics    |            游戏数据分析和统计             | GitHub | 还没上传                                                                                  |
|     Animancer      |           高度灵活的动画状态机插件           | GitHub | 还没上传                                                                                  |

# 子库列表（按需获取）默认已全部带上。可以删除自己不需要的。由于默认大部分用户是`中国大陆`用户。故项目中`默认`为`镜像地址`

如果Github 下载有问题。可以更换`github.com` 为 `www.gitlink.org.cn` 镜像地址。注意镜像地址同步可能滞后几天。取决于镜像站的同步策略

例如："com.alianblank.gameframex.unity.mono": "https://`github.com`/AlianBlank/com.alianblank.gameframex.unity.mono.git"

更换为 "com.alianblank.gameframex.unity.mono": "https://`www.gitlink.org.cn`/AlianBlank/com.alianblank.gameframex.unity.mono.git"

```json
{
    "com.alianblank.gameframex.unity": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.git",
    "com.alianblank.gameframex.unity.asset": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.asset.git",
    "com.alianblank.gameframex.unity.blankoperationclipboard": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.blankoperationclipboard.git",
    "com.alianblank.gameframex.unity.config": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.config.git",
    "com.alianblank.gameframex.unity.coroutine": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.coroutine.git",
    "com.alianblank.gameframex.unity.cysharp.unitask": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.cysharp.unitask.git",
    "com.alianblank.gameframex.unity.demigiant.dotween": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.demigiant.dotween.git",
    "com.alianblank.gameframex.unity.download": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.download.git",
    "com.alianblank.gameframex.unity.entity": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.entity.git",
    "com.alianblank.gameframex.unity.entry": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.entry.git",
    "com.alianblank.gameframex.unity.event": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.event.git",
    "com.alianblank.gameframex.unity.fairygui": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.fairygui.git",
    "com.alianblank.gameframex.unity.fairygui.unity": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.fairygui.unity.git",
    "com.alianblank.gameframex.unity.focus-creative-games.luban": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.focus-creative-games.luban.git",
    "com.alianblank.gameframex.unity.fsm": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.fsm.git",
    "com.alianblank.gameframex.unity.gameanalytics": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.gameanalytics.git",
    "com.alianblank.gameframex.unity.getchannel": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.getchannel.git",
    "com.alianblank.gameframex.unity.globalconfig": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.globalconfig.git",
    "com.alianblank.gameframex.unity.google.protobuf": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.google.protobuf.git",
    "com.alianblank.gameframex.unity.gwiazdorrr.betterstreamingassets": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.gwiazdorrr.betterstreamingassets.git",
    "com.alianblank.gameframex.unity.json.simplejson": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.json.simplejson.git",
    "com.alianblank.gameframex.unity.localization": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.localization.git",
    "com.alianblank.gameframex.unity.mono": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.mono.git",
    "com.alianblank.gameframex.unity.network": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.network.git",
    "com.alianblank.gameframex.unity.procedure": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.procedure.git",
    "com.alianblank.gameframex.unity.protobuff2cs": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.protobuff2cs.git",
    "com.alianblank.gameframex.unity.psygames.unitywebsocket": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.psygames.unitywebsocket.git",
    "com.alianblank.gameframex.unity.readassets": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.readassets.git",
    "com.alianblank.gameframex.unity.scene": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.scene.git",
    "com.alianblank.gameframex.unity.setting": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.setting.git",
    "com.alianblank.gameframex.unity.sound": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.sound.git",
    "com.alianblank.gameframex.unity.timer": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.timer.git",
    "com.alianblank.gameframex.unity.web": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.web.git",
    "com.alianblank.gameframex.unity.tuyoogame.yooasset": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.tuyoogame.yooasset.git",
    "com.alianblank.gameframex.unity.xincger.litjson": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.xincger.litjson.git",
    "com.alianblank.gameframex.unity.yasirkula.debugconsole": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.yasirkula.debugconsole.git",
    "com.alianblank.gameframex.unity.weixin.minigame": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.weixin.minigame.git",
    "com.alianblank.gameframex.unity.advertisement": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.advertisement.git",
    "com.alianblank.gameframex.unity.advertisement.wechatminigame": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.advertisement.wechatminigame.git",
    "com.alianblank.gameframex.unity.objectstorage": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.objectstorage.git",
    "com.alianblank.gameframex.unity.objectstorage.qiniu": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.objectstorage.qiniu.git",
    "com.alianblank.gameframex.unity.objectstorage.aliyun": "https://www.gitlink.org.cn/AlianBlank/com.alianblank.gameframex.unity.objectstorage.aliyun.git",
}
```

# 交流方式(建议。需求。BUG)

QQ群：467608841

# Doc

文档地址 : https://www.yuque.com/alianblank/gameframex

# 免责声明

所有插件均来自互联网.请各位使用时自行付费.如果以上插件涉及侵权.请发email.本人将移除.谢谢

# LICENSE

Apache License 2
