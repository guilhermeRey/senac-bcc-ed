using System;
using System.Collections.Generic;

namespace Senac.ED.Grafos;

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
        matriz[destino, origem] = 1;
    }

    public void RemoverAresta(int origem, int destino)
    {
        matriz[origem, destino] = 0;
        matriz[destino, origem] = 0;
    }

    public bool ExisteAresta(int origem, int destino)
    {
        return matriz[origem, destino] == 1;
    }

    public List<int> Vizinhos(int vertice)
    {
        List<int> vizinhos = new List<int>();
        for (int i = 0; i < vertices; i++)
        {
            if (matriz[vertice, i] == 1)
            {
                vizinhos.Add(i);
            }
        }
        return vizinhos;
    }

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

    public void Imprimir()
    {
        Console.Write("    ");
        for (int i = 0; i < vertices; i++)
        {
            Console.Write($"{i}  ");
        }
        Console.WriteLine();

        Console.Write("   ");
        for (int i = 0; i < vertices; i++)
        {
            Console.Write("---");
        }
        Console.WriteLine();

        for (int i = 0; i < vertices; i++)
        {
            Console.Write($"{i} | ");
            for (int j = 0; j < vertices; j++)
            {
                Console.Write($"{matriz[i, j]}  ");
            }
            Console.WriteLine();
        }
    }

    public int Vertices => vertices;
}
