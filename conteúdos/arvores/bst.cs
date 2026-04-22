namespace senac.ed.bst;
using System;

/**
    Classe BST
    ------------
    @author: Prof. Guilherme Rey
    
    Representa uma árvore binária de busca, com os métodos:
    - Search(int vaor)
    - Insert(int valor)
    - Delete(int valor)
    - Max()
    - Min()
    - PrintInOrder()
    - PrintNicely()

*/
public class BST {
    private No Raiz { get; set; }
    
    public BST() {
        this.Raiz = null;
    }
    
    private No delete(No node, int chave) {
        if (node == null)
            return null;

        if (chave < node.Key) {
            node.Esq = this.delete(node.Esq, chave);
        }
        else if (chave > node.Key) {
            node.Dir = this.delete(node.Dir, chave);
        }
        else {
            if (node.Esq == null) {
                return node.Dir;
            }
            else if (node.Dir == null) {
                return node.Esq;
            }
            else {
                int valorSucessor = (int)this.min(node.Dir);
                node.Key = valorSucessor;
                node.Dir = this.delete(node.Dir, valorSucessor);
            }
        }

        return node;
    }
    
    private No InsertRecursivo(No raiz, int chave) {
        if (raiz == null) {
            return new No(chave);
        }
        
        if (chave > raiz.Key) {
            raiz.Dir = this.InsertRecursivo(raiz.Dir, chave);
        }
        else {
            raiz.Esq = this.InsertRecursivo(raiz.Esq, chave);
        }
        
        return raiz;
    }

    private No SearchRecursivo(No raiz, int chave) {
        if (raiz == null) {
            return null;
        }

        if (raiz.Key == chave)
            return raiz;
        else if (chave > raiz.Key)
            return this.SearchRecursivo(raiz.Dir, chave);
        else
            return this.SearchRecursivo(raiz.Esq, chave);
    }

    private int? min(No node) {
        No aux = node;
        while (aux != null && aux.Esq != null) {
            aux = aux.Esq;
        }
        
        return aux == null ? null : aux.Key;
    }

    private void printNicely(No node, string spacing) {
        if (node != null) {
            Console.WriteLine(spacing + node.Key);
            this.printNicely(node.Esq, spacing + "..");
            this.printNicely(node.Dir, spacing + "..");
        }
    }

    public No Search(int valor) {
        return this.SearchRecursivo(this.Raiz, valor);
    }
    
    public void Insert(int valor) {
        this.Raiz = this.InsertRecursivo(this.Raiz, valor);
    }

    public void Delete(int valor) {
        this.Raiz = this.delete(this.Raiz, valor);
    }
    
    public int? Max() {
        No aux = this.Raiz;
        while (aux != null && aux.Dir != null) {
            aux = aux.Dir;
        }
        
        return aux == null ? null : aux.Key;
    }
    
    public int? Min() {
        return this.min(this.Raiz);
    }
    
    public void PrintInOrder(No node) {
        if (node != null) {
            this.PrintInOrder(node.Esq);
            Console.Write(node.Key + " ");
            this.PrintInOrder(node.Dir);
        }
    }
    
    public void PrintNicely() {
        this.printNicely(this.Raiz, ".");
    }
}