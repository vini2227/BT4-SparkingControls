[English](../../README.md) · [Français](README.fr.md) · [Italiano](README.it.md) · [Deutsch](README.de.md) · [Español (España)](README.es.md) · [العربية](README.ar.md) · [Bahasa Indonesia](README.id.md) · [Español (Latinoamérica)](README.es-419.md) · [Português (Brasil)](README.pt.md) · [Polski](README.pl.md) · [Русский](README.ru.md) · [简体中文](README.zh-Hans.md) · [繁體中文](README.zh-Hant.md) · [日本語](README.ja.md) · [한국어](README.ko.md) · [ไทย](README.th.md)

---

# BT4 Sparking Controls — 0.9.8 Test

0.9.8 の変更：一意のデータシンボルのコンパイラグループ変更による誤った非対応判定を減らしました。検証済みの新しいローカル入力経路とネイティブ補助エンコーダに対応します。コード、参照先、曖昧な名前は引き続き検証します。操作、HUD、設定は保持し、確認は起動時のみです。実際のプロトコル変更は再確認が必要な場合があります。

0.9.7 の変更：検証済みの新しい入力実装に対応し、アドレスをシンボルで解決します。2つの引数のレジスタを個別に認識し、ターゲット選択用の補助フィールドを保持します。確認は起動時のみです。実際の構造・プロトコル変更は引き続き確認が必要な場合があります。

0.9.5 の変更：地上復帰で元のボタンと方向入力を使用できます。対応する空中の被弾状態では R1 と ↑/↓ で復帰を要求します。新しいゲーム関数を検証し、互換性確認に失敗しても最後の EXE/CMD を自動保存します。新しい中央 HUD では四角／丸が三角の位置と倍率に追従します。規則、時間、消費は BT4 のままで、旧版にも対応します。0.9.1～0.9.5 の修正を含みます。自動テストは実際のオンライン対戦の確認に代わるものではありません。

Windows ZIP を展開し、BT4-SparkingControls.exe を起動します。BT4decomp.exe または同じフォルダーの .cmd を選択し、保存して起動してください。0.9.8 Test にはフォント、CMD 起動、撃ち合い入力、ポートレートの修正が含まれます。設定は保持されます。Windows 専用です。

[Windows ZIP](https://github.com/vini2227/BT4-SparkingControls/releases/download/v0.9.8-test/BT4-SparkingControls-0.9.8-Test-Windows.zip) · [0.9.8 Changes / English](../../CHANGELOG.md) · [Full English guide](../../README.md)

## 操作

□＝四角、△＝三角、○＝丸、LS＝左スティック。LS の矢印は十字キーではなくスティック方向です。通常のメニュー操作は元のままです。

| ボタン／組み合わせ | 動作 |
| --- | --- |
| `□` | ラッシュ攻撃 |
| `△` | 気弾 |
| `X` | 元の移動・ダッシュ |
| `R2` | 気力をためる |
| `L1` | 上昇 |
| `L2` | 下降 |
| `R2 + L1` | 上方向の垂直 Z Dash |
| `R2 + L2` | 下方向の垂直 Z Dash |
| `R1` | 対応コンボ中以外のガード |
| `R2 + □` | 必殺技 1 |
| `R2 + △` | 必殺技 2 |
| `R2 + ○` | 究極技、元の発動条件に従う |
| `R2 + D-pad ↑ / ↓` | スキル 1 |
| `R2 + D-pad ← / →` | スキル 2 |
| `○` | 元のカウンター構え |
| `R1 (combo)` | 対応コンボ中の前方瞬間移動 |
| `R1 + LS (combo)` | 同コンボ中の方向指定瞬間移動 |
| `R1 + □` | 通常のつかみ |
| `R1 + LS↓ + □` | 代替ダウンつかみ、スティックのみ |
| `LS↑ + △` | 元の入力によるダウンつかみ |
| `R3` | Emergency Blaster Wave、元の条件に従う |
| `L3` | 敵を探す／ロックオン |
| `Select / View` | カメラ切り替え |
| `D-pad ↑` | 長押し：変身／合体 |
| `D-pad ←` | 長押し：仲間の交代 |
| `D-pad ↓ / → (no R2)` | 誤操作防止のため戦闘中は無効 |

R2 を押したままでも必殺技を選べます。ためを続けるには割り当てられた攻撃ボタンを押し続けます。R2 だけを離しても攻撃ボタンは離されません。ブースト指示では □＋指定方向を入力します。表示は元の三角だけを置き換え、手の絵を隠しません。スキルのショートカットは ZERO の完全再現ではありません。

○ は対応コンボ中に押し続けた攻撃より優先して BT4 のカウンター構えを要求します。ZERO の Perception/Super Perception 全体を追加するものではありません。R1 の瞬間移動は元の気力条件とキャンセル可能時間に従い、敵の背後やその場での移動を保証しません。R1＋□ は優先して X→離す→X を生成しますが、射程や受付時間は必要です。元の X 2 回入力も残ります。ダウンつかみに十字キー↓は使えず、LS↑＋○でも発動しません。R3 は BT4 の体力消費と条件を使い、ZERO のスキルカウント式 Revenge Counter とは異なります。元の物理 R3 変身／合体入力は抑制され、カードが必要な入力を内部生成します。

## 変身・合体・交代

十字キー↑を押し、カードに対応する □/△/X/○ を選びます。↑を離すと閉じます。変身と合体の両方がある場合のみ L1/R1 でタブ切り替え。空の分類は飛ばし、合体のみなら直接開きます。十字キー←を押して仲間を選択。相方、形態、ゲージ、クールダウン、戦闘状態など BT4 の条件は維持されます。各メニューのカード位置は共通で、空き枠と控えの体力バーも表示。オンラインのローカル HUD はプレイヤー 2 でも左側ですが、論理的な入力側は変えません。


## HUD / F6 · F7 · F8

F6: attack panel · F7: outer frame · F8: panel placement. Configure shortcuts and shortcut lock in the launcher. Settings: `%LOCALAPPDATA%\BT4-SparkingControls`.

[Compatibility, validation, source and license / English](../../README.md#compatibility-online-and-validation)
