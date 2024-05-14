# 介绍：

GameFrameX for Unity是GameFrameX综合解决方案的重要组成部分，专为Unity客户端设计。它融合了众多实用的功能组件，形成了一个强大的模块库，旨在为游戏的前端开发、后端服务及管理界面提供一个一体化平台。这个解决方案促进了不同系统间的无缝协作，实现了高效的游戏开发和运维流程。

> 正在开发中...

# `注意`!!! `注意` !!! `注意` !!!

开发期,随时可能变化结构。

# 客户端

## `Unity` 集成功能

|        组件名称        |         介绍          |   来源   | 链接地址                                                                 |
|:------------------:|:-------------------:|:------:|:---------------------------------------------------------------------|
|   GameFramework    |        客户端框架        | GitHub | https://github.com/AlianBlank/GameFrameX                             |
|      YooAsset      |      定制的资源包管理       | GitHub | https://github.com/AlianBlank/com.tuyoogame.yooasset                 |
|      UniTask       |  异步Await/Async的实现   | GitHub | https://github.com/AlianBlank/com.cysharp.unitask                    |
|    FairyGUI UI     |         编辑器         | GitHub | https://github.com/AlianBlank/com.fairygui.unity                     |
|      ProtoBuf      |     数据序列化和通讯协议      | GitHub | https://github.com/AlianBlank/com.google.protobuf                    |
|    MessagePack     |     高效的二进制序列化库      | GitHub | https://github.com/AlianBlank/com.neuecc.messagepack                 |
|     HybridCLR      |         热更新         | GitHub | https://github.com/focus-creative-games/hybridclr                    |
|        XLua        |         热更新         | GitHub | https://github.com/AlianBlank/com.tencent.xlua                       |
|   GameAnalytics    |      游戏数据分析和统计      | GitHub | 还没上传                                                                 |
|       Sentry       |      错误追踪和性能监控      | GitHub | https://github.com/AlianBlank/io.sentry.unity                        |
|      LitJson       |  JSON序列化工具（马三修改版本）  | GitHub | https://github.com/AlianBlank/com.xincger.litjson                    |
|     logViewer      |        日志查看器        | GitHub | https://github.com/AlianBlank/com.sharelib.logviewer                 |
|      DoTween       |       强大的动画插件       | GitHub | https://github.com/AlianBlank/com.demigiant.dotween                  |
|     Animancer      |    高度灵活的动画状态机插件     | GitHub | 还没上传                                                                 |
|      BestHTTP      |     全面的HTTP协议实现     | GitHub | https://github.com/AlianBlank/com.benedicht.besthttp                 |
| OperationClipBoard |    实现剪贴板数据的设置与获取    | GitHub | https://github.com/AlianBlank/com.alianblank.blankoperationclipboard |
|     GetChannel     |  渠道获取及集成基础的渠道获取方式   | GitHub | https://github.com/AlianBlank/com.alianblank.blankgetchannel         |
|     ReadAssets     | 直接读取Android只读目录下的文件 | GitHub | https://github.com/AlianBlank/com.alianblank.readassets              |
|   FindReference2   |     强大的资源引用查找插件     | GitHub | https://github.com/AlianBlank/com.vietlabs.fr2                       |

# 子库列表（按需获取）默认已全部带上。可以删除自己不需要的。由于默认大部分用户是`中国大陆`用户。故项目中`默认`为`镜像地址`

如果Github 下载有问题。可以更换`github.com` 为 `www.gitlink.org.cn` 镜像地址。注意镜像地址同步可能滞后几天。取决于镜像站的同步策略

例如："com.alianblank.gameframex.unity.mono": "https://`github.com`/AlianBlank/com.alianblank.gameframex.unity.mono.git"

更换为 "com.alianblank.gameframex.unity.mono": "https://`www.gitlink.org.cn`/AlianBlank/com.alianblank.gameframex.unity.mono.git"

```json
{
  "com.alianblank.gameframex.unity.focus-creative-games.luban": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.focus-creative-games.luban.git",
  "com.alianblank.gameframex.unity.getchannel": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.getchannel.git",
  "com.alianblank.gameframex.unity.blankoperationclipboard": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.blankoperationclipboard.git",
  "com.alianblank.gameframex.unity.readassets": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.readassets.git",
  "com.alianblank.gameframex.unity.psygames.unitywebsocket": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.psygames.unitywebsocket.git",
  "com.alianblank.gameframex.unity.fairygui.unity": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.fairygui.unity.git",
  "com.alianblank.gameframex.unity.fairygui": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.fairygui.git",
  "com.alianblank.gameframex.unity.demigiant.dotween": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.demigiant.dotween.git",
  "com.alianblank.gameframex.unity.cysharp.unitask": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.cysharp.unitask.git",
  "com.alianblank.gameframex.unity.gwiazdorrr.betterstreamingassets": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.gwiazdorrr.betterstreamingassets.git",
  "com.alianblank.gameframex.unity.tuyoogame.yooasset": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.tuyoogame.yooasset.git",
  "com.alianblank.gameframex.unity.json.simplejson": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.json.simplejson.git",
  "com.alianblank.gameframex.unity.xincger.litjson": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.xincger.litjson.git",
  "com.alianblank.gameframex.unity.google.protobuf": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.google.protobuf.git",
  "com.alianblank.gameframex.unity.protobuff2cs": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.protobuff2cs.git",
  "com.alianblank.gameframex.unity.web": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.web.git",
  "com.alianblank.gameframex.unity.entry": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.entry.git",
  "com.alianblank.gameframex.unity.event": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.event.git",
  "com.alianblank.gameframex.unity.globalconfig": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.globalconfig.git",
  "com.alianblank.gameframex.unity.fsm": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.fsm.git",
  "com.alianblank.gameframex.unity.procedure": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.procedure.git",
  "com.alianblank.gameframex.unity.entity": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.entity.git",
  "com.alianblank.gameframex.unity.asset": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.asset.git",
  "com.alianblank.gameframex.unity.localization": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.localization.git",
  "com.alianblank.gameframex.unity.config": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.config.git",
  "com.alianblank.gameframex.unity.timer": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.timer.git",
  "com.alianblank.gameframex.unity.sound": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.sound.git",
  "com.alianblank.gameframex.unity.scene": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.scene.git",
  "com.alianblank.gameframex.unity.download": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.download.git",
  "com.alianblank.gameframex.unity.coroutine": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.coroutine.git",
  "com.alianblank.gameframex.unity.setting": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.setting.git",
  "com.alianblank.gameframex.unity.mono": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.mono.git",
  "com.alianblank.gameframex.unity.gameanalytics": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.gameanalytics.git",
  "com.alianblank.gameframex.unity.yasirkula.debugconsole": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.yasirkula.debugconsole.git",
  "com.alianblank.gameframex.unity.esotericsoftware.spine.spine-unity": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.esotericsoftware.spine.spine-unity.git",
  "com.alianblank.gameframex.unity.network": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.network.git"
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
