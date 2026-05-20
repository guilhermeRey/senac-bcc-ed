# 🎓 Bacharelado em Ciência da Computação - Senac

### Estruturas de Dados

## 🧠 Exercício Programa — Bora Viajar

**Prof. Guilherme Rey**
📧 guilherme.prey@sp.senac.br

## 📌 Descrição

O objetivo deste exercício é trabalhar com:

- 🗺️ Grafos usando `Dictionary<string, List<string>>`
- 🔍 Busca em Profundidade (DFS) — recursiva
- 🌊 Busca em Largura (BFS) — com fila
- 🧭 Encontrar caminhos entre vértices

Você deverá criar um programa que:

- Modela um mapa de cidades brasileiras conectadas por estradas
- Permite consultar conexões diretas entre cidades
- Encontra se existe rota entre duas cidades (DFS)
- Encontra a rota com menos paradas entre duas cidades (BFS)

---

## 🚀 Parte 1 — Mapa de rotas

Seu programa deve:

1. Criar um grafo usando `Dictionary<string, List<string>>` com as seguintes cidades e conexões:

   ```
   São Paulo  — Rio de Janeiro, Curitiba, Belo Horizonte
   Rio de Janeiro — São Paulo, Belo Horizonte, Vitória
   Belo Horizonte — São Paulo, Rio de Janeiro, Brasília
   Curitiba — São Paulo, Florianópolis
   Florianópolis — Curitiba, Porto Alegre
   Porto Alegre — Florianópolis
   Brasília — Belo Horizonte, Goiânia
   Goiânia — Brasília
   Vitória — Rio de Janeiro
   Salvador — Recife
   Recife — Salvador, Fortaleza
   Fortaleza — Recife
   ```

   > Note que Salvador, Recife e Fortaleza formam um grupo **desconexo** do restante — não há estrada ligando eles às cidades do Sul/Sudeste. Isso é proposital!

2. Exibir um **menu interativo** com as opções:

   1. **Listar cidades** — mostra todas as cidades e suas conexões diretas
   2. **Verificar conexão direta** — o usuário digita duas cidades e o programa diz se existe estrada direta entre elas
   3. **Existe rota?** (DFS) — o usuário digita origem e destino, e o programa usa DFS para dizer se existe alguma rota (mesmo com escalas) entre as duas cidades
   4. **Menor rota** (BFS) — o usuário digita origem e destino, e o programa usa BFS para encontrar a rota com o **menor número de paradas**, exibindo o caminho completo
   5. **Sair**

3. Para a opção 3 (DFS), exiba a **ordem de visitação** durante a busca

4. Para a opção 4 (BFS), exiba o **caminho completo** e o **número de paradas**

> **Dica:** para reconstruir o caminho na BFS, use um `Dictionary<string, string>` para guardar de onde cada cidade foi alcançada (o "pai" de cada vértice na busca).

---

## 💻 Exemplo de execução

```bash
> === Bora Viajar! ===
> Menu: 1) listar cidades  2) conexão direta  3) existe rota? (DFS)  4) menor rota (BFS)  5) sair
> _1
>
> Cidades e conexões:
>   São Paulo: [Rio de Janeiro, Curitiba, Belo Horizonte]
>   Rio de Janeiro: [São Paulo, Belo Horizonte, Vitória]
>   Belo Horizonte: [São Paulo, Rio de Janeiro, Brasília]
>   Curitiba: [São Paulo, Florianópolis]
>   Florianópolis: [Curitiba, Porto Alegre]
>   Porto Alegre: [Florianópolis]
>   Brasília: [Belo Horizonte, Goiânia]
>   Goiânia: [Brasília]
>   Vitória: [Rio de Janeiro]
>   Salvador: [Recife]
>   Recife: [Salvador, Fortaleza]
>   Fortaleza: [Recife]
>
> Menu: 1) listar cidades  2) conexão direta  3) existe rota? (DFS)  4) menor rota (BFS)  5) sair
> _2
> Cidade 1: São Paulo
> Cidade 2: Curitiba
> São Paulo e Curitiba possuem conexão direta!
>
> Menu: 1) listar cidades  2) conexão direta  3) existe rota? (DFS)  4) menor rota (BFS)  5) sair
> _2
> Cidade 1: São Paulo
> Cidade 2: Porto Alegre
> São Paulo e Porto Alegre NÃO possuem conexão direta.
>
> Menu: 1) listar cidades  2) conexão direta  3) existe rota? (DFS)  4) menor rota (BFS)  5) sair
> _3
> Origem: São Paulo
> Destino: Goiânia
> DFS visitando: São Paulo -> Rio de Janeiro -> Belo Horizonte -> Brasília -> Goiânia
> Rota encontrada! É possível ir de São Paulo até Goiânia.
>
> Menu: 1) listar cidades  2) conexão direta  3) existe rota? (DFS)  4) menor rota (BFS)  5) sair
> _3
> Origem: São Paulo
> Destino: Salvador
> DFS visitando: São Paulo -> Rio de Janeiro -> Belo Horizonte -> Brasília -> Goiânia -> Curitiba -> Florianópolis -> Porto Alegre -> Vitória
> Rota NÃO encontrada. Não é possível ir de São Paulo até Salvador.
>
> Menu: 1) listar cidades  2) conexão direta  3) existe rota? (DFS)  4) menor rota (BFS)  5) sair
> _4
> Origem: São Paulo
> Destino: Goiânia
> Menor rota (BFS): São Paulo -> Belo Horizonte -> Brasília -> Goiânia
> Paradas: 3
>
> Menu: 1) listar cidades  2) conexão direta  3) existe rota? (DFS)  4) menor rota (BFS)  5) sair
> _4
> Origem: Curitiba
> Destino: Vitória
> Menor rota (BFS): Curitiba -> São Paulo -> Rio de Janeiro -> Vitória
> Paradas: 3
>
> Menu: 1) listar cidades  2) conexão direta  3) existe rota? (DFS)  4) menor rota (BFS)  5) sair
> _4
> Origem: Porto Alegre
> Destino: Fortaleza
> Não existe rota entre Porto Alegre e Fortaleza.
>
> Menu: 1) listar cidades  2) conexão direta  3) existe rota? (DFS)  4) menor rota (BFS)  5) sair
> _5
> Boa viagem! 🧳
```

---

## 🔥 Parte 1.1 — Pra quem gosta mesmo

Além das funcionalidades da Parte 1, implemente:

1. **Adicionar cidades e estradas** — o usuário pode adicionar novas cidades e conexões ao mapa. Se o usuário conectar Salvador a Brasília, por exemplo, o grafo se torna totalmente conexo!

2. **Grupos isolados** — implemente uma função que usa DFS para encontrar todos os **componentes conexos** do grafo. Ou seja, quais grupos de cidades estão isolados entre si (sem estrada ligando um grupo ao outro).

3. **Cidades próximas** — dado uma cidade, mostre todas as cidades alcançáveis com no máximo **2 paradas** (equivalente a "amigos de amigos" em redes sociais). Use BFS com controle de nível.

---

## 💻 Exemplo da Parte 1.1

```bash
> Menu: 1) listar  2) conexão direta  3) existe rota? (DFS)  4) menor rota (BFS)
>       5) adicionar conexão  6) grupos isolados  7) cidades próximas  8) sair
> _6
> Grupos de cidades conectadas:
>   Grupo 1: [São Paulo, Rio de Janeiro, Belo Horizonte, Curitiba, Florianópolis, Porto Alegre, Brasília, Goiânia, Vitória]
>   Grupo 2: [Salvador, Recife, Fortaleza]
> Existem 2 grupos isolados.
>
> Menu: ...
> _5
> Cidade 1: Brasília
> Cidade 2: Salvador
> Conexão adicionada: Brasília <-> Salvador
>
> Menu: ...
> _6
> Grupos de cidades conectadas:
>   Grupo 1: [São Paulo, Rio de Janeiro, Belo Horizonte, Curitiba, Florianópolis, Porto Alegre, Brasília, Goiânia, Vitória, Salvador, Recife, Fortaleza]
> Todas as cidades estão conectadas!
>
> Menu: ...
> _7
> Cidade: Curitiba
> Cidades alcançáveis com até 2 paradas a partir de Curitiba:
>   1 parada:  [São Paulo, Florianópolis]
>   2 paradas: [Rio de Janeiro, Belo Horizonte, Porto Alegre]
>
> Menu: ...
> _8
> Boa viagem! 🧳
```
