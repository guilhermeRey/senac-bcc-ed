using System;
using System.Collections.Generic;
using Senac.ED.Grafos;

public class Programa
{
    public static void Main(string[] args)
    {
        // ===========================================
        // PARTE 1: Matriz de Adjacencias
        // ===========================================
        Console.WriteLine("=== Parte 1: Matriz de Adjacencias ===\n");

        // Grafo:
        //   0 --- 1
        //   |     |
        //   |     |
        //   2 --- 3
        //         |
        //         4

        GrafoMatriz gMatriz = new GrafoMatriz(5);
        gMatriz.AdicionarAresta(0, 1);
        gMatriz.AdicionarAresta(0, 2);
        gMatriz.AdicionarAresta(1, 3);
        gMatriz.AdicionarAresta(2, 3);
        gMatriz.AdicionarAresta(3, 4);

        Console.WriteLine("Grafo com 5 vertices e 5 arestas:");
        Console.WriteLine("  0---1");
        Console.WriteLine("  |   |");
        Console.WriteLine("  2---3");
        Console.WriteLine("      |");
        Console.WriteLine("      4\n");

        Console.WriteLine("Matriz de Adjacencias:");
        gMatriz.Imprimir();

        Console.WriteLine($"\nExiste aresta 0-1? {gMatriz.ExisteAresta(0, 1)}");
        Console.WriteLine($"Existe aresta 0-3? {gMatriz.ExisteAresta(0, 3)}");
        Console.WriteLine($"Vizinhos de 3: [{string.Join(", ", gMatriz.Vizinhos(3))}]");

        // ===========================================
        // PARTE 2: Lista de Adjacencias
        // ===========================================
        Console.WriteLine("\n=== Parte 2: Lista de Adjacencias ===\n");

        // Mesmo grafo, representacao diferente
        GrafoLista gLista = new GrafoLista(5);
        gLista.AdicionarAresta(0, 1);
        gLista.AdicionarAresta(0, 2);
        gLista.AdicionarAresta(1, 3);
        gLista.AdicionarAresta(2, 3);
        gLista.AdicionarAresta(3, 4);

        Console.WriteLine("Mesmo grafo usando Lista de Adjacencias:");
        gLista.Imprimir();

        Console.WriteLine($"\nExiste aresta 0-1? {gLista.ExisteAresta(0, 1)}");
        Console.WriteLine($"Existe aresta 0-3? {gLista.ExisteAresta(0, 3)}");
        Console.WriteLine($"Vizinhos de 3: [{string.Join(", ", gLista.Vizinhos(3))}]");
        Console.WriteLine($"Total de arestas: {gLista.TotalArestas}");

        // ===========================================
        // PARTE 3: Busca em Profundidade (DFS)
        // ===========================================
        Console.WriteLine("\n=== Parte 3: Busca em Profundidade (DFS) ===\n");

        Console.Write("DFS a partir do vertice 0 (Matriz): ");
        bool[] visitadosDFS1 = new bool[5];
        gMatriz.DFS(0, visitadosDFS1);
        Console.WriteLine();

        Console.Write("DFS a partir do vertice 0 (Lista):  ");
        bool[] visitadosDFS2 = new bool[5];
        gLista.DFS(0, visitadosDFS2);
        Console.WriteLine();

        // Grafo maior para ver DFS mais interessante
        Console.WriteLine("\nGrafo maior para DFS:");
        Console.WriteLine("  0---1---2");
        Console.WriteLine("  |   |");
        Console.WriteLine("  3---4---5");
        Console.WriteLine("      |");
        Console.WriteLine("      6\n");

        GrafoLista gGrande = new GrafoLista(7);
        gGrande.AdicionarAresta(0, 1);
        gGrande.AdicionarAresta(0, 3);
        gGrande.AdicionarAresta(1, 2);
        gGrande.AdicionarAresta(1, 4);
        gGrande.AdicionarAresta(3, 4);
        gGrande.AdicionarAresta(4, 5);
        gGrande.AdicionarAresta(4, 6);

        Console.Write("DFS a partir de 0: ");
        bool[] visitadosGrande = new bool[7];
        gGrande.DFS(0, visitadosGrande);
        Console.WriteLine();

        // ===========================================
        // PARTE 4: Busca em Largura (BFS)
        // ===========================================
        Console.WriteLine("\n=== Parte 4: Busca em Largura (BFS) ===\n");

        Console.Write("BFS a partir do vertice 0 (Matriz): ");
        gMatriz.BFS(0);
        Console.WriteLine();

        Console.Write("BFS a partir do vertice 0 (Lista):  ");
        gLista.BFS(0);
        Console.WriteLine();

        Console.Write("\nBFS no grafo maior a partir de 0:   ");
        gGrande.BFS(0);
        Console.WriteLine();

        Console.WriteLine("\nComparacao de ordem de visitacao (grafo maior):");
        Console.Write("  DFS: ");
        bool[] v = new bool[7];
        gGrande.DFS(0, v);
        Console.Write("\n  BFS: ");
        gGrande.BFS(0);
        Console.WriteLine();

        // ===========================================
        // PARTE 5: Exercicio - Rede Social
        // ===========================================
        Console.WriteLine("\n=== Parte 5: Exercicio - Rede Social ===\n");

        Console.WriteLine("Modele uma rede social com 6 pessoas:");
        Console.WriteLine("  Alice(0), Bruno(1), Carla(2), Diego(3), Elena(4), Fabio(5)\n");
        Console.WriteLine("Conexoes de amizade:");
        Console.WriteLine("  Alice  - Bruno, Carla");
        Console.WriteLine("  Bruno  - Alice, Carla, Diego");
        Console.WriteLine("  Carla  - Alice, Bruno, Elena");
        Console.WriteLine("  Diego  - Bruno, Fabio");
        Console.WriteLine("  Elena  - Carla, Fabio");
        Console.WriteLine("  Fabio  - Diego, Elena\n");

        string[] nomes = { "Alice", "Bruno", "Carla", "Diego", "Elena", "Fabio" };

        GrafoLista redeSocial = new GrafoLista(6);
        redeSocial.AdicionarAresta(0, 1); // Alice - Bruno
        redeSocial.AdicionarAresta(0, 2); // Alice - Carla
        redeSocial.AdicionarAresta(1, 2); // Bruno - Carla
        redeSocial.AdicionarAresta(1, 3); // Bruno - Diego
        redeSocial.AdicionarAresta(2, 4); // Carla - Elena
        redeSocial.AdicionarAresta(3, 5); // Diego - Fabio
        redeSocial.AdicionarAresta(4, 5); // Elena - Fabio

        Console.WriteLine("Lista de adjacencias da rede:");
        for (int i = 0; i < 6; i++)
        {
            List<string> amigos = new List<string>();
            foreach (int vizinho in redeSocial.Vizinhos(i))
            {
                amigos.Add(nomes[vizinho]);
            }
            Console.WriteLine($"  {nomes[i]}: [{string.Join(", ", amigos)}]");
        }

        Console.WriteLine($"\nTotal de amizades: {redeSocial.TotalArestas}");

        Console.WriteLine("\nBFS a partir de Alice (quem ela alcanca e em quantos graus?):");
        // BFS com niveis para mostrar graus de separacao
        bool[] visitadosRede = new bool[6];
        int[] distancia = new int[6];
        for (int i = 0; i < 6; i++) distancia[i] = -1;

        Queue<int> filaRede = new Queue<int>();
        visitadosRede[0] = true;
        distancia[0] = 0;
        filaRede.Enqueue(0);

        while (filaRede.Count > 0)
        {
            int atual = filaRede.Dequeue();
            foreach (int vizinho in redeSocial.Vizinhos(atual))
            {
                if (!visitadosRede[vizinho])
                {
                    visitadosRede[vizinho] = true;
                    distancia[vizinho] = distancia[atual] + 1;
                    filaRede.Enqueue(vizinho);
                }
            }
        }

        for (int i = 0; i < 6; i++)
        {
            Console.WriteLine($"  {nomes[0]} -> {nomes[i]}: {distancia[i]} grau(s) de separacao");
        }

        // ===========================================
        // DESAFIO: Implementar deteccao de caminho
        // ===========================================
        Console.WriteLine("\n=== DESAFIO ===\n");
        Console.WriteLine("Implemente uma funcao que receba dois vertices e retorne");
        Console.WriteLine("se existe caminho entre eles. Teste com o grafo abaixo que");
        Console.WriteLine("tem dois componentes DESCONEXOS:\n");
        Console.WriteLine("  Componente 1:  0---1---2");
        Console.WriteLine("  Componente 2:  3---4\n");
        Console.WriteLine("  ExisteCaminho(0, 2) -> true");
        Console.WriteLine("  ExisteCaminho(0, 4) -> false");

        // TODO: Os alunos devem implementar a funcao ExisteCaminho
        // usando DFS ou BFS. Dica: fazer uma DFS a partir da origem
        // e verificar se o destino foi visitado.
    }
}
