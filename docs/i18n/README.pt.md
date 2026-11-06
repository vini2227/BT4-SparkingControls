[English](../../README.md) · [Français](README.fr.md) · [Italiano](README.it.md) · [Deutsch](README.de.md) · [Español (España)](README.es.md) · [العربية](README.ar.md) · [Bahasa Indonesia](README.id.md) · [Español (Latinoamérica)](README.es-419.md) · [Português (Brasil)](README.pt.md) · [Polski](README.pl.md) · [Русский](README.ru.md) · [简体中文](README.zh-Hans.md) · [繁體中文](README.zh-Hant.md) · [日本語](README.ja.md) · [한국어](README.ko.md) · [ไทย](README.th.md)

---

# BT4 Sparking Controls — 0.9.8 Test

Novidades da 0.9.8: reduz falsos bloqueios após atualizações ao reconhecer símbolos de dados únicos que mudaram de grupo no compilador. Suporta o novo coletor local e o codificador auxiliar nativo revisados. Código, destinos e nomes duplicados continuam verificados. Controles, HUD e configurações preservados; análise apenas na abertura. Mudanças reais no protocolo ainda podem exigir revisão.

Novidades da 0.9.7: compatibilidade com os coletores nativos revisados mais recentes; endereços resolvidos pelos símbolos; reconhecimento independente dos registradores dos dois argumentos; preservação dos campos auxiliares de alvo. A checagem ocorre somente na abertura. Alterações reais de estrutura ou protocolo ainda podem exigir revisão.

Novidades da 0.9.5: recuperação no chão com botões e direções nativos; R1 e ↑/↓ para solicitar recuperação aérea nas situações compatíveis; reconhecimento das novas funções revisadas do jogo; último EXE/CMD salvo automaticamente, mesmo se a validação falhar; quadrado/círculo acompanhando a escala e a posição do triângulo no HUD Centralizado da versão nova. As regras, tempos e custos continuam sendo os do BT4. A versão anterior permanece aceita. Inclui as correções da 0.9.1 à 0.9.5. Os testes automatizados não substituem a confirmação em partida online real.

Extraia o ZIP Windows e execute BT4-SparkingControls.exe. Em Procurar, selecione BT4decomp.exe ou um arquivo .cmd na mesma pasta do jogo; depois clique em Salvar e iniciar. A versão 0.9.8 Test inclui as correções de fonte, CMD, disputa de energia e retratos. As configurações são preservadas. Este pacote é somente para Windows.

[Windows ZIP](https://github.com/vini2227/BT4-SparkingControls/releases/download/v0.9.8-test/BT4-SparkingControls-0.9.8-Test-Windows.zip) · [0.9.8 Changes / English](../../CHANGELOG.md) · [Full English guide](../../README.md)

## Controles

□ = quadrado; △ = triângulo; ○ = círculo; LS = analógico esquerdo. As setas em LS indicam o analógico, não o D-pad. A navegação normal dos menus mantém os controles originais.

| Botão / combinação | Ação |
| --- | --- |
| `□` | Ataques Rush |
| `△` | Disparos de Ki |
| `X` | Movimentação e dash nativos |
| `R2` | Carregar Ki |
| `L1` | Subir |
| `L2` | Descer |
| `R2 + L1` | Z Dash vertical para cima |
| `R2 + L2` | Z Dash vertical para baixo |
| `R1` | Defender fora dos próprios combos compatíveis |
| `R2 + □` | Superataque 1 |
| `R2 + △` | Superataque 2 |
| `R2 + ○` | Ataque supremo; exige as condições nativas |
| `R2 + D-pad ↑ / ↓` | Habilidade 1 |
| `R2 + D-pad ← / →` | Habilidade 2 |
| `○` | Postura de contra-ataque nativa |
| `R1 (combo)` | Teleporte frontal nativo durante combos compatíveis |
| `R1 + LS (combo)` | Teleporte direcional nativo durante esses combos |
| `R1 + □` | Agarrão normal |
| `R1 + LS↓ + □` | Agarrão no chão alternativo; apenas analógico |
| `LS↑ + △` | Agarrão no chão pelo comando original |
| `R3` | Emergency Blaster Wave; exige elegibilidade nativa |
| `L3` | Buscar / fixar alvo |
| `Select / View` | Mudar câmera |
| `D-pad ↑` | Manter: abrir transformações/fusões |
| `D-pad ←` | Manter: abrir troca de personagem |
| `D-pad ↓ / → (no R2)` | Bloqueados na batalha para evitar comandos acidentais |

Os especiais podem ser selecionados com R2 já pressionado. Para continuar carregando um especial, mantenha o botão de ataque correspondente; soltar só R2 não solta esse botão. Quando solicitado, □ + direção indicada envia o boost original. A sobreposição cobre apenas o triângulo e preserva a mão. As habilidades são atalhos deste auxiliar, não uma cópia exata dos controles de ZERO.

○ usa a postura nativa do BT4, com prioridade sobre ataques mantidos em combos compatíveis; não acrescenta todo o sistema Perception/Super Perception de ZERO. R1 usa os teleportes e suas exigências de Ki/janelas nativas; não garante aparecer atrás do inimigo nem ficar parado. R1 + □ tem prioridade sobre essas rotas e gera X–soltar–X: depende de alcance e janela válida. O duplo X original continua disponível. O agarrão no chão não aceita D-pad ↓; LS↑ + ○ não o ativa. R3 usa o custo de vida e condições do BT4, não o Revenge Counter baseado em Skill Count de ZERO. O R3 físico original de transformação/fusão é consumido; os cartões geram os comandos internamente.

## Transformações, fusão e troca

Mantenha D-pad ↑ e pressione o botão □/△/X/○ mostrado no cartão; solte ↑ para fechar. L1/R1 alternam entre transformações e fusões apenas quando ambas existem. Categorias vazias são ignoradas; personagens só com fusão abrem direto nela. Mantenha D-pad ← para trocar e escolha o cartão correspondente. Parceiro, forma, barras, cooldowns e estado da luta continuam exigidos pelo BT4. Os três menus usam posições consistentes, cartões vazios e barras de vida dos reservas. No online, os painéis locais ficam à esquerda mesmo sendo Player 2, sem mudar o lado lógico dos comandos.


## HUD / F6 · F7 · F8

F6: attack panel · F7: outer frame · F8: panel placement. Configure shortcuts and shortcut lock in the launcher. Settings: `%LOCALAPPDATA%\BT4-SparkingControls`.

[Compatibility, validation, source and license / English](../../README.md#compatibility-online-and-validation)
