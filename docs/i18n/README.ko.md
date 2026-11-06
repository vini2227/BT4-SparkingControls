[English](../../README.md) · [Français](README.fr.md) · [Italiano](README.it.md) · [Deutsch](README.de.md) · [Español (España)](README.es.md) · [العربية](README.ar.md) · [Bahasa Indonesia](README.id.md) · [Español (Latinoamérica)](README.es-419.md) · [Português (Brasil)](README.pt.md) · [Polski](README.pl.md) · [Русский](README.ru.md) · [简体中文](README.zh-Hans.md) · [繁體中文](README.zh-Hant.md) · [日本語](README.ja.md) · [한국어](README.ko.md) · [ไทย](README.th.md)

---

# BT4 Sparking Controls — 0.9.8 Test

0.9.8 변경: 고유 데이터 심벌의 컴파일러 그룹 변경에 따른 잘못된 호환성 차단을 줄였습니다. 검토된 새 로컬 입력 경로와 기본 보조 인코더를 지원합니다. 코드, 참조 대상과 모호한 이름은 계속 검증합니다. 조작, HUD와 설정을 유지하며 검사는 시작 시에만 수행합니다. 실제 프로토콜 변경은 추가 검토가 필요할 수 있습니다.

0.9.7 변경: 검토된 새 기본 입력 구현 지원, 심벌 기반 주소 확인, 두 인수의 레지스터를 개별 인식하고 대상 선택 보조 필드 유지. 검사는 시작 시에만 수행합니다. 실제 구조나 프로토콜 변경은 추가 검토가 필요할 수 있습니다.

0.9.5 변경: 지상 회복에 기본 버튼과 방향 입력을 다시 사용할 수 있습니다. 지원되는 공중 피격 상태에서는 R1과 ↑/↓로 회복을 요청합니다. 새 게임 인터페이스를 검증하며, 호환성 검사 실패 시에도 마지막 EXE/CMD를 자동 저장합니다. 새 중앙 HUD에서 네모/동그라미가 세모의 위치와 크기를 따릅니다. 규칙, 시간, 비용은 BT4가 결정하며 이전 버전도 지원합니다. 0.9.1–0.9.5 수정 포함. 자동 테스트는 실제 온라인 대전 확인을 대신하지 않습니다.

Windows ZIP을 풀고 BT4-SparkingControls.exe를 실행하세요. BT4decomp.exe 또는 같은 폴더의 .cmd를 선택한 후 저장하고 시작하세요. 0.9.8 Test에는 글꼴, CMD 실행, 에너지 충돌 입력, 초상화 수정이 포함됩니다. 설정은 유지됩니다. Windows 전용입니다.

[Windows ZIP](https://github.com/vini2227/BT4-SparkingControls/releases/download/v0.9.8-test/BT4-SparkingControls-0.9.8-Test-Windows.zip) · [0.9.8 Changes / English](../../CHANGELOG.md) · [Full English guide](../../README.md)

## 조작

□ = 네모, △ = 세모, ○ = 동그라미, LS = 왼쪽 스틱. LS 화살표는 방향 패드가 아닌 스틱 방향입니다. 일반 메뉴 탐색은 원래 조작을 유지합니다.

| 버튼 / 조합 | 동작 |
| --- | --- |
| `□` | 러시 공격 |
| `△` | 기탄 |
| `X` | 기존 이동 및 대시 |
| `R2` | 기 모으기 |
| `L1` | 상승 |
| `L2` | 하강 |
| `R2 + L1` | 위쪽 수직 Z Dash |
| `R2 + L2` | 아래쪽 수직 Z Dash |
| `R1` | 지원되는 자신의 콤보 이외의 가드 |
| `R2 + □` | 슈퍼 공격 1 |
| `R2 + △` | 슈퍼 공격 2 |
| `R2 + ○` | 원래 조건에 따른 궁극기 |
| `R2 + D-pad ↑ / ↓` | 스킬 1 |
| `R2 + D-pad ← / →` | 스킬 2 |
| `○` | 기존 반격 자세 |
| `R1 (combo)` | 지원되는 콤보 중 기존 전방 순간이동 |
| `R1 + LS (combo)` | 해당 콤보 중 방향 순간이동 |
| `R1 + □` | 일반 잡기 |
| `R1 + LS↓ + □` | 대체 다운 잡기, 스틱만 사용 |
| `LS↑ + △` | 원래 입력의 다운 잡기 |
| `R3` | 원래 조건 충족 시 Emergency Blaster Wave |
| `L3` | 적 탐색 / 록온 |
| `Select / View` | 카메라 변경 |
| `D-pad ↑` | 길게: 변신/합체 |
| `D-pad ←` | 길게: 팀원 교대 |
| `D-pad ↓ / → (no R2)` | 실수 방지를 위해 전투 중 차단 |

R2를 이미 누른 상태에서도 특수기를 선택할 수 있습니다. 충전을 계속하려면 지정된 공격 버튼을 유지하세요. R2만 놓아도 공격 버튼은 해제되지 않습니다. 부스트 안내가 나오면 □ + 지정 방향을 사용합니다. 덧씌운 아이콘은 세모만 가리고 손 그림은 보존합니다. 스킬 단축 조작은 ZERO의 정확한 복제가 아닙니다.

○는 지원 콤보에서 유지 중인 공격보다 우선하여 BT4 반격 자세를 요청하며 ZERO의 전체 Perception/Super Perception을 추가하지 않습니다. R1 순간이동은 원래 기 요구량과 캔슬 가능 시간에 따르며 상대 뒤 이동이나 제자리 이동을 보장하지 않습니다. R1 + □는 우선적으로 X–해제–X를 생성하지만 사거리와 허용 시간이 필요합니다. 기존 X 두 번 잡기는 유지됩니다. 다운 잡기는 방향 패드 ↓를 사용하지 않으며 LS↑ + ○로도 발동하지 않습니다. R3는 BT4 체력 소모와 조건을 사용하며 ZERO의 스킬 카운트 기반 Revenge Counter와 다릅니다. 기존 물리 R3 변신/합체 입력은 차단되고 카드가 필요한 입력을 내부 생성합니다.

## 변신, 합체 및 교대

방향 패드 ↑를 누른 채 카드에 표시된 □/△/X/○를 선택하고 ↑를 놓으면 닫습니다. 변신과 합체가 둘 다 있을 때만 L1/R1로 분류를 전환합니다. 빈 분류는 건너뛰며 합체만 있으면 바로 합체를 엽니다. 방향 패드 ←를 눌러 팀원을 선택합니다. 파트너, 형태, 게이지, 재사용 대기시간과 전투 상태 조건은 BT4를 따릅니다. 카드 위치를 통일하고 빈 칸과 대기 팀원 체력을 표시합니다. 온라인에서는 플레이어 2라도 로컬 패널이 왼쪽에 나오지만 논리적 입력 측은 바뀌지 않습니다.


## HUD / F6 · F7 · F8

F6: attack panel · F7: outer frame · F8: panel placement. Configure shortcuts and shortcut lock in the launcher. Settings: `%LOCALAPPDATA%\BT4-SparkingControls`.

[Compatibility, validation, source and license / English](../../README.md#compatibility-online-and-validation)
