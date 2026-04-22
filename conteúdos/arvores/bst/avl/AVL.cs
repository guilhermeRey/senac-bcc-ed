using System;
namespace Senac.ED.BST.AVL;

/**
Vamos criar uma classe de árvore de busca binária balanceada, AVL.

1. Criar nossa classe AVL
2. Adicionar método de busca na árvore
3. Adicionar método de altura de um nó
3.1 Faz sentido esse método estar na árvore?
4. Adicionar método para cálculo de fator de balanceamento
5. Adicionar método que consiga fazer o RotL
6. Adicionar método que consiga fazer o RotR
7. Adicionar método que faça a inserção balanceada
*/
public class AVL
{
  private AVLNo Raiz { get; set; }

  private void printNicely(AVLNo node, string spacing)
  {
    if (node != null)
    {
      Console.WriteLine(spacing + node.Chave);
      this.printNicely(node.Esq, spacing + "..");
      this.printNicely(node.Dir, spacing + "..");
    }
  }

  public void PrintNicely()
  {
    this.printNicely(this.Raiz, ".");
  }

  private AVLNo BuscaRecursivo(AVLNo raiz, int chave)
  {
    if (raiz == null)
    {
      return null;
    }

    if (raiz.Chave == chave)
      return raiz;
    else if (chave > raiz.Chave)
      return BuscaRecursivo(raiz.Dir, chave);
    else
      return BuscaRecursivo(raiz.Esq, chave);
  }

  public AVLNo Busca(int valor)
  {
    return BuscaRecursivo(Raiz, valor);
  }

  private AVLNo RotacionaEsquerda(AVLNo raiz)
  {
    if (raiz == null) return raiz;

    AVLNo novaRaiz = raiz.Dir;
    raiz.Dir = novaRaiz.Esq;
    novaRaiz.Esq = raiz;
    raiz.CalculaAltura();
    novaRaiz.CalculaAltura();

    return novaRaiz;
  }
  private AVLNo RotacionaDireita(AVLNo raiz)
  {
    if (raiz == null) return raiz;

    AVLNo novaRaiz = raiz.Esq;
    raiz.Esq = novaRaiz.Dir;
    novaRaiz.Dir = raiz;
    raiz.CalculaAltura();
    novaRaiz.CalculaAltura();

    return novaRaiz;
  }

  private AVLNo InserirRecursivo(AVLNo raiz, int chave)
  {
    if (raiz == null) return new AVLNo(chave);

    // Igual BST padrão: vamos buscar onde inserir o nó
    // navegando pela árvore
    if (chave > raiz.Chave)
      raiz.Dir = InserirRecursivo(raiz.Dir, chave);
    else
      raiz.Esq = InserirRecursivo(raiz.Esq, chave);

    // Aqui nós modificamos a raiz, adicionando algo à direita ou 
    // à esquerda. Por isso, recalculamos sua altura.
    raiz.CalculaAltura();
    // Agora, vamos verificar se o nó está desbalanceado
    if (raiz.FatorDeBalanceamento == 2)
    {
      if (raiz.Esq?.FatorDeBalanceamento < 0)
        raiz.Esq = RotacionaEsquerda(raiz.Esq);

      raiz = RotacionaDireita(raiz);
    }
    else if (raiz.FatorDeBalanceamento == -2)
    {
      if (raiz.Dir?.FatorDeBalanceamento > 0)
        raiz.Dir = RotacionaDireita(raiz.Dir);

      raiz = RotacionaEsquerda(raiz);
    }

    return raiz;
  }

  public void Insert(int valor)
  {
    Raiz = InserirRecursivo(Raiz, valor);
  }


}