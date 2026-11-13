# UIProduction 包结构

> 由 gencode 插件自动生成（发布时更新），请勿手改。
> 标注说明：`Wrapper·FGUI类型·mode`；`→ varName` 为生成的 C# 属性名（m_ 前缀）；`[无绑定]` 表示该节点未生成 C# 属性（无名称或 n+数字 自动名）；`[pkg=X]` 跨包引用；`← 资源名` 图片资源；`"文本"` 文案；`(↺)` 循环引用已省略。

## UIProduction (UIProduction·GComponent, 1080x2160) [exported]

```text
Controllers: tab(生产, 产线), window(隐藏, 显示)
├─ screen_base_image (GGraph, 1080x2160) [无绑定]
├─ screen_chrome (UIProductionScreenChrome·GComponent, 1080x2160) [无绑定]
│  ├─ screen_background_part0 (GImage, 1080x2048) ← screen_background_p0
│  ├─ screen_background_part1 (GImage, 1080x112) ← screen_background_p1
│  ├─ back_button (UIProductionBackButton·GButton, 100x100)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  └─ up (GImage, 99x99) ← back_button
│  ├─ production_navigation_button (UIProductionProductionNavigationButton·GButton, 270x104)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  ├─ up (GImage, 270x103) ← production_background
│  │  ├─ production_title_shadow (GTextField, 99x47) "生产"
│  │  └─ title (GTextField, 99x47) "生产"
│  ├─ adventure_navigation_button (UIProductionAdventureNavigationButton·GButton, 270x104)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  ├─ up (GImage, 270x103) ← adventure_background
│  │  ├─ adventure_title_shadow (GTextField, 101x48) "探险"
│  │  └─ title (GTextField, 102x48) "探险"
│  ├─ training_navigation_button (UIProductionTrainingNavigationButton·GButton, 1080x2160)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  ├─ up (GImage, 270x103) ← training_background
│  │  ├─ training_title_shadow (GTextField, 99x49) "培养"
│  │  └─ title (GTextField, 100x48) "培养"
│  ├─ production_tab_button (UIProductionProductionTabButton·GButton·Check, 210x62)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  ├─ up (GImage, 209x62) ← production_tab_background
│  │  └─ title (GTextField, 73x31) "生 产"
│  ├─ production_lines_tab_button (UIProductionProductionLinesTabButton·GButton·Check, 210x62)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  ├─ up (GImage, 209x62) ← production_lines_tab_background
│  │  └─ title (GTextField, 75x31) "产 线"
│  ├─ screen_title_shadow (GTextField, 185x89) "生产"
│  └─ title (GTextField, 185x89) "生产"
├─ buildings_panel (UIProductionBuildingsPanel·GComponent, 1036x1544) [无绑定]
│  ├─ description_outer_panel (GImage, 1033x175) ← deco_descriptionouterpanel
│  ├─ description_header_background (GImage, 1003x49) ← deco_descriptionheaderbackground
│  ├─ description_body_background (GImage, 1011x153) ← deco_descriptionbodybackground
│  ├─ description_right_corner_decoration (GImage, 25x25) ← deco_descriptionrightcornerdecoration
│  ├─ description_left_corner_decoration (GImage, 25x25) ← deco_descriptionleftcornerdecoration
│  ├─ description_heading (GTextField, 80x32) "描 述"
│  ├─ description_body (GTextField, 957x65) "配置生产线最终…"
│  ├─ available_workers_background (GImage, 547x57) ← available_workers_background
│  ├─ available_workers (GTextField, 284x27) "可分配工人：100/20…"
│  ├─ all_category_button (UIProductionAllCategoryButton·GButton·Check, 170x54)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  ├─ all_category_border (GImage, 167x50) ← all_category_border
│  │  ├─ up (GImage, 167x50) ← all_category_background
│  │  ├─ all_category_decoration (GImage, 15x15) ← all_category_decoration
│  │  └─ title (GTextField, 59x25) "全 部"
│  ├─ left_category_button (UIProductionProductionLeftCategoryButton·GButton·Check, 170x52)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  ├─ production_left_category_border (GImage, 168x50) ← production_left_category_border
│  │  ├─ up (GImage, 168x50) ← production_left_category_background
│  │  ├─ production_left_category_decoration (GImage, 15x15) ← production_left_category_decoration
│  │  └─ title (GTextField, 60x25) "生 产"
│  ├─ center_left_category_button (UIProductionProductionCenterLeftCategoryButton·GButton·Check, 170x52)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  ├─ production_center_left_category_border (GImage, 168x50) ← production_center_left_category_border
│  │  ├─ up (GImage, 168x50) ← production_center_left_category_background
│  │  ├─ production_center_left_category_decoration (GImage, 15x15) ← production_center_left_category_decoration
│  │  └─ title (GTextField, 60x25) "生 产"
│  ├─ center_right_category_button (UIProductionProductionCenterRightCategoryButton·GButton·Check, 170x52)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  ├─ production_center_right_category_border (GImage, 168x50) ← production_center_left_category_border
│  │  ├─ up (GImage, 168x50) ← production_center_left_category_background
│  │  ├─ production_center_right_category_decoration (GImage, 15x15) ← production_center_left_category_decoration
│  │  └─ title (GTextField, 60x25) "生 产"
│  ├─ right_category_button (UIProductionProductionRightCategoryButton·GButton·Check, 172x54)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  ├─ production_right_category_border (GImage, 167x50) ← production_right_category_border
│  │  ├─ up (GImage, 167x50) ← production_right_category_background
│  │  ├─ production_right_category_decoration (GImage, 15x15) ← production_right_category_decoration
│  │  └─ title (GTextField, 60x25) "生 产"
│  ├─ production_lines_category_button (UIProductionProductionLinesCategoryButton·GButton·Check, 170x52)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  ├─ production_lines_category_border (GImage, 167x50) ← production_lines_category_border
│  │  ├─ up (GImage, 167x50) ← production_lines_category_background
│  │  ├─ production_lines_category_decoration (GImage, 15x15) ← production_lines_category_decoration
│  │  └─ title (GTextField, 61x25) "产 线"
│  └─ building_list (GList, 1010x868)
│        └─ defaultItem: UIProductionBuildingCard (UIProductionBuildingCard·GComponent)
├─ production_lines_panel (UIProductionProductionLinesPanel·GComponent, 1038x1334) [无绑定]
│  ├─ deco_descriptionouterpanel (GImage, 1033x175) ← deco_descriptionouterpanel
│  ├─ deco_descriptionheaderbackground (GImage, 1003x49) ← deco_descriptionheaderbackground
│  ├─ deco_descriptionbodybackground (GImage, 1011x153) ← deco_descriptionbodybackground
│  ├─ deco_descriptionrightcornerdecoration (GImage, 25x25) ← deco_descriptionrightcornerdecoration
│  ├─ deco_descriptionleftcornerdecoration (GImage, 25x25) ← deco_descriptionleftcornerdecoration
│  ├─ description_heading (GTextField, 80x32) "描 述"
│  ├─ description_body (GTextField, 957x65) "配置生产线最终…"
│  ├─ deco_availableworkersbackground (GImage, 547x57) ← deco_availableworkersbackground
│  ├─ available_workers (GTextField, 289x28) "可分配工人：100/20…"
│  ├─ line_list (GList, 1034x1054)
│  │     └─ defaultItem: UIProductionProductionLineCard (UIProductionProductionLineCard·GComponent)
│  └─ empty_line_card (UIProductionEmptyProductionLineCard·GComponent, 1036x336)
│     ├─ deco_cardpanel2 (GImage, 1033x287) ← deco_cardpanel2
│     ├─ deco_cardcornerdecoration2 (GImage, 13x13) ← deco_cardcornerdecoration2
│     ├─ production_line_name (GTextField, 122x33) "生产线3"
│     ├─ deco_editlinebutton3 (GImage, 40x40) ← deco_editlinebutton3
│     ├─ deco_deletelinebutton3 (GImage, 37x41) ← deco_deletelinebutton3
│     ├─ create_line_hint (GTextField, 258x36) "点击新建生产线"
│     ├─ create_line_button (UIProductionCreateLineButton·GButton, 84x86)
│     │  Controllers: button(up, down, over, selectedOver)
│     │  └─ up (GImage, 84x86) ← deco_createlinebutton
│     └─ create_line_hint (GTextField, 258x36) "点击新建生产线"
├─ modal_dim_overlay (GGraph, 1080x2160) [无绑定]
├─ create_line_window_frame (UIProductionCreateLineWindowFrame·GComponent, 910x1028) [无绑定]
│  ├─ window_center_decoration (GImage, 31x30) ← window_center_decoration
│  ├─ window_outer_panel (GImage, 899x997) ← window_outer_panel
│  ├─ window_inner_panel (GImage, 867x966) ← window_inner_panel
│  ├─ window_top_highlight (GImage, 31x7) ← window_top_highlight
│  ├─ window_top_highlight_dot (GImage, 7x7) ← window_top_highlight_dot
│  ├─ window_bottom_highlight (GImage, 31x7) ← window_bottom_highlight
│  ├─ window_bottom_highlight_dot (GImage, 7x7) ← window_bottom_highlight_dot
│  ├─ title_small_star (GImage, 17x18) ← title_small_star
│  ├─ title_left_star (GImage, 32x32) ← title_left_star
│  ├─ title_right_star (GImage, 32x32) ← title_right_star
│  ├─ title_top_star (GImage, 16x15) ← title_top_star
│  ├─ title_bottom_star (GImage, 16x16) ← title_bottom_star
│  ├─ close_button (UIProductionCloseLineWindowButton·GButton, 58x60)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  └─ up (GImage, 57x59) ← closebutton
│  ├─ title (UIProductionCreateLineWindowTitle·GComponent, 228x80)
│  │  ├─ window_title_background (GImage, 228x79) ← window_title_background
│  │  └─ title (GTextField, 125x30) "建立产线"
│  └─ window_content_divider (GImage, 315x12) ← window_content_divider
└─ create_line_content (UIProductionCreateLineContent·GComponent, 770x868) [无绑定]
   ├─ product_catalog (UIProductionProductCatalog·GComponent, 770x750)
   │  ├─ catalog_outer_panel (GImage, 766x747) ← catalog_outer_panel
   │  ├─ catalog_header_background (GImage, 743x66) ← catalog_header_background
   │  ├─ catalog_body_background (GImage, 750x730) ← catalog_body_background
   │  ├─ catalog_right_corner_decoration (GImage, 19x19) ← catalog_right_corner_decoration
   │  ├─ catalog_left_corner_decoration (GImage, 19x19) ← catalog_left_corner_decoration
   │  ├─ era_selected_background (GImage, 121x70) ← era_selected_background
   │  ├─ era_button_0 (UIProductionEraButton·GButton·Check, 100x70)
   │  │  Controllers: button(up, down, over, selectedOver)
   │  │  └─ title (GTextField, 56x28) "全部"
   │  ├─ era_button_1 (UIProductionEraButton·GButton·Check, 100x70)
   │  │  Controllers: button(up, down, over, selectedOver)
   │  │  └─ title (GTextField, 56x28) "全部"
   │  ├─ era_button_2 (UIProductionEraButton·GButton·Check, 100x70)
   │  │  Controllers: button(up, down, over, selectedOver)
   │  │  └─ title (GTextField, 56x28) "全部"
   │  ├─ era_button_3 (UIProductionEraButton·GButton·Check, 100x70)
   │  │  Controllers: button(up, down, over, selectedOver)
   │  │  └─ title (GTextField, 56x28) "全部"
   │  ├─ era_button_4 (UIProductionEraButton·GButton·Check, 100x70)
   │  │  Controllers: button(up, down, over, selectedOver)
   │  │  └─ title (GTextField, 56x28) "全部"
   │  ├─ era_button_5 (UIProductionEraButton·GButton·Check, 100x70)
   │  │  Controllers: button(up, down, over, selectedOver)
   │  │  └─ title (GTextField, 56x28) "全部"
   │  └─ product_list (GList, 719x622)
   │        └─ defaultItem: UIProductionProductCard (UIProductionProductCard·GComponent)
   ├─ production_line_instructions (GTextField, 691x29) "选择2级以上或产…"
   └─ production_recipe_instructions (GTextField, 526x28) "可以手动修改产…"
```

## UIProductionProductionRightCategoryButton (UIProductionProductionRightCategoryButton·GButton, 172x54) [exported]

```text
Controllers: button(up, down, over, selectedOver)
├─ production_right_category_border (GImage, 167x50) → m_production_right_category_border ← production_right_category_border
├─ up (GImage, 167x50) → m_up ← production_right_category_background
├─ production_right_category_decoration (GImage, 15x15) → m_production_right_category_decoration ← production_right_category_decoration
└─ title (GTextField, 60x25) [无绑定] "生 产"
```

## UIProductionScreenChrome (UIProductionScreenChrome·GComponent, 1080x2160) [exported]

```text
├─ screen_background_part0 (GImage, 1080x2048) [无绑定] ← screen_background_p0
├─ screen_background_part1 (GImage, 1080x112) [无绑定] ← screen_background_p1
├─ back_button (UIProductionBackButton·GButton, 100x100) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  └─ up (GImage, 99x99) ← back_button
├─ production_navigation_button (UIProductionProductionNavigationButton·GButton, 270x104) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  ├─ up (GImage, 270x103) ← production_background
│  ├─ production_title_shadow (GTextField, 99x47) "生产"
│  └─ title (GTextField, 99x47) "生产"
├─ adventure_navigation_button (UIProductionAdventureNavigationButton·GButton, 270x104) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  ├─ up (GImage, 270x103) ← adventure_background
│  ├─ adventure_title_shadow (GTextField, 101x48) "探险"
│  └─ title (GTextField, 102x48) "探险"
├─ training_navigation_button (UIProductionTrainingNavigationButton·GButton, 1080x2160) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  ├─ up (GImage, 270x103) ← training_background
│  ├─ training_title_shadow (GTextField, 99x49) "培养"
│  └─ title (GTextField, 100x48) "培养"
├─ production_tab_button (UIProductionProductionTabButton·GButton·Check, 210x62) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  ├─ up (GImage, 209x62) ← production_tab_background
│  └─ title (GTextField, 73x31) "生 产"
├─ production_lines_tab_button (UIProductionProductionLinesTabButton·GButton·Check, 210x62) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  ├─ up (GImage, 209x62) ← production_lines_tab_background
│  └─ title (GTextField, 75x31) "产 线"
├─ screen_title_shadow (GTextField, 185x89) [无绑定] "生产"
└─ title (GTextField, 185x89) [无绑定] "生产"
```

## UIProductionProductionTabButton (UIProductionProductionTabButton·GButton, 210x62) [exported]

```text
Controllers: button(up, down, over, selectedOver)
├─ up (GImage, 209x62) → m_up ← production_tab_background
└─ title (GTextField, 73x31) [无绑定] "生 产"
```

## UIProductionProductionLinesTabButton (UIProductionProductionLinesTabButton·GButton, 210x62) [exported]

```text
Controllers: button(up, down, over, selectedOver)
├─ up (GImage, 209x62) → m_up ← production_lines_tab_background
└─ title (GTextField, 75x31) [无绑定] "产 线"
```

## UIProductionProductionNavigationButton (UIProductionProductionNavigationButton·GButton, 270x104) [exported]

```text
Controllers: button(up, down, over, selectedOver)
├─ up (GImage, 270x103) → m_up ← production_background
├─ production_title_shadow (GTextField, 99x47) → m_production_title_shadow "生产"
└─ title (GTextField, 99x47) [无绑定] "生产"
```

## UIProductionAdventureNavigationButton (UIProductionAdventureNavigationButton·GButton, 270x104) [exported]

```text
Controllers: button(up, down, over, selectedOver)
├─ up (GImage, 270x103) → m_up ← adventure_background
├─ adventure_title_shadow (GTextField, 101x48) → m_adventure_title_shadow "探险"
└─ title (GTextField, 102x48) [无绑定] "探险"
```

## UIProductionTrainingNavigationButton (UIProductionTrainingNavigationButton·GButton, 1080x2160) [exported]

```text
Controllers: button(up, down, over, selectedOver)
├─ up (GImage, 270x103) → m_up ← training_background
├─ training_title_shadow (GTextField, 99x49) → m_training_title_shadow "培养"
└─ title (GTextField, 100x48) [无绑定] "培养"
```

## UIProductionBackButton (UIProductionBackButton·GButton, 100x100) [exported]

```text
Controllers: button(up, down, over, selectedOver)
└─ up (GImage, 99x99) → m_up ← back_button
```

## UIProductionBuildingsPanel (UIProductionBuildingsPanel·GComponent, 1036x1544) [exported]

```text
├─ description_outer_panel (GImage, 1033x175) [无绑定] ← deco_descriptionouterpanel
├─ description_header_background (GImage, 1003x49) [无绑定] ← deco_descriptionheaderbackground
├─ description_body_background (GImage, 1011x153) [无绑定] ← deco_descriptionbodybackground
├─ description_right_corner_decoration (GImage, 25x25) [无绑定] ← deco_descriptionrightcornerdecoration
├─ description_left_corner_decoration (GImage, 25x25) [无绑定] ← deco_descriptionleftcornerdecoration
├─ description_heading (GTextField, 80x32) [无绑定] "描 述"
├─ description_body (GTextField, 957x65) [无绑定] "配置生产线最终…"
├─ available_workers_background (GImage, 547x57) [无绑定] ← available_workers_background
├─ available_workers (GTextField, 284x27) [无绑定] "可分配工人：100/20…"
├─ all_category_button (UIProductionAllCategoryButton·GButton·Check, 170x54) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  ├─ all_category_border (GImage, 167x50) ← all_category_border
│  ├─ up (GImage, 167x50) ← all_category_background
│  ├─ all_category_decoration (GImage, 15x15) ← all_category_decoration
│  └─ title (GTextField, 59x25) "全 部"
├─ left_category_button (UIProductionProductionLeftCategoryButton·GButton·Check, 170x52) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  ├─ production_left_category_border (GImage, 168x50) ← production_left_category_border
│  ├─ up (GImage, 168x50) ← production_left_category_background
│  ├─ production_left_category_decoration (GImage, 15x15) ← production_left_category_decoration
│  └─ title (GTextField, 60x25) "生 产"
├─ center_left_category_button (UIProductionProductionCenterLeftCategoryButton·GButton·Check, 170x52) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  ├─ production_center_left_category_border (GImage, 168x50) ← production_center_left_category_border
│  ├─ up (GImage, 168x50) ← production_center_left_category_background
│  ├─ production_center_left_category_decoration (GImage, 15x15) ← production_center_left_category_decoration
│  └─ title (GTextField, 60x25) "生 产"
├─ center_right_category_button (UIProductionProductionCenterRightCategoryButton·GButton·Check, 170x52) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  ├─ production_center_right_category_border (GImage, 168x50) ← production_center_left_category_border
│  ├─ up (GImage, 168x50) ← production_center_left_category_background
│  ├─ production_center_right_category_decoration (GImage, 15x15) ← production_center_left_category_decoration
│  └─ title (GTextField, 60x25) "生 产"
├─ right_category_button (UIProductionProductionRightCategoryButton·GButton·Check, 172x54) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  ├─ production_right_category_border (GImage, 167x50) ← production_right_category_border
│  ├─ up (GImage, 167x50) ← production_right_category_background
│  ├─ production_right_category_decoration (GImage, 15x15) ← production_right_category_decoration
│  └─ title (GTextField, 60x25) "生 产"
├─ production_lines_category_button (UIProductionProductionLinesCategoryButton·GButton·Check, 170x52) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  ├─ production_lines_category_border (GImage, 167x50) ← production_lines_category_border
│  ├─ up (GImage, 167x50) ← production_lines_category_background
│  ├─ production_lines_category_decoration (GImage, 15x15) ← production_lines_category_decoration
│  └─ title (GTextField, 61x25) "产 线"
└─ building_list (GList, 1010x868) [无绑定]
      └─ defaultItem: UIProductionBuildingCard (UIProductionBuildingCard·GComponent)
```

## UIProductionBuildingCard (UIProductionBuildingCard·GComponent, 326x272) [exported]

```text
├─ card_panel (GImage, 324x269) [无绑定] ← card_panel
├─ card_top_left_decoration (GImage, 8x9) [无绑定] ← card_top_left_decoration
├─ card_top_divider (GImage, 175x7) [无绑定] ← card_top_divider
├─ card_bottom_divider (GImage, 175x7) [无绑定] ← card_bottom_divider
├─ title (GTextField, 153x22) [无绑定] "伐木场 LV.256"
├─ quantity (UIProductionWorkerQuantityControl·GComponent, 252x42) [无绑定]
│  ├─ decrease_button (UIProductionDecreaseButton·GButton, 40x40)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  ├─ up (GImage, 38x38) ← decrease_background
│  │  └─ icon (GImage, 28x9) ← decrease_symbol
│  ├─ increase_button (UIProductionIncreaseButton·GButton, 40x40)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  ├─ up (GImage, 38x38) ← increase_background
│  │  └─ icon (GImage, 28x27) ← increase_symbol
│  ├─ quantity_background (GImage, 163x37) ← quantity_background
│  ├─ value (GTextField, 26x18) "60"
│  ├─ maximum_button (UIProductionMaximumWorkersButton·GButton, 40x40)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  ├─ up (GImage, 38x38) ← maximum_background
│  │  ├─ maximum_arrow_primary (GTextField, 18x20) ">"
│  │  └─ maximum_arrow_secondary (GTextField, 18x20) ">"
│  └─ minimum_button (UIProductionMinimumWorkersButton·GButton, 40x40)
│     Controllers: button(up, down, over, selectedOver)
│     ├─ up (GImage, 38x38) ← minimum_background
│     ├─ minimum_arrow_primary (GTextField, 18x20) ">"
│     └─ minimum_arrow_secondary (GTextField, 18x20) ">"
└─ building_image (GLoader, 122x146) [无绑定]
```

## UIProductionWorkerQuantityControl (UIProductionWorkerQuantityControl·GComponent, 252x42) [exported]

```text
├─ decrease_button (UIProductionDecreaseButton·GButton, 40x40) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  ├─ up (GImage, 38x38) ← decrease_background
│  └─ icon (GImage, 28x9) ← decrease_symbol
├─ increase_button (UIProductionIncreaseButton·GButton, 40x40) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  ├─ up (GImage, 38x38) ← increase_background
│  └─ icon (GImage, 28x27) ← increase_symbol
├─ quantity_background (GImage, 163x37) [无绑定] ← quantity_background
├─ value (GTextField, 26x18) [无绑定] "60"
├─ maximum_button (UIProductionMaximumWorkersButton·GButton, 40x40) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  ├─ up (GImage, 38x38) ← maximum_background
│  ├─ maximum_arrow_primary (GTextField, 18x20) ">"
│  └─ maximum_arrow_secondary (GTextField, 18x20) ">"
└─ minimum_button (UIProductionMinimumWorkersButton·GButton, 40x40) [无绑定]
   Controllers: button(up, down, over, selectedOver)
   ├─ up (GImage, 38x38) ← minimum_background
   ├─ minimum_arrow_primary (GTextField, 18x20) ">"
   └─ minimum_arrow_secondary (GTextField, 18x20) ">"
```

## UIProductionMaximumWorkersButton (UIProductionMaximumWorkersButton·GButton, 40x40) [exported]

```text
Controllers: button(up, down, over, selectedOver)
├─ up (GImage, 38x38) → m_up ← maximum_background
├─ maximum_arrow_primary (GTextField, 18x20) → m_maximum_arrow_primary ">"
└─ maximum_arrow_secondary (GTextField, 18x20) → m_maximum_arrow_secondary ">"
```

## UIProductionMinimumWorkersButton (UIProductionMinimumWorkersButton·GButton, 40x40) [exported]

```text
Controllers: button(up, down, over, selectedOver)
├─ up (GImage, 38x38) → m_up ← minimum_background
├─ minimum_arrow_primary (GTextField, 18x20) → m_minimum_arrow_primary ">"
└─ minimum_arrow_secondary (GTextField, 18x20) → m_minimum_arrow_secondary ">"
```

## UIProductionIncreaseButton (UIProductionIncreaseButton·GButton, 40x40) [exported]

```text
Controllers: button(up, down, over, selectedOver)
├─ up (GImage, 38x38) → m_up ← increase_background
└─ icon (GImage, 28x27) [无绑定] ← increase_symbol
```

## UIProductionDecreaseButton (UIProductionDecreaseButton·GButton, 40x40) [exported]

```text
Controllers: button(up, down, over, selectedOver)
├─ up (GImage, 38x38) → m_up ← decrease_background
└─ icon (GImage, 28x9) [无绑定] ← decrease_symbol
```

## UIProductionProductionLinesPanel (UIProductionProductionLinesPanel·GComponent, 1038x1334) [exported]

```text
├─ deco_descriptionouterpanel (GImage, 1033x175) [无绑定] ← deco_descriptionouterpanel
├─ deco_descriptionheaderbackground (GImage, 1003x49) [无绑定] ← deco_descriptionheaderbackground
├─ deco_descriptionbodybackground (GImage, 1011x153) [无绑定] ← deco_descriptionbodybackground
├─ deco_descriptionrightcornerdecoration (GImage, 25x25) [无绑定] ← deco_descriptionrightcornerdecoration
├─ deco_descriptionleftcornerdecoration (GImage, 25x25) [无绑定] ← deco_descriptionleftcornerdecoration
├─ description_heading (GTextField, 80x32) [无绑定] "描 述"
├─ description_body (GTextField, 957x65) [无绑定] "配置生产线最终…"
├─ deco_availableworkersbackground (GImage, 547x57) [无绑定] ← deco_availableworkersbackground
├─ available_workers (GTextField, 289x28) [无绑定] "可分配工人：100/20…"
├─ line_list (GList, 1034x1054) [无绑定]
│     └─ defaultItem: UIProductionProductionLineCard (UIProductionProductionLineCard·GComponent)
└─ empty_line_card (UIProductionEmptyProductionLineCard·GComponent, 1036x336) [无绑定]
   ├─ deco_cardpanel2 (GImage, 1033x287) ← deco_cardpanel2
   ├─ deco_cardcornerdecoration2 (GImage, 13x13) ← deco_cardcornerdecoration2
   ├─ production_line_name (GTextField, 122x33) "生产线3"
   ├─ deco_editlinebutton3 (GImage, 40x40) ← deco_editlinebutton3
   ├─ deco_deletelinebutton3 (GImage, 37x41) ← deco_deletelinebutton3
   ├─ create_line_hint (GTextField, 258x36) "点击新建生产线"
   ├─ create_line_button (UIProductionCreateLineButton·GButton, 84x86)
   │  Controllers: button(up, down, over, selectedOver)
   │  └─ up (GImage, 84x86) ← deco_createlinebutton
   └─ create_line_hint (GTextField, 258x36) "点击新建生产线"
```

## UIProductionProductionLineCard (UIProductionProductionLineCard·GComponent, 1034x334) [exported]

```text
├─ deco_cardpanel (GImage, 1033x287) [无绑定] ← deco_cardpanel
├─ deco_cardcornerdecoration (GImage, 13x13) [无绑定] ← deco_cardcornerdecoration
├─ deco_carddivider (GImage, 397x15) [无绑定] ← deco_carddivider
├─ production_line_name (GTextField, 113x33) [无绑定] "生产线1"
├─ product_label (UIProductionProductionLineProductLabel·GComponent, 202x48) [无绑定]
│  ├─ deco_productlabelbackground (GImage, 201x47) ← deco_productlabelbackground
│  └─ title (GTextField, 162x27) "现代建筑组件"
├─ edit_line_button (UIProductionEditLineButton·GButton, 40x40) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  └─ up (GImage, 40x40) ← deco_editlinebutton
├─ delete_line_button (UIProductionDeleteLineButton·GButton, 38x42) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  └─ up (GImage, 37x41) ← deco_deletelinebutton
├─ output_label (GTextField, 80x33) [无绑定] "产出："
├─ output_rate (GTextField, 123x32) [无绑定] "x0.5/天"
├─ inventory_value (GTextField, 249x35) [无绑定] "(库存：1.6/100）"
├─ output_product_label (UIProductionOutputProductLabel·GComponent, 162x38) [无绑定]
│  ├─ deco_outputproductbackground (GImage, 161x37) ← deco_outputproductbackground
│  └─ title (GTextField, 130x21) "现代建筑组件"
├─ worker_allocation_label (GTextField, 125x28) [无绑定] "工人分配："
└─ quantity (UIProductionWorkerQuantityControl·GComponent, 396x40) [无绑定]
   ├─ decrease_button (UIProductionDecreaseButton·GButton, 40x40)
   │  Controllers: button(up, down, over, selectedOver)
   │  ├─ up (GImage, 38x38) ← decrease_background
   │  └─ icon (GImage, 28x9) ← decrease_symbol
   ├─ increase_button (UIProductionIncreaseButton·GButton, 40x40)
   │  Controllers: button(up, down, over, selectedOver)
   │  ├─ up (GImage, 38x38) ← increase_background
   │  └─ icon (GImage, 28x27) ← increase_symbol
   ├─ quantity_background (GImage, 163x37) ← quantity_background
   ├─ value (GTextField, 26x18) "60"
   ├─ maximum_button (UIProductionMaximumWorkersButton·GButton, 40x40)
   │  Controllers: button(up, down, over, selectedOver)
   │  ├─ up (GImage, 38x38) ← maximum_background
   │  ├─ maximum_arrow_primary (GTextField, 18x20) ">"
   │  └─ maximum_arrow_secondary (GTextField, 18x20) ">"
   └─ minimum_button (UIProductionMinimumWorkersButton·GButton, 40x40)
      Controllers: button(up, down, over, selectedOver)
      ├─ up (GImage, 38x38) ← minimum_background
      ├─ minimum_arrow_primary (GTextField, 18x20) ">"
      └─ minimum_arrow_secondary (GTextField, 18x20) ">"
```

## UIProductionProductionLineProductLabel (UIProductionProductionLineProductLabel·GComponent, 202x48) [exported]

```text
├─ deco_productlabelbackground (GImage, 201x47) [无绑定] ← deco_productlabelbackground
└─ title (GTextField, 162x27) [无绑定] "现代建筑组件"
```

## UIProductionOutputProductLabel (UIProductionOutputProductLabel·GComponent, 162x38) [exported]

```text
├─ deco_outputproductbackground (GImage, 161x37) [无绑定] ← deco_outputproductbackground
└─ title (GTextField, 130x21) [无绑定] "现代建筑组件"
```

## UIProductionEmptyProductionLineCard (UIProductionEmptyProductionLineCard·GComponent, 1036x336) [exported]

```text
├─ deco_cardpanel2 (GImage, 1033x287) [无绑定] ← deco_cardpanel2
├─ deco_cardcornerdecoration2 (GImage, 13x13) [无绑定] ← deco_cardcornerdecoration2
├─ production_line_name (GTextField, 122x33) [无绑定] "生产线3"
├─ deco_editlinebutton3 (GImage, 40x40) [无绑定] ← deco_editlinebutton3
├─ deco_deletelinebutton3 (GImage, 37x41) [无绑定] ← deco_deletelinebutton3
├─ create_line_hint (GTextField, 258x36) [无绑定] "点击新建生产线"
├─ create_line_button (UIProductionCreateLineButton·GButton, 84x86) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  └─ up (GImage, 84x86) ← deco_createlinebutton
└─ create_line_hint (GTextField, 258x36) [无绑定] "点击新建生产线"
```

## UIProductionCreateLineButton (UIProductionCreateLineButton·GButton, 84x86) [exported]

```text
Controllers: button(up, down, over, selectedOver)
└─ up (GImage, 84x86) → m_up ← deco_createlinebutton
```

## UIProductionEditLineButton (UIProductionEditLineButton·GButton, 40x40) [exported]

```text
Controllers: button(up, down, over, selectedOver)
└─ up (GImage, 40x40) → m_up ← deco_editlinebutton
```

## UIProductionDeleteLineButton (UIProductionDeleteLineButton·GButton, 38x42) [exported]

```text
Controllers: button(up, down, over, selectedOver)
└─ up (GImage, 37x41) → m_up ← deco_deletelinebutton
```

## UIProductionCreateLineWindowFrame (UIProductionCreateLineWindowFrame·GComponent, 910x1028) [exported]

```text
├─ window_center_decoration (GImage, 31x30) [无绑定] ← window_center_decoration
├─ window_outer_panel (GImage, 899x997) [无绑定] ← window_outer_panel
├─ window_inner_panel (GImage, 867x966) [无绑定] ← window_inner_panel
├─ window_top_highlight (GImage, 31x7) [无绑定] ← window_top_highlight
├─ window_top_highlight_dot (GImage, 7x7) [无绑定] ← window_top_highlight_dot
├─ window_bottom_highlight (GImage, 31x7) [无绑定] ← window_bottom_highlight
├─ window_bottom_highlight_dot (GImage, 7x7) [无绑定] ← window_bottom_highlight_dot
├─ title_small_star (GImage, 17x18) [无绑定] ← title_small_star
├─ title_left_star (GImage, 32x32) [无绑定] ← title_left_star
├─ title_right_star (GImage, 32x32) [无绑定] ← title_right_star
├─ title_top_star (GImage, 16x15) [无绑定] ← title_top_star
├─ title_bottom_star (GImage, 16x16) [无绑定] ← title_bottom_star
├─ close_button (UIProductionCloseLineWindowButton·GButton, 58x60) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  └─ up (GImage, 57x59) ← closebutton
├─ title (UIProductionCreateLineWindowTitle·GComponent, 228x80) [无绑定]
│  ├─ window_title_background (GImage, 228x79) ← window_title_background
│  └─ title (GTextField, 125x30) "建立产线"
└─ window_content_divider (GImage, 315x12) [无绑定] ← window_content_divider
```

## UIProductionCreateLineWindowTitle (UIProductionCreateLineWindowTitle·GComponent, 228x80) [exported]

```text
├─ window_title_background (GImage, 228x79) [无绑定] ← window_title_background
└─ title (GTextField, 125x30) [无绑定] "建立产线"
```

## UIProductionCreateLineContent (UIProductionCreateLineContent·GComponent, 770x868) [exported]

```text
├─ product_catalog (UIProductionProductCatalog·GComponent, 770x750) [无绑定]
│  ├─ catalog_outer_panel (GImage, 766x747) ← catalog_outer_panel
│  ├─ catalog_header_background (GImage, 743x66) ← catalog_header_background
│  ├─ catalog_body_background (GImage, 750x730) ← catalog_body_background
│  ├─ catalog_right_corner_decoration (GImage, 19x19) ← catalog_right_corner_decoration
│  ├─ catalog_left_corner_decoration (GImage, 19x19) ← catalog_left_corner_decoration
│  ├─ era_selected_background (GImage, 121x70) ← era_selected_background
│  ├─ era_button_0 (UIProductionEraButton·GButton·Check, 100x70)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  └─ title (GTextField, 56x28) "全部"
│  ├─ era_button_1 (UIProductionEraButton·GButton·Check, 100x70)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  └─ title (GTextField, 56x28) "全部"
│  ├─ era_button_2 (UIProductionEraButton·GButton·Check, 100x70)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  └─ title (GTextField, 56x28) "全部"
│  ├─ era_button_3 (UIProductionEraButton·GButton·Check, 100x70)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  └─ title (GTextField, 56x28) "全部"
│  ├─ era_button_4 (UIProductionEraButton·GButton·Check, 100x70)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  └─ title (GTextField, 56x28) "全部"
│  ├─ era_button_5 (UIProductionEraButton·GButton·Check, 100x70)
│  │  Controllers: button(up, down, over, selectedOver)
│  │  └─ title (GTextField, 56x28) "全部"
│  └─ product_list (GList, 719x622)
│        └─ defaultItem: UIProductionProductCard (UIProductionProductCard·GComponent)
├─ production_line_instructions (GTextField, 691x29) [无绑定] "选择2级以上或产…"
└─ production_recipe_instructions (GTextField, 526x28) [无绑定] "可以手动修改产…"
```

## UIProductionProductCatalog (UIProductionProductCatalog·GComponent, 770x750) [exported]

```text
├─ catalog_outer_panel (GImage, 766x747) [无绑定] ← catalog_outer_panel
├─ catalog_header_background (GImage, 743x66) [无绑定] ← catalog_header_background
├─ catalog_body_background (GImage, 750x730) [无绑定] ← catalog_body_background
├─ catalog_right_corner_decoration (GImage, 19x19) [无绑定] ← catalog_right_corner_decoration
├─ catalog_left_corner_decoration (GImage, 19x19) [无绑定] ← catalog_left_corner_decoration
├─ era_selected_background (GImage, 121x70) [无绑定] ← era_selected_background
├─ era_button_0 (UIProductionEraButton·GButton·Check, 100x70) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  └─ title (GTextField, 56x28) "全部"
├─ era_button_1 (UIProductionEraButton·GButton·Check, 100x70) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  └─ title (GTextField, 56x28) "全部"
├─ era_button_2 (UIProductionEraButton·GButton·Check, 100x70) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  └─ title (GTextField, 56x28) "全部"
├─ era_button_3 (UIProductionEraButton·GButton·Check, 100x70) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  └─ title (GTextField, 56x28) "全部"
├─ era_button_4 (UIProductionEraButton·GButton·Check, 100x70) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  └─ title (GTextField, 56x28) "全部"
├─ era_button_5 (UIProductionEraButton·GButton·Check, 100x70) [无绑定]
│  Controllers: button(up, down, over, selectedOver)
│  └─ title (GTextField, 56x28) "全部"
└─ product_list (GList, 719x622) [无绑定]
      └─ defaultItem: UIProductionProductCard (UIProductionProductCard·GComponent)
```

## UIProductionProductCard (UIProductionProductCard·GComponent, 176x204) [exported]

```text
Controllers: state(普通, 选中)
├─ product_card_shadow (GImage, 169x197) [无绑定] ← product_card_shadow
├─ product_card_background (GImage, 170x198) [无绑定] ← product_card_background
├─ product_card_highlight (GImage, 572x287) [无绑定] ← product_card_highlight
├─ product_upper_gradient (GImage, 175x139) [无绑定] ← product_upper_gradient
├─ product_lower_gradient (GImage, 175x140) [无绑定] ← product_lower_gradient
├─ product_caption_background (GImage, 185x47) [无绑定] ← product_caption_background
├─ title (GTextField, 105x26) [无绑定] "坦克组件"
└─ product_image (GLoader, 115x137) [无绑定]
```

## UIProductionEraButton (UIProductionEraButton·GButton, 56x28) [exported]

```text
Controllers: button(up, down, over, selectedOver)
└─ title (GTextField, 56x28) [无绑定] "全部"
```

## UIProductionCloseLineWindowButton (UIProductionCloseLineWindowButton·GButton, 58x60) [exported]

```text
Controllers: button(up, down, over, selectedOver)
└─ up (GImage, 57x59) → m_up ← closebutton
```

## UIProductionAllCategoryButton (UIProductionAllCategoryButton·GButton, 170x54) [exported]

```text
Controllers: button(up, down, over, selectedOver)
├─ all_category_border (GImage, 167x50) → m_all_category_border ← all_category_border
├─ up (GImage, 167x50) → m_up ← all_category_background
├─ all_category_decoration (GImage, 15x15) → m_all_category_decoration ← all_category_decoration
└─ title (GTextField, 59x25) [无绑定] "全 部"
```

## UIProductionProductionLinesCategoryButton (UIProductionProductionLinesCategoryButton·GButton, 170x52) [exported]

```text
Controllers: button(up, down, over, selectedOver)
├─ production_lines_category_border (GImage, 167x50) → m_production_lines_category_border ← production_lines_category_border
├─ up (GImage, 167x50) → m_up ← production_lines_category_background
├─ production_lines_category_decoration (GImage, 15x15) → m_production_lines_category_decoration ← production_lines_category_decoration
└─ title (GTextField, 61x25) [无绑定] "产 线"
```

## UIProductionProductionLeftCategoryButton (UIProductionProductionLeftCategoryButton·GButton, 170x52) [exported]

```text
Controllers: button(up, down, over, selectedOver)
├─ production_left_category_border (GImage, 168x50) → m_production_left_category_border ← production_left_category_border
├─ up (GImage, 168x50) → m_up ← production_left_category_background
├─ production_left_category_decoration (GImage, 15x15) → m_production_left_category_decoration ← production_left_category_decoration
└─ title (GTextField, 60x25) [无绑定] "生 产"
```

## UIProductionProductionCenterLeftCategoryButton (UIProductionProductionCenterLeftCategoryButton·GButton, 170x52) [exported]

```text
Controllers: button(up, down, over, selectedOver)
├─ production_center_left_category_border (GImage, 168x50) → m_production_center_left_category_border ← production_center_left_category_border
├─ up (GImage, 168x50) → m_up ← production_center_left_category_background
├─ production_center_left_category_decoration (GImage, 15x15) → m_production_center_left_category_decoration ← production_center_left_category_decoration
└─ title (GTextField, 60x25) [无绑定] "生 产"
```

## UIProductionProductionCenterRightCategoryButton (UIProductionProductionCenterRightCategoryButton·GButton, 170x52) [exported]

```text
Controllers: button(up, down, over, selectedOver)
├─ production_center_right_category_border (GImage, 168x50) → m_production_center_right_category_border ← production_center_left_category_border
├─ up (GImage, 168x50) → m_up ← production_center_left_category_background
├─ production_center_right_category_decoration (GImage, 15x15) → m_production_center_right_category_decoration ← production_center_left_category_decoration
└─ title (GTextField, 60x25) [无绑定] "生 产"
```
