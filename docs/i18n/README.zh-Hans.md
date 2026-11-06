[English](../../README.md) · [Français](README.fr.md) · [Italiano](README.it.md) · [Deutsch](README.de.md) · [Español (España)](README.es.md) · [العربية](README.ar.md) · [Bahasa Indonesia](README.id.md) · [Español (Latinoamérica)](README.es-419.md) · [Português (Brasil)](README.pt.md) · [Polski](README.pl.md) · [Русский](README.ru.md) · [简体中文](README.zh-Hans.md) · [繁體中文](README.zh-Hant.md) · [日本語](README.ja.md) · [한국어](README.ko.md) · [ไทย](README.th.md)

---

# BT4 Sparking Controls — 0.9.8 Test

0.9.8 更新：唯一数据符号更换编译器分组时，减少误报不兼容。支持已审核的新本地输入路径及原生辅助编码器，继续验证代码、目标地址和歧义名称。保留控制、HUD 和设置，仅在启动时检查。真正的协议变化仍可能需要重新审核。

0.9.7 更新：支持已审核的新原生输入实现，按符号解析地址，分别识别两个参数的寄存器，并保留辅助目标选择字段。检查仅在启动时执行。真正的结构或协议变化仍可能需要重新审核。

0.9.5 更新：倒地时恢复原生按键和方向操作；在支持的空中受击状态下，R1 和 ↑/↓ 请求恢复；验证新版游戏接口；即使兼容性验证失败，也会自动记住最后选择的 EXE/CMD；在新版居中 HUD 中，方块/圆圈跟随三角形的位置和缩放。规则、时机和消耗仍由 BT4 决定，并保留旧版支持。包含 0.9.1–0.9.5 修复；自动测试不能替代真实在线对战。

解压 Windows ZIP，运行 BT4-SparkingControls.exe。选择 BT4decomp.exe 或同一文件夹中的 .cmd，然后保存并启动。0.9.8 Test 包含字体、CMD 启动、对波输入和头像修复。设置会保留。仅支持 Windows。

[Windows ZIP](https://github.com/vini2227/BT4-SparkingControls/releases/download/v0.9.8-test/BT4-SparkingControls-0.9.8-Test-Windows.zip) · [0.9.8 Changes / English](../../CHANGELOG.md) · [Full English guide](../../README.md)

## 操作

□ = 方块；△ = 三角；○ = 圆圈；LS = 左摇杆。LS 箭头表示摇杆方向，不是十字键。普通菜单导航保留游戏原始操作。

| 按键／组合 | 动作 |
| --- | --- |
| `□` | Rush 连击 |
| `△` | 气弹 |
| `X` | 原生移动与冲刺 |
| `R2` | 蓄气 |
| `L1` | 上升 |
| `L2` | 下降 |
| `R2 + L1` | 向上垂直 Z Dash |
| `R2 + L2` | 向下垂直 Z Dash |
| `R1` | 自身受支持连段之外的防御 |
| `R2 + □` | 超级攻击 1 |
| `R2 + △` | 超级攻击 2 |
| `R2 + ○` | 究极攻击，仍需满足原生条件 |
| `R2 + D-pad ↑ / ↓` | 技能 1 |
| `R2 + D-pad ← / →` | 技能 2 |
| `○` | 原生反击架势 |
| `R1 (combo)` | 受支持连段中的原生向前瞬移 |
| `R1 + LS (combo)` | 这些连段中的原生方向瞬移 |
| `R1 + □` | 普通抓投 |
| `R1 + LS↓ + □` | 替代倒地抓投，仅限摇杆 |
| `LS↑ + △` | 原始指令倒地抓投 |
| `R3` | Emergency Blaster Wave，须满足原生条件 |
| `L3` | 搜索／锁定目标 |
| `Select / View` | 切换镜头 |
| `D-pad ↑` | 按住：变身／融合 |
| `D-pad ←` | 按住：切换队员 |
| `D-pad ↓ / → (no R2)` | 战斗中禁用以防误触 |

已按住 R2 时也能选择特殊攻击。要继续蓄力，请保持对应攻击键；只松开 R2 不会松开攻击键。出现提示时，用 □ + 指定方向输入原生 boost。覆盖图标只遮住三角，不挡手部图案。技能快捷键属于本辅助程序，不声称与 ZERO 完全相同。

○ 请求 BT4 原生反击架势，在受支持连段中优先于持续按住的攻击，不是完整的 ZERO Perception/Super Perception 系统。R1 瞬移仍需满足原生气力与取消窗口，不保证移到敌人身后或原地停留。R1 + □ 优先生成 X—松开—X，不能绕过距离和动作窗口；原始双击 X 抓投保留。倒地抓投不接受十字键 ↓；LS↑ + ○ 不再触发倒地抓投。R3 使用 BT4 的生命消耗和触发条件，不是消耗技能点的 ZERO Revenge Counter。原始物理 R3 变身／融合操作会被接管，由卡片内部生成所需指令。

## 变身、融合与队员切换

按住十字键 ↑，按卡片所示 □/△/X/○ 选择；松开 ↑ 关闭。只有同时存在变身和融合时，L1/R1 才切换分类。跳过空分类；仅有融合的角色直接进入融合。按住十字键 ← 选择队员。伙伴、形态、量表、冷却和战斗状态要求仍由 BT4 决定。三个菜单卡片位置一致，保留空位并显示后备生命条。联机本地面板始终在左侧，即使逻辑上是玩家 2，也不改变输入对应的玩家。


## HUD / F6 · F7 · F8

F6: attack panel · F7: outer frame · F8: panel placement. Configure shortcuts and shortcut lock in the launcher. Settings: `%LOCALAPPDATA%\BT4-SparkingControls`.

[Compatibility, validation, source and license / English](../../README.md#compatibility-online-and-validation)
