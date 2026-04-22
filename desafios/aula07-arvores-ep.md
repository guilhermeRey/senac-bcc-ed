# 🎓 Bacharelado em Ciência da Computação - Senac

### Estruturas de Dados

## 🧠 Exercício Programa — Nas alturas

**Prof. Guilherme Rey**
📧 guilherme.prey@sp.senac.br

## 📌 Descrição

O objetivo deste exercício é trabalhar com:

- 🌳 Árvores Binárias de Busca (BST)
- ⚖️ Árvores balanceadas (AVL)

Você deverá criar um programa que:

- Gera diversas árvores com números aleatórios
- Calcula e registra suas **alturas**
- Produz um **relatório com médias**

---

## 🚀 Parte 1 — Inserção e média das alturas

Seu programa deve:

1. Receber:
   - `A` → número de amostras
   - `N` → quantidade de nós por árvore

2. Para cada amostra:
   - Criar:
     - Uma **BST comum**
     - Uma **AVL**
   - Gerar `N` números aleatórios **distintos**
   - Inserir os mesmos valores nas duas árvores

3. Registrar:
   - Altura de cada árvore
   - Calcular:
     - Média geral
     - Média BST
     - Média AVL

---

## 💻 Exemplo de execução

```bash
> EP2 - Exemplo de interação
> -------------------------------------------
> Menu: 1) nova simulação ou 2) sair
> _1
> Digite a quantidade de amostras: 2
> Digite a quantidade de elementos para cada amostra: 6
>
> Experimento com A = 2 e N = 6
> ----------------------------------
> Altura média geral:     3.5
> Altura média BST comum: 4
> Altura média AVL:       3
> ----------------------------------
> Menu: 1) nova simulação ou 2) sair
> _2
> Tchau!
```

---

## 🔥 Parte 1.1 — Pra quem gosta mesmo

Além das alturas, calcule também:

⏱️ **Tempo de execução da construção das árvores**

Para isso:

- Meça o tempo de construção:
  - BST comum
  - AVL
- Calcule:
  - Tempo médio por tipo
  - Tempo médio geral

---

## 💻 Exemplo com tempo

```bash
> Experimento com A = 2 e N = 6
> ----------------------------------
> Altura média geral: 3.5
> Tempo médio geral de construção: 123 segundos
> ---
> Altura média BST comum: 4
> Tempo médio de construção BST: 120 segundos
> ---
> Altura média AVL: 3
> Tempo médio de construção AVL: 90 segundos
> ----------------------------------
```
