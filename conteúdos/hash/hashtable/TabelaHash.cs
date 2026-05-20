using System;

namespace Senac.ED.Hash;

public class TabelaHash
{
    private No[] tabela;
    private int tamanho;
    private int quantidade;

    public TabelaHash(int tamanho)
    {
        this.tamanho = tamanho;
        this.tabela = new No[tamanho];
        this.quantidade = 0;
    }

    public int FuncaoHash(string chave)
    {
        int soma = 0;
        foreach (char c in chave)
        {
            soma += (int)c;
        }
        return soma % this.tamanho;
    }

    public void Inserir(string chave, string valor)
    {
        int indice = FuncaoHash(chave);

        // Verifica se a chave ja existe e atualiza
        No atual = tabela[indice];
        while (atual != null)
        {
            if (atual.Chave == chave)
            {
                atual.Valor = valor;
                return;
            }
            atual = atual.Proximo;
        }

        // Insere no inicio da lista (prepend)
        No novo = new No(chave, valor);
        novo.Proximo = tabela[indice];
        tabela[indice] = novo;
        quantidade++;
    }

    public string Buscar(string chave)
    {
        int indice = FuncaoHash(chave);
        No atual = tabela[indice];

        while (atual != null)
        {
            if (atual.Chave == chave)
            {
                return atual.Valor;
            }
            atual = atual.Proximo;
        }

        return null;
    }

    public bool Remover(string chave)
    {
        int indice = FuncaoHash(chave);
        No atual = tabela[indice];
        No anterior = null;

        while (atual != null)
        {
            if (atual.Chave == chave)
            {
                if (anterior == null)
                {
                    tabela[indice] = atual.Proximo;
                }
                else
                {
                    anterior.Proximo = atual.Proximo;
                }
                quantidade--;
                return true;
            }
            anterior = atual;
            atual = atual.Proximo;
        }

        return false;
    }

    public void Imprimir()
    {
        for (int i = 0; i < tamanho; i++)
        {
            Console.Write($"  [{i}]: ");
            No atual = tabela[i];
            if (atual == null)
            {
                Console.WriteLine("(vazio)");
            }
            else
            {
                while (atual != null)
                {
                    Console.Write($"({atual.Chave}: {atual.Valor})");
                    if (atual.Proximo != null)
                    {
                        Console.Write(" -> ");
                    }
                    atual = atual.Proximo;
                }
                Console.WriteLine();
            }
        }
    }

    public double FatorDeCarga()
    {
        return (double)quantidade / tamanho;
    }

    public int Quantidade => quantidade;
    public int Tamanho => tamanho;
}
