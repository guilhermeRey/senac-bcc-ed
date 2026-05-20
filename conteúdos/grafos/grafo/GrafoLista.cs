using System;
using System.Collections.Generic;

namespace Senac.ED.Grafos;

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
        adjacencias[destino].Add(origem);
    }

    public void RemoverAresta(int origem, int destino)
    {
        adjacencias[origem].Remove(destino);
        adjacencias[destino].Remove(origem);
    }

    public bool ExisteAresta(int origem, int destino)
    {
        return adjacencias[origem].Contains(destino);
    }

    public List<int> Vizinhos(int vertice)
    {
        return adjacencias[vertice];
    }

    public void DFS(int vertice, bool[] visitados)
    {
        visitados[vertice] = true;
        Console.Write(vertice + " ");

        foreach (int vizinho in adjacencias[vertice])
        {
            if (!visitados[vizinho])
            {
                DFS(vizinho, visitados);
            }
        }
    }

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

            foreach (int vizinho in adjacencias[vertice])
            {
                if (!visitados[vizinho])
                {
                    visitados[vizinho] = true;
                    fila.Enqueue(vizinho);
                }
            }
        }
    }

    public void Imprimir()
    {
        for (int i = 0; i < vertices; i++)
        {
            Console.Write($"  {i}: [");
            for (int j = 0; j < adjacencias[i].Count; j++)
            {
                Console.Write(adjacencias[i][j]);
                if (j < adjacencias[i].Count - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine("]");
        }
    }

    public int Vertices => vertices;
    public int TotalArestas
    {
        get
        {
            int total = 0;
            for (int i = 0; i < vertices; i++)
            {
                total += adjacencias[i].Count;
            }
            return total / 2;
        }
    }
}
