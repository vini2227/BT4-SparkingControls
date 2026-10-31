[English](README.md) · [Français](docs/i18n/README.fr.md) · [Italiano](docs/i18n/README.it.md) · [Deutsch](docs/i18n/README.de.md) · [Español (España)](docs/i18n/README.es.md) · [العربية](docs/i18n/README.ar.md) · [Bahasa Indonesia](docs/i18n/README.id.md) · [Español (Latinoamérica)](docs/i18n/README.es-419.md) · [Português (Brasil)](docs/i18n/README.pt.md) · [Polski](docs/i18n/README.pl.md) · [Русский](docs/i18n/README.ru.md) · [简体中文](docs/i18n/README.zh-Hans.md) · [繁體中文](docs/i18n/README.zh-Hant.md) · [日本語](docs/i18n/README.ja.md) · [한국어](docs/i18n/README.ko.md) · [ไทย](docs/i18n/README.th.md)

---

<img src="desenvolvimento/assets/dragon-ball-four-stars.png" alt="BT4 Sparking Controls" width="86">

# BT4 Sparking Controls

**0.9 Test — Windows Controls and Setup Guide**

Sparking! ZERO-inspired controls and battle HUD for **BT4 Decomp**. This is the separate BT4 edition of [BT3 Sparking Controls](https://github.com/vini2227/BT3-SparkingControls). Native animations, damage, Ki costs, range, partner requirements and timing still apply. The game and game artwork are not included.

[**Download Windows x64 ZIP**](https://github.com/vini2227/BT4-SparkingControls/releases/download/v0.9-test/BT4-SparkingControls-0.9-Test-Windows.zip) · [**Release notes**](https://github.com/vini2227/BT4-SparkingControls/releases/tag/v0.9-test) · [English TXT](docs/Controls-and-Setup-Guide.txt) · [Changes](CHANGELOG.md)

## 0.9 — Changes since the previous public release

- **R7.1 Hotfix compatibility:** accepts compatible input-collection changes by checking the native 18-byte packet, analog layout, button inversion, successful return, Win64 register/stack preservation and online sampling caller. Equivalent instruction reordering and register allocation are accepted automatically. Other reviewed interfaces and references remain checked.
- **Fast startup-only analysis:** measured around 0.2 seconds for the initial boundary plus full validation in the test environment. No new per-frame analysis or combat polling.
- **Circle-only manual get-up (0.7):** while grounded/downed, press Circle to recover. Circle + direction chooses a native directional get-up. R1 and directions alone do not request manual get-up; standing guard and native automatic recovery remain available. Other local-player menu/state, CPU/scripted input and remote input are preserved.
- **Centered HUD boost-glyph alignment (0.8):** the replacement Square/Circle stays over the native Triangle instead of being centered a second time. Checked against live captured prompt coordinates and four aspect ratios.
- Includes all earlier BT4 improvements: four-star icon/red launcher, native Triangle in beam struggles, names and face-centered portraits, configurable shortcuts, saved preferences, split-screen/ultrawide placement, EXE/CMD translation launch and private in-memory HUD fonts with validated styles/fallback.

The first public release was **0.6 Test**. Local 0.7 and 0.8 improvements are included in this **0.9 Test** download. [Complete version-by-version history](CHANGELOG.md).

**Windows x64 test release. There is no validated native Linux BT4 package.**

## Windows installation

1. Extract the ZIP and run `BT4-SparkingControls.exe`.
2. Click **Browse** and select your `BT4decomp.exe`. For a translation launcher, select its `.cmd` in the **same folder** as `BT4decomp.exe` (for example, `Jogar em Portugues.cmd`). Nested CMD calls, setup steps and language environment variables are preserved.
3. Choose language, panel placement, attack visibility and shortcuts.
4. Click **Save and start**. Launch through the helper to use the modified controls.

The single EXE includes its helper resources. No modloader or game recompilation is required. The helper patches the launched process in memory; it does not replace game files. A selected translation CMD may prepare its own files.

Settings are stored in `%LOCALAPPDATA%\BT4-SparkingControls`, separately from BT3, and remain across updates. Close the helper before replacing its EXE. Keep a copy of your previous EXE if you want to roll back. If the game moves, select its new location.

## Basic controls

- Square: Rush attacks.
- Triangle: Ki blasts.
- X: Native movement and dash actions. Original double-tap X grabs remain.
- R2: Charge Ki.
- L1: Ascend.
- L2: Descend.
- R2 + L1: Vertical Z Dash upward (hold).
- R2 + L2: Vertical Z Dash downward (hold).
- R1: Guard outside your own supported attack combos.
- L3: BT4's original search/lock-on action.
- Select / View: Change camera.

These labels use PlayStation-style button names. Ordinary menu navigation keeps the original game layout.

## Ground recovery

While grounded/downed, **Circle** is the manual get-up button. Hold a direction with Circle for directional recovery. R1 and other buttons or directions alone do not request that manual get-up. R1 still guards while standing. Native automatic get-up remains unchanged. This restriction applies to lying/down states, not every airborne hit reaction.

## Special attacks and skills

- R2 + Square: Super Attack 1.
- R2 + Triangle: Super Attack 2.
- R2 + Circle: Ultimate Attack, subject to native requirements.
- R2 + D-pad Up or Down: Skill 1.
- R2 + D-pad Left or Right: Skill 2.

Special selection works while R2 is already held. These skill shortcuts are this helper's layout, not a claim of exact ZERO controls.

For a chargeable special, keep holding its assigned face button to continue charging. Releasing R2 alone does not release that attack button.

When the game requests a boost input, use Square together with the requested direction. The overlay replaces the old Triangle glyph while preserving the hand graphic.

During native beam/rush struggles, follow the native Triangle prompt. The helper preserves that input and does not replace the clash glyph.

## Counters, vanishes and grabs

Circle: Requests BT4's native counter stance, inspired by ZERO's Perception. During supported Rush/Ki combos, it takes priority over held attack buttons. It does not add ZERO's full Perception or Super Perception systems. Release Circle while retaining R1 to request the end of the counter stance and return to guard, without another R1 press. This waits for BT4's actual stance exit and respects the native animation window, for short or long holds.

R1 during your own supported combos: Requests BT4's native forward vanish. It may move forward slightly. It is not a stationary teleport and does not guarantee appearing behind the opponent.

R1 + left-stick direction during those combos: Requests the corresponding native directional vanish. Native Ki requirements and cancel windows apply.

R1 + Square: Requests a regular grab, with priority over vanish/counter routes. The helper generates the original X-release-X sequence. Native range and action windows still apply; it cannot force an unavailable grab.

R1 + left stick Down + Square: Alternate ground-grab shortcut. Use the left analog stick, not D-pad Down. The original Triangle + left stick Up command also remains available. Circle + left stick Up no longer requests the ground grab.

Original double-tap X grabs remain intentionally available. D-pad Down and Right without R2 are blocked during battle to prevent accidental native commands.

R3: Requests BT4's Emergency Blaster Wave through its native input sequence when eligible. It uses BT4's health cost and requirements, not ZERO's Skill Count-based Revenge Counter system. Original physical R3 transformation/fusion shortcuts are consumed during battle; the card menus generate the required native inputs internally.

## Transformations, fusion and teammate switching

Hold D-pad Up: Open transformation/fusion cards. Press Square, Triangle, X or Circle to select the matching card. Release D-pad Up to close the panel.

L1 / R1: Switch between transformations and fusion when both are available. Empty categories are skipped. Fusion-only characters open directly on fusion.

Hold D-pad Left: Open teammate-switching cards. Press the face button shown on the desired teammate card.

Fusion, transformation and switching retain BT4's native partner, form, gauge, cooldown and battle-state requirements. A visible card does not bypass those requirements.

All three menus use consistent card positions. Switching cards show reserve health bars; missing characters remain as empty card placeholders. Online local panels appear on the left HUD side even when you join as Player 2. This visual placement does not change the logical input side.

## HUD and customization

Hold R2: Display the active character's attack list, commands and costs. Available HD game button artwork is preferred; original assets are the fallback. No game artwork or HD texture pack is bundled.

Panel placement options: - Centered: A balance between the center and edges. - Expanded: More space between the panels. - Classic (4:3): Original positioning within a 4:3 region. - Screen edges: Position menus near the outer picture edges, including solo ultrawide.

Native 4:3 positioning is preserved. Centered, Expanded and Classic use their established safe regions. Screen edges is an optional layout for outer-edge positioning.

All four battle menus fade in over 100 ms and out over 80 ms. Closing retains only the last visual snapshot until it becomes transparent. Reopening reverses smoothly. No command waits for the fade. Losing game focus clears immediately.

Show attack panel: Enable or hide the attack list. Attack panel outer frame: Optional; off by default for fresh settings. Existing saved choices are preserved during updates.

## Default keyboard shortcuts

- F6: Show or hide the attack panel.
- F7: Toggle the attack panel outer frame.
- F8: Cycle panel placement.

Click a shortcut button in the launcher to assign another key or combination, clear the binding or restore its default. Duplicate bindings are rejected. Shortcuts require game focus and do not repeat while the key is held. Enable Lock shortcuts during battle to prevent in-game changes.

Game path, language, placement, attack visibility, frame, shortcut bindings and shortcut lock are saved. HUD changes made during gameplay are also saved and restored the next time you open the app.

The launcher includes 16 interface languages and Arabic right-to-left layout. Move and character names still come from your game's assets and language.

## Compatibility, online and validation

The helper translates the local controller packet before network sampling. The peer receives native inputs; remote input and native combat decision functions are not patched. Online local menus use the left side even when joining as P2. This does not change your logical input side.

Startup and relaunch checks validate the reviewed native interfaces and typed references. Compatible address-only changes and supported input-collector implementation changes can be accepted automatically. The bounded input contract checks the packet rather than pinning the whole collector body. Missing symbols or structural changes outside the supported contracts may still need a helper update. Universal future-version compatibility and desync-free operation are not guaranteed. BT3 Char Package/Tag Team support is not a claim of support for arbitrary BT4 forks.

Validated: 1,541,133 native packet/ABI/command checks; 1,408 transformation/fusion/switch layout checks; portrait loading/names and projection at four aspect ratios; saved preferences; isolated direct/nested CMD launch before first game instruction; 568 current/previous image and corrupted jump-table checks; 200 font draw/measure cycles after file replacement/deletion and invalid/missing font fallback. The R7.1 input boundary passed 155 focused acceptance/rejection checks, including equivalent axis-load reordering and register allocation. Relocation/corruption checks, a suspended-process hook probe, CMD launch and settings checks passed. The intermittent font dialog itself did not reproduce in the font validation run. Automated checks are not physical-controller, sustained-FPS or live online/rollback tests.

## Credits, source and license

Special thanks to **Aero** for extensive testing, detailed reproduction reports and diagnostic logs that guided the performance work inherited from the BT3 project.

Source and [AGPL-3.0 license](LICENSE-AGPL-3.0.txt) are included. [Windows build instructions](desenvolvimento/BUILD.md). Game-derived resource layouts are credited in the source; game artwork is read at runtime from the user's installation and is not distributed. Bundled fonts retain their license in `desenvolvimento/assets`. Unofficial fan project; trademarks belong to their owners.
