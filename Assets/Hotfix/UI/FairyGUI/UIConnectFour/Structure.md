# UIConnectFour 包结构

> 由 gencode 插件自动生成（发布时更新），请勿手改。
> 标注说明：`Wrapper·FGUI类型·mode`；`→ varName` 为生成的 C# 属性名（m_ 前缀）；`[无绑定]` 表示该节点未生成 C# 属性（无名称或 n+数字 自动名）；`[pkg=X]` 跨包引用；`← 资源名` 图片资源；`"文本"` 文案；`(↺)` 循环引用已省略。

## UIConnectFourButton (GButton, 160x54)

```text
├─ bg (GGraph, 160x54) → m_bg
└─ title (GTextField, 160x54) [无绑定] "按钮"
```

## UIConnectFourBattlePanel (UIConnectFourBattlePanel·GComponent, 1080x2160) [exported]

```text
├─ mask (GGraph, 1080x2160) [无绑定]
├─ window_bg (GGraph, 960x1060) [无绑定]
├─ top_bg (GGraph, 880x110) [无绑定]
├─ room_title_text (GTextField, 560x42) [无绑定] "房间 #0"
├─ room_state_text (GTextField, 620x30) [无绑定] "等待中 0/2  局数:1 …"
├─ turn_text (GTextField, 816x38) [无绑定] "等待开始"
├─ leave_button (GButton, 96x52) [无绑定]
│  ├─ bg (GGraph, 160x54)
│  └─ title (GTextField, 160x54) "按钮"
├─ close_button (GButton, 96x52) [无绑定]
│  ├─ bg (GGraph, 160x54)
│  └─ title (GTextField, 160x54) "按钮"
├─ result_text (GTextField, 620x48) [无绑定] "等待开始"
├─ board_bg (GGraph, 620x548) [无绑定]
├─ cell_0 (GGraph, 76x76) [无绑定]
├─ cell_1 (GGraph, 76x76) [无绑定]
├─ cell_2 (GGraph, 76x76) [无绑定]
├─ cell_3 (GGraph, 76x76) [无绑定]
├─ cell_4 (GGraph, 76x76) [无绑定]
├─ cell_5 (GGraph, 76x76) [无绑定]
├─ cell_6 (GGraph, 76x76) [无绑定]
├─ cell_7 (GGraph, 76x76) [无绑定]
├─ cell_8 (GGraph, 76x76) [无绑定]
├─ cell_9 (GGraph, 76x76) [无绑定]
├─ cell_10 (GGraph, 76x76) [无绑定]
├─ cell_11 (GGraph, 76x76) [无绑定]
├─ cell_12 (GGraph, 76x76) [无绑定]
├─ cell_13 (GGraph, 76x76) [无绑定]
├─ cell_14 (GGraph, 76x76) [无绑定]
├─ cell_15 (GGraph, 76x76) [无绑定]
├─ cell_16 (GGraph, 76x76) [无绑定]
├─ cell_17 (GGraph, 76x76) [无绑定]
├─ cell_18 (GGraph, 76x76) [无绑定]
├─ cell_19 (GGraph, 76x76) [无绑定]
├─ cell_20 (GGraph, 76x76) [无绑定]
├─ cell_21 (GGraph, 76x76) [无绑定]
├─ cell_22 (GGraph, 76x76) [无绑定]
├─ cell_23 (GGraph, 76x76) [无绑定]
├─ cell_24 (GGraph, 76x76) [无绑定]
├─ cell_25 (GGraph, 76x76) [无绑定]
├─ cell_26 (GGraph, 76x76) [无绑定]
├─ cell_27 (GGraph, 76x76) [无绑定]
├─ cell_28 (GGraph, 76x76) [无绑定]
├─ cell_29 (GGraph, 76x76) [无绑定]
├─ cell_30 (GGraph, 76x76) [无绑定]
├─ cell_31 (GGraph, 76x76) [无绑定]
├─ cell_32 (GGraph, 76x76) [无绑定]
├─ cell_33 (GGraph, 76x76) [无绑定]
├─ cell_34 (GGraph, 76x76) [无绑定]
├─ cell_35 (GGraph, 76x76) [无绑定]
├─ cell_36 (GGraph, 76x76) [无绑定]
├─ cell_37 (GGraph, 76x76) [无绑定]
├─ cell_38 (GGraph, 76x76) [无绑定]
├─ cell_39 (GGraph, 76x76) [无绑定]
├─ cell_40 (GGraph, 76x76) [无绑定]
├─ cell_41 (GGraph, 76x76) [无绑定]
├─ column_button_0 (GButton, 80x52) [无绑定]
│  ├─ bg (GGraph, 160x54)
│  └─ title (GTextField, 160x54) "按钮"
├─ column_button_1 (GButton, 80x52) [无绑定]
│  ├─ bg (GGraph, 160x54)
│  └─ title (GTextField, 160x54) "按钮"
├─ column_button_2 (GButton, 80x52) [无绑定]
│  ├─ bg (GGraph, 160x54)
│  └─ title (GTextField, 160x54) "按钮"
├─ column_button_3 (GButton, 80x52) [无绑定]
│  ├─ bg (GGraph, 160x54)
│  └─ title (GTextField, 160x54) "按钮"
├─ column_button_4 (GButton, 80x52) [无绑定]
│  ├─ bg (GGraph, 160x54)
│  └─ title (GTextField, 160x54) "按钮"
├─ column_button_5 (GButton, 80x52) [无绑定]
│  ├─ bg (GGraph, 160x54)
│  └─ title (GTextField, 160x54) "按钮"
├─ column_button_6 (GButton, 80x52) [无绑定]
│  ├─ bg (GGraph, 160x54)
│  └─ title (GTextField, 160x54) "按钮"
├─ start_button (GButton, 160x56) [无绑定]
│  ├─ bg (GGraph, 160x54)
│  └─ title (GTextField, 160x54) "按钮"
├─ restart_button (GButton, 160x56) [无绑定]
│  ├─ bg (GGraph, 160x54)
│  └─ title (GTextField, 160x54) "按钮"
└─ tips_text (GTextField, 880x34) [无绑定] "点击列号落子，…"
```
