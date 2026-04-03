# Árvores Binárias de Busca

Agora que você aprendeu o conceito de Árvore Binária de Busca, uma árvore binária ordenada, onde filhos esquerdos são menores que o pai e filhos direitos são maiores.

## Desafio

Faça uma implementação, usando `C#`, que represente sua Árvore de Busca Binária. A classe pode se chamar `BST`, para `Binary Search Tree`. 

Já temos a classe `Node`, que representa um nó da árvore. Cada nó tem um filho à esquerda e um filho à direita, bem como sua chave (key).

Seu desafio é implementar a classe `BST`, com dois métodos:
- `Insert(int value)`: insere um valor na árvore, mantendo ela como uma árvore binária de busca. Considere que não serão inseridos valores repetidos neste momento.
- `Search(int value)`: busca um valor na árvore, retornando o `Node` encontrado ou `null` caso contrário.

Além disso, lembre-se de que a `BST` precisa ter algum nó que seja a raíz da árvore ;).

```csharp
// Nó da árvore
public class Node {
    public int Key { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }
    
    // Construtor
    public Node(int key) {
        this.Key = key;
    }
}

// BST é Binary Search Tree
public class BST {

}
```