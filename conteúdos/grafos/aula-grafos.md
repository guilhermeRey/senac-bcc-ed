# Grafos (Graphs)

## O que e um Grafo?

Um **grafo** e uma estrutura de dados que modela **relacionamentos** entre elementos. E composto por:

- **Vertices** (ou nos): os elementos
- **Arestas** (ou arcos): as conexoes entre os elementos

### Analogia

Pense em um mapa de cidades conectadas por estradas. Cada cidade e um vertice e cada estrada e uma aresta. Voce pode perguntar: "existe estrada de A para B?" ou "qual o caminho mais curto de A ate C?".

### Tipos de Grafos

| Tipo | Descricao | Exemplo |
|------|-----------|---------|
| **Nao direcionado** | Arestas funcionam nos dois sentidos | Amizade no Facebook |
| **Direcionado (digrafo)** | Arestas tem direcao (A -> B != B -> A) | Seguir no Instagram |
| **Ponderado** | Arestas tem peso/custo | Distancia entre cidades |
| **Nao ponderado** | Todas as arestas tem o mesmo peso | Rede de contatos |

### Terminologia

- **Grau** de um vertice: numero de arestas conectadas a ele
- **Caminho**: sequencia de vertices conectados por arestas
- **Ciclo**: caminho que comeca e termina no mesmo vertice
- **Grafo conexo**: existe caminho entre qualquer par de vertices
- **Adjacencia**: dois vertices sao adjacentes se existe aresta entre eles

---

## Representacoes de Grafos

Existem duas formas principais de representar um grafo na memoria:

### 1. Matriz de Adjacencias

Uma **matriz quadrada** de tamanho V x V (onde V = numero de vertices). A posicao `[i][j]` indica se existe aresta do vertice `i` para o vertice `j`.

```
Grafo:          Matriz de Adjacencias:
                    0  1  2  3  4
  0 --- 1          ---------------------
  |     |      0 |  0  1  1  0  0
  |     |      1 |  1  0  0  1  0
  2 --- 3      2 |  1  0  0  1  1
        |      3 |  0  1  1  0  1
        4      4 |  0  0  1  1  0
```

```csharp
public class GrafoMatriz
{
    private int[,] matriz;
    private int vertices;

    public GrafoMatriz(int vertices)
    {
        this.vertices = vertices;
        this.matriz = new int[vertices, vertices];
    }

    public void AdicionarAresta(int origem, int destino)
    {
        matriz[origem, destino] = 1;
        matriz[destino, origem] = 1; // grafo nao direcionado
    }

    public bool ExisteAresta(int origem, int destino)
    {
        return matriz[origem, destino] == 1;
    }
}
```

**Complexidade:**

| Operacao | Complexidade |
|----------|-------------|
| Verificar se existe aresta | O(1) |
| Adicionar aresta | O(1) |
| Remover aresta | O(1) |
| Listar vizinhos de um vertice | O(V) |
| Espaco em memoria | O(V^2) |

**Vantagens:**
- Acesso direto O(1) para verificar se existe aresta
- Simples de implementar

**Desvantagens:**
- Usa O(V^2) de memoria, mesmo se o grafo tiver poucas arestas (grafo esparso)
- Listar vizinhos custa O(V)

---

### 2. Lista de Adjacencias

Cada vertice mantem uma **lista** com seus vizinhos. Usa um array de listas.

```
Grafo:           Lista de Adjacencias:

  0 --- 1        0: [1, 2]
  |     |        1: [0, 3]
  |     |        2: [0, 3, 4]
  2 --- 3        3: [1, 2, 4]
        |        4: [2, 3]
        4
```

```csharp
public class GrafoLista
{
    private List<int>[] adjacencias;
    private int vertices;

    public GrafoLista(int vertices)
    {
        this.vertices = vertices;
        this.adjacencias = new List<int>[vertices];
        for (int i = 0; i < vertices; i++)
        {
            adjacencias[i] = new List<int>();
        }
    }

    public void AdicionarAresta(int origem, int destino)
    {
        adjacencias[origem].Add(destino);
        adjacencias[destino].Add(origem); // grafo nao direcionado
    }

    public List<int> Vizinhos(int vertice)
    {
        return adjacencias[vertice];
    }
}
```

**Complexidade:**

| Operacao | Complexidade |
|----------|-------------|
| Verificar se existe aresta | O(grau do vertice) |
| Adicionar aresta | O(1) |
| Remover aresta | O(grau do vertice) |
| Listar vizinhos de um vertice | O(grau do vertice) |
| Espaco em memoria | O(V + E) |

**Vantagens:**
- Usa menos memoria para grafos esparsos: O(V + E)
- Listar vizinhos e eficiente

**Desvantagens:**
- Verificar se existe aresta pode custar O(grau)
- Um pouco mais complexa de implementar

### Quando usar cada uma?

| Criterio | Matriz | Lista |
|----------|--------|-------|
| Grafo **denso** (muitas arestas) | Melhor | - |
| Grafo **esparso** (poucas arestas) | - | Melhor |
| Precisa verificar arestas com frequencia | Melhor | - |
| Precisa listar vizinhos com frequencia | - | Melhor |
| Memoria limitada | - | Melhor |

---

## Busca em Profundidade (DFS - Depth-First Search)

A DFS explora o grafo indo o **mais fundo possivel** antes de retroceder. Funciona como explorar um labirinto: voce segue por um corredor ate chegar a um beco sem saida, depois volta e tenta outro caminho.

### Como funciona

1. Comece em um vertice, marque como visitado
2. Visite um vizinho nao visitado e repita
3. Se todos os vizinhos ja foram visitados, retroceda (backtrack)
4. Repita ate visitar todos os vertices alcancaveis

### Visualizacao

```
Grafo:         Ordem de visitacao DFS (inicio: 0):

  0 --- 1      0 -> 1 -> 3 -> 2 -> 4
  |     |
  |     |      Pilha (implicita na recursao):
  2 --- 3      [0] -> [0,1] -> [0,1,3] -> [0,1,3,2] -> [0,1,3,2,4]
        |
        4
```

### Implementacao (recursiva)

```csharp
public void DFS(int vertice, bool[] visitados)
{
    visitados[vertice] = true;
    Console.Write(vertice + " ");

    foreach (int vizinho in Vizinhos(vertice))
    {
        if (!visitados[vizinho])
        {
            DFS(vizinho, visitados);
        }
    }
}
```

### Complexidade da DFS

| | Matriz de Adjacencias | Lista de Adjacencias |
|-|----------------------|---------------------|
| Tempo | O(V^2) | O(V + E) |
| Espaco | O(V) | O(V) |

### Aplicacoes da DFS

- Detectar ciclos em grafos
- Encontrar componentes conectados
- Ordenacao topologica (grafos direcionados)
- Resolver labirintos
- Verificar se um grafo e bipartido

---

## Busca em Largura (BFS - Breadth-First Search)

A BFS explora o grafo **nivel por nivel**, visitando todos os vizinhos de um vertice antes de avancar. Funciona como ondas se propagando em um lago quando voce joga uma pedra.

### Como funciona

1. Comece em um vertice, marque como visitado, coloque na **fila**
2. Retire um vertice da fila
3. Visite todos os vizinhos nao visitados e coloque-os na fila
4. Repita ate a fila ficar vazia

### Visualizacao

```
Grafo:         Ordem de visitacao BFS (inicio: 0):

  0 --- 1      Nivel 0: 0
  |     |      Nivel 1: 1, 2
  |     |      Nivel 2: 3
  2 --- 3      Nivel 3: 4
        |
        4      Fila: [0] -> [1,2] -> [2,3] -> [3,4] -> [4] -> []
```

### Implementacao

```csharp
public void BFS(int inicio)
{
    bool[] visitados = new bool[vertices];
    Queue<int> fila = new Queue<int>();

    visitados[inicio] = true;
    fila.Enqueue(inicio);

    while (fila.Count > 0)
    {
        int vertice = fila.Dequeue();
        Console.Write(vertice + " ");

        foreach (int vizinho in Vizinhos(vertice))
        {
            if (!visitados[vizinho])
            {
                visitados[vizinho] = true;
                fila.Enqueue(vizinho);
            }
        }
    }
}
```

### Complexidade da BFS

| | Matriz de Adjacencias | Lista de Adjacencias |
|-|----------------------|---------------------|
| Tempo | O(V^2) | O(V + E) |
| Espaco | O(V) | O(V) |

### Aplicacoes da BFS

- **Caminho mais curto** em grafos nao ponderados
- Encontrar componentes conectados
- Verificar se um grafo e bipartido
- Crawler de web (visitar paginas por nivel de profundidade)
- Redes sociais (sugestao de amigos por grau de separacao)

---

## DFS vs BFS

| Criterio | DFS | BFS |
|----------|-----|-----|
| Estrutura auxiliar | Pilha (recursao) | Fila |
| Estrategia | Vai fundo, depois volta | Explora nivel a nivel |
| Caminho mais curto? | Nao garante | **Sim** (grafos nao ponderados) |
| Uso de memoria | O(V) pior caso | O(V) pior caso |
| Melhor para... | Detectar ciclos, backtracking | Caminho mais curto, niveis |

---

## Grafos em C#

### Usando Dictionary para grafos flexiveis

```csharp
// Grafo com vertices nomeados (strings)
Dictionary<string, List<string>> grafo = new Dictionary<string, List<string>>();

grafo["SP"] = new List<string> { "RJ", "MG", "PR" };
grafo["RJ"] = new List<string> { "SP", "MG" };
grafo["MG"] = new List<string> { "SP", "RJ" };
grafo["PR"] = new List<string> { "SP", "SC" };
grafo["SC"] = new List<string> { "PR" };

// Listar vizinhos de SP
foreach (string vizinho in grafo["SP"])
{
    Console.WriteLine($"SP -> {vizinho}");
}
```

---

## Casos de uso no mundo real

| Uso | Exemplo |
|-----|---------|
| Redes sociais | Amizades, seguidores, sugestoes de conexao |
| Navegacao / GPS | Cidades e estradas, caminho mais curto |
| Internet | Roteamento de pacotes entre servidores |
| Compiladores | Dependencias entre modulos |
| Jogos | Mapa de fases, pathfinding de NPCs |
| Biologia | Redes de interacao de proteinas |
| Recomendacao | "Quem comprou X tambem comprou Y" |

---

## Resumo comparativo das representacoes

| | Matriz de Adjacencias | Lista de Adjacencias |
|-|----------------------|---------------------|
| Espaco | O(V^2) | O(V + E) |
| Verificar aresta | O(1) | O(grau) |
| Listar vizinhos | O(V) | O(grau) |
| Adicionar aresta | O(1) | O(1) |
| Remover aresta | O(1) | O(grau) |
| Melhor para | Grafos densos | Grafos esparsos |
