# Tabelas Hash (Hash Tables)

## O que é uma Tabela Hash?

Uma **tabela hash** (ou hash table) é uma estrutura de dados que associa **chaves** a **valores**, permitindo buscas em tempo **O(1)** na média.

### Analogia

Pense em um armário com gavetas numeradas. Cada item que você guarda passa por uma "regra" que diz em qual gaveta ele vai. Quando precisa encontrar o item, aplica a mesma regra e vai direto à gaveta certa — sem precisar abrir todas.

### Complexidade

| Operação | Média | Pior caso |
|----------|-------|-----------|
| Inserção | O(1)  | O(n)      |
| Busca    | O(1)  | O(n)      |
| Remoção  | O(1)  | O(n)      |

O pior caso acontece quando **todas as chaves colidem** no mesmo índice (tudo vira uma lista ligada).

---

## Função Hash

A função hash transforma uma chave (qualquer tipo de dado) em um **índice inteiro** dentro do intervalo da tabela.

```
indice = funcaoHash(chave) % tamanho_da_tabela
```

### Propriedades de uma boa função hash

1. **Determinística** — a mesma chave sempre produz o mesmo índice
2. **Distribuição uniforme** — espalha as chaves igualmente pelas posições
3. **Rápida de calcular** — O(1) ou O(k) onde k é o tamanho da chave

### Exemplo simples: soma dos códigos ASCII

```csharp
public int FuncaoHash(string chave)
{
    int soma = 0;
    foreach (char c in chave)
    {
        soma += (int)c;
    }
    return soma % this.tamanho;
}
```

Para a string `"Ana"`:
- 'A' = 65, 'n' = 110, 'a' = 97
- soma = 65 + 110 + 97 = 272
- Se tamanho = 7: índice = 272 % 7 = **6**

---

## Colisões

Uma **colisão** ocorre quando duas chaves diferentes produzem o mesmo índice.

Isso é inevitável (princípio da casa dos pombos): se temos mais chaves possíveis do que posições na tabela, colisões vão acontecer.

### Estratégia 1: Encadeamento (Chaining)

Cada posição da tabela armazena uma **lista ligada**. Quando há colisão, o novo elemento é adicionado à lista naquela posição.

```
[0]: (vazio)
[1]: (Bruno: bruno@email) -> (Gil: gil@email)
[2]: (Ana: ana@email)
[3]: (vazio)
[4]: (Carlos: carlos@email) -> (Diana: diana@email)
[5]: (vazio)
[6]: (Eduardo: edu@email)
```

**Vantagens:**
- Simples de implementar
- Nunca "enche" — sempre dá para adicionar mais
- Remoção é fácil (remover nó da lista)

**Desvantagens:**
- Usa memória extra (ponteiros da lista)
- Cache-unfriendly (nós espalhados na memória)

### Estratégia 2: Endereçamento Aberto (Open Addressing)

Quando há colisão, procura a **próxima posição livre** na própria tabela.

**Sondagem linear (linear probing):**
```
indice = (hash(chave) + i) % tamanho   // i = 0, 1, 2, ...
```

**Sondagem quadrática:**
```
indice = (hash(chave) + i²) % tamanho
```

**Vantagens:**
- Melhor uso de cache (dados contíguos na memória)
- Sem alocação extra

**Desvantagens:**
- Pode formar "clusters" (agrupamentos)
- A tabela pode encher
- Remoção é mais complexa (precisa de marcador "deletado")

---

## Fator de Carga (Load Factor)

O fator de carga indica quão "cheia" está a tabela:

```
α = n / m
```

Onde:
- `n` = número de elementos armazenados
- `m` = tamanho da tabela

### Quando redimensionar?

- **Encadeamento:** tipicamente quando α > 1.0
- **Endereçamento aberto:** tipicamente quando α > 0.75

O redimensionamento (rehashing) cria uma nova tabela maior e reinsere todos os elementos — custa O(n) mas acontece raramente (custo amortizado).

---

## Hash Tables em C#

### `Dictionary<TKey, TValue>`

O Dictionary do C# é a implementação padrão de tabela hash:

```csharp
Dictionary<string, string> contatos = new Dictionary<string, string>();

// Inserir
contatos["Ana"] = "ana@email.com";
contatos.Add("Bruno", "bruno@email.com");

// Buscar
string email = contatos["Ana"];              // lança exceção se não existir
bool achou = contatos.TryGetValue("Ana", out string valor);

// Remover
contatos.Remove("Ana");

// Verificar existência
bool existe = contatos.ContainsKey("Bruno");
```

### `HashSet<T>`

Quando você só precisa saber se um elemento **existe ou não** (sem valor associado):

```csharp
HashSet<string> visitados = new HashSet<string>();
visitados.Add("pagina1.html");
visitados.Add("pagina2.html");
visitados.Add("pagina1.html");  // ignorado, já existe

Console.WriteLine(visitados.Count);          // 2
Console.WriteLine(visitados.Contains("pagina1.html")); // True
```

### `GetHashCode()` e `Equals()`

Todo objeto em C# tem o método `GetHashCode()`. Se você criar classes customizadas para usar como chave, precisa sobrescrever ambos:

```csharp
public class Aluno
{
    public string RA { get; set; }
    public string Nome { get; set; }

    public override int GetHashCode()
    {
        return RA.GetHashCode();  // usa o RA como identificador unico
    }

    public override bool Equals(object obj)
    {
        if (obj is Aluno outro)
            return this.RA == outro.RA;
        return false;
    }
}
```

**Regra fundamental:** se dois objetos são `Equals`, eles **devem** ter o mesmo `GetHashCode()`.

---

## Casos de uso no mundo real

| Uso | Exemplo |
|-----|---------|
| Cache | Armazenar resultados de consultas caras |
| Índices de banco de dados | Busca rápida por chave primária |
| Desduplicação | Remover itens repetidos de uma coleção |
| Contagem de frequência | Contar palavras em um texto |
| Roteamento | Mapear URLs para handlers |
| Compiladores | Tabela de símbolos (variáveis e seus tipos) |
| Blockchain | Verificação de integridade de dados |

---

## Resumo comparativo

| Estrutura | Busca | Inserção | Remoção | Ordenado? |
|-----------|-------|----------|---------|-----------|
| Lista     | O(n)  | O(1)*    | O(n)    | Não       |
| BST       | O(log n) | O(log n) | O(log n) | Sim    |
| AVL       | O(log n) | O(log n) | O(log n) | Sim    |
| Hash Table | O(1) média | O(1) média | O(1) média | **Não** |

*\* inserção no início*

**Trade-off principal:** tabelas hash são mais rápidas para busca/inserção, mas **não mantêm ordem**. Se você precisa iterar em ordem, use uma árvore.
