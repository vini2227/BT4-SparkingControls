[English](../../README.md) · [Français](README.fr.md) · [Italiano](README.it.md) · [Deutsch](README.de.md) · [Español (España)](README.es.md) · [العربية](README.ar.md) · [Bahasa Indonesia](README.id.md) · [Español (Latinoamérica)](README.es-419.md) · [Português (Brasil)](README.pt.md) · [Polski](README.pl.md) · [Русский](README.ru.md) · [简体中文](README.zh-Hans.md) · [繁體中文](README.zh-Hant.md) · [日本語](README.ja.md) · [한국어](README.ko.md) · [ไทย](README.th.md)

---

# BT4 Sparking Controls — 0.9.7 Test

Novedades 0.9.7: nuevas variantes nativas verificadas, direcciones resueltas mediante símbolos, registros de ambos argumentos reconocidos por separado y campos auxiliares de objetivo conservados. Verificación solo al iniciar. Los cambios reales de estructura o protocolo aún pueden requerir revisión.

Novedades 0.9.5: recuperación en el suelo con botones y direcciones nativos; R1 y ↑/↓ solicitan recuperación aérea en reacciones compatibles; nuevas interfaces del juego verificadas; último EXE/CMD guardado automáticamente incluso si falla la validación; cuadrado/círculo sigue la posición y escala del triángulo en el HUD centrado nuevo. BT4 conserva sus reglas, tiempos y costos. La versión anterior sigue admitida. Incluye 0.9.1–0.9.5; las pruebas automáticas no sustituyen una partida online real.

Extrae el ZIP Windows y abre BT4-SparkingControls.exe. Selecciona BT4decomp.exe o un archivo .cmd en la misma carpeta; luego guarda e inicia. La versión 0.9.7 Test corrige fuentes, inicio CMD, choques de energía y retratos. Los ajustes se conservan. Solo Windows.

[Windows ZIP](https://github.com/vini2227/BT4-SparkingControls/releases/download/v0.9.7-test/BT4-SparkingControls-0.9.7-Test-Windows.zip) · [0.9.7 Changes / English](../../CHANGELOG.md) · [Full English guide](../../README.md)

## Controles

□ = Cuadrado; △ = Triángulo; ○ = Círculo; LS = stick izquierdo. LS con flechas se refiere al stick, no a la cruz direccional. La navegación de los menús normales conserva los controles originales.

| Botón / combinación | Acción |
| --- | --- |
| `□` | Ataques Rush |
| `△` | Ráfagas de Ki |
| `X` | Movimiento y desplazamientos nativos |
| `R2` | Cargar Ki |
| `L1` | Ascender |
| `L2` | Descender |
| `R2 + L1` | Z Dash vertical hacia arriba |
| `R2 + L2` | Z Dash vertical hacia abajo |
| `R1` | Guardia fuera de tus combos compatibles |
| `R2 + □` | Superataque 1 |
| `R2 + △` | Superataque 2 |
| `R2 + ○` | Ataque definitivo, sujeto a requisitos nativos |
| `R2 + D-pad ↑ / ↓` | Habilidad 1 |
| `R2 + D-pad ← / →` | Habilidad 2 |
| `○` | Postura de contraataque nativa |
| `R1 (combo)` | Teletransporte frontal nativo durante combos compatibles |
| `R1 + LS (combo)` | Teletransporte direccional nativo durante esos combos |
| `R1 + □` | Agarre normal |
| `R1 + LS↓ + □` | Agarre en el suelo alternativo, solo stick |
| `LS↑ + △` | Agarre en el suelo con el cocontrol original |
| `R3` | Emergency Blaster Wave, sujeto a requisitos nativos |
| `L3` | Buscar / fijar objetivo |
| `Select / View` | Cambiar cámara |
| `D-pad ↑` | Mantener: transformaciones/fusiones |
| `D-pad ←` | Mantener: cambio de compañero |
| `D-pad ↓ / → (no R2)` | Bloqueados en combate para evitar acciones accidentales |

Puedes seleccionar ataques mientras ya mantienes R2. Para seguir cargando un especial, mantén su botón de ataque; soltar solo R2 no lo suelta. Cuando se indique, □ + dirección solicitada envía el boost original. La superposición tapa solo el Triángulo y mantiene visible la mano. Los atajos de habilidades no son una reproducción exacta de ZERO.

○ solicita la postura nativa de BT4 y tiene prioridad sobre los ataques mantenidos durante combos compatibles; no añade todo Perception/Super Perception de ZERO. Los teletransportes de R1 conservan costos y ventanas nativos: no garantizan aparecer detrás ni permanecer inmóvil. R1 + □ tiene prioridad y genera X–soltar–X, según alcance y ventana válida. Se conserva el doble X original. El agarre en el suelo no acepta cruz direccional ↓; LS↑ + ○ no lo activa. R3 usa condiciones y coste de vida de BT4, no el Revenge Counter de ZERO basado en puntos de habilidad. Los antiguos atajos físicos R3 de transformación/fusión se consumen; las tarjetas generan sus entradas.

## Transformaciones, fusión y cambio

Mantén cruz direccional ↑ y pulsa □/△/X/○ según la tarjeta; suelta ↑ para cerrar. L1/R1 cambian entre formas y fusiones solo si existen ambas. Se omiten categorías vacías; los personajes con solo fusión entran directamente en ella. Mantén cruz direccional ← para cambiar compañero. Se mantienen los requisitos de compañero, forma, barras, recarga y estado del combate. Los tres menús usan posiciones fijas, huecos vacíos y barras de vida de reserva. Online, los paneles locales aparecen a la izquierda incluso como Jugador 2, sin cambiar el lado lógico de las entradas.


## HUD / F6 · F7 · F8

F6: attack panel · F7: outer frame · F8: panel placement. Configure shortcuts and shortcut lock in the launcher. Settings: `%LOCALAPPDATA%\BT4-SparkingControls`.

[Compatibility, validation, source and license / English](../../README.md#compatibility-online-and-validation)
