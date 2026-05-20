# 🎓 Bacharelado em Ciência da Computação - Senac

### Estruturas de Dados

## 🧠 Exercício Programa — Conta Palavras

**Prof. Guilherme Rey**
📧 guilherme.prey@sp.senac.br

## 📌 Descrição

O objetivo deste exercício é trabalhar com:

- #️⃣ Tabelas Hash usando `Dictionary<TKey, TValue>` do C#
- 📊 Contagem de frequência de palavras

Você deverá criar um programa que:

- Recebe um texto digitado pelo usuário
- Conta a frequência de cada palavra usando um `Dictionary`
- Exibe as palavras mais frequentes

---

## 🚀 Parte 1 — Contador de frequência

Seu programa deve:

1. Receber um texto do usuário (várias linhas, terminando quando o usuário digitar uma linha vazia)
2. **Normalizar** o texto:
   - Converter para minúsculas
   - Remover pontuação (`.`, `,`, `!`, `?`, `:`, `;`, etc.)
3. Usar um `Dictionary<string, int>` para contar as ocorrências de cada palavra
4. Exibir:
   - Total de palavras lidas
   - Total de palavras **distintas**
   - As **10 palavras mais frequentes** com suas contagens

---

## 💻 Exemplo de execução

```bash
> Digite o texto (linha vazia para encerrar):
> o rato roeu a roupa do rei de roma
> a rainha com raiva rasgou o resto
>
> === Resultado ===
> Total de palavras: 17
> Palavras distintas: 15
>
> Top 10 palavras mais frequentes:
>   1. "o"      - 2 ocorrencias
>   2. "a"      - 2 ocorrencias
>   3. "rato"   - 1 ocorrencia
>   4. "roeu"   - 1 ocorrencia
>   5. "roupa"  - 1 ocorrencia
>   6. "do"     - 1 ocorrencia
>   7. "rei"    - 1 ocorrencia
>   8. "de"     - 1 ocorrencia
>   9. "roma"   - 1 ocorrencia
>  10. "rainha" - 1 ocorrencia
```

---

## 🔥 Parte 1.1 — Pra quem gosta mesmo

Além da contagem de palavras, implemente um **menu interativo** com as seguintes opções:

1. **Novo texto** — digitar outro texto e recalcular
2. **Buscar palavra** — o usuário digita uma palavra e o programa diz quantas vezes ela aparece
3. **Comparar textos** — o programa guarda os resultados dos textos anteriores e mostra quais palavras aparecem em ambos (interseção) usando um `HashSet<string>`
4. **Sair**

```bash
> Menu: 1) novo texto  2) buscar palavra  3) comparar textos  4) sair
> _1
> Digite o texto (linha vazia para encerrar):
> to be or not to be
>
> Total de palavras: 6
> Palavras distintas: 4
>   1. "to" - 2 ocorrencias
>   2. "be" - 2 ocorrencias
>   3. "or" - 1 ocorrencia
>   4. "not" - 1 ocorrencia
>
> Menu: 1) novo texto  2) buscar palavra  3) comparar textos  4) sair
> _2
> Qual palavra? be
> "be" aparece 2 vezes
>
> Menu: 1) novo texto  2) buscar palavra  3) comparar textos  4) sair
> _4
> Tchau!
```
