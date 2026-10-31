[English](../../README.md) · [Français](README.fr.md) · [Italiano](README.it.md) · [Deutsch](README.de.md) · [Español (España)](README.es.md) · [العربية](README.ar.md) · [Bahasa Indonesia](README.id.md) · [Español (Latinoamérica)](README.es-419.md) · [Português (Brasil)](README.pt.md) · [Polski](README.pl.md) · [Русский](README.ru.md) · [简体中文](README.zh-Hans.md) · [繁體中文](README.zh-Hant.md) · [日本語](README.ja.md) · [한국어](README.ko.md) · [ไทย](README.th.md)

---

# BT4 Sparking Controls — 0.9 Test

0.9 更新：自適應相容 R7.1 Hotfix，僅在啟動時分析；倒地後手動起身只用圓圈（圓圈加方向可定向起身，站立時 R1 仍可防禦，自動起身保留）；修正置中 HUD 的方塊覆蓋位置。包含所有先前改進。

解壓縮 Windows ZIP，執行 BT4-SparkingControls.exe。選擇 BT4decomp.exe 或同一資料夾中的 .cmd，然後儲存並啟動。0.9 Test 包含字型、CMD 啟動、對波輸入與頭像修正。設定會保留。僅提供 Windows 版本。

[Windows ZIP](https://github.com/vini2227/BT4-SparkingControls/releases/download/v0.9-test/BT4-SparkingControls-0.9-Test-Windows.zip) · [0.9 Changes / English](../../CHANGELOG.md) · [Full English guide](../../README.md)

## 操作

□ = 方塊；△ = 三角；○ = 圓圈；LS = 左類比搖桿。LS 箭頭表示搖桿方向，不是方向鍵。一般選單保留遊戲原始操作。

| 按鍵／組合 | 動作 |
| --- | --- |
| `□` | Rush 連擊 |
| `△` | 氣彈 |
| `X` | 原生移動與衝刺 |
| `R2` | 集氣 |
| `L1` | 上升 |
| `L2` | 下降 |
| `R2 + L1` | 向上垂直 Z Dash |
| `R2 + L2` | 向下垂直 Z Dash |
| `R1` | 自身支援連段之外的防禦 |
| `R2 + □` | 超級攻擊 1 |
| `R2 + △` | 超級攻擊 2 |
| `R2 + ○` | 究極攻擊，仍須滿足原生條件 |
| `R2 + D-pad ↑ / ↓` | 技能 1 |
| `R2 + D-pad ← / →` | 技能 2 |
| `○` | 原生反擊架勢 |
| `R1 (combo)` | 支援連段中的原生向前瞬移 |
| `R1 + LS (combo)` | 這些連段中的原生方向瞬移 |
| `R1 + □` | 一般抓投 |
| `R1 + LS↓ + □` | 替代倒地抓投，僅限搖桿 |
| `LS↑ + △` | 原始指令倒地抓投 |
| `R3` | Emergency Blaster Wave，須符合原生條件 |
| `L3` | 搜尋／鎖定目標 |
| `Select / View` | 切換鏡頭 |
| `D-pad ↑` | 按住：變身／融合 |
| `D-pad ←` | 按住：切換隊員 |
| `D-pad ↓ / → (no R2)` | 戰鬥中停用以防誤觸 |

已按住 R2 時也能選擇特殊攻擊。要繼續蓄力，請按住對應攻擊鍵；只放開 R2 不會放開攻擊鍵。出現提示時，以 □ + 指定方向輸入原生 boost。覆蓋圖示只遮住三角，不遮手部圖案。技能快捷鍵屬於本輔助程式，並非 ZERO 操作的完全複製。

○ 請求 BT4 原生反擊架勢，在支援連段中優先於持續按住的攻擊，並非完整的 ZERO Perception/Super Perception 系統。R1 瞬移仍須滿足原生氣力及取消窗口，不保證移到敵人背後或停在原地。R1 + □ 優先產生 X—放開—X，不能繞過距離及動作窗口；保留原始雙按 X 抓投。倒地抓投不接受方向鍵 ↓；LS↑ + ○ 不再觸發。R3 使用 BT4 的生命消耗與條件，而非消耗技能點的 ZERO Revenge Counter。原始實體 R3 變身／融合操作會被接管，由卡片內部產生所需指令。

## 變身、融合與隊員切換

按住方向鍵 ↑，按卡片上的 □/△/X/○ 選取；放開 ↑ 關閉。只有同時存在變身和融合時，L1/R1 才切換分類。跳過空分類；僅有融合的角色直接開啟融合。按住方向鍵 ← 切換隊員。夥伴、型態、量表、冷卻和戰鬥狀態要求仍由 BT4 決定。三個選單的卡片位置一致，保留空格並顯示後備生命條。連線時本地面板在左側，即使邏輯上是玩家 2，也不改變輸入對應的玩家。


## HUD / F6 · F7 · F8

F6: attack panel · F7: outer frame · F8: panel placement. Configure shortcuts and shortcut lock in the launcher. Settings: `%LOCALAPPDATA%\BT4-SparkingControls`.

[Compatibility, validation, source and license / English](../../README.md#compatibility-online-and-validation)
