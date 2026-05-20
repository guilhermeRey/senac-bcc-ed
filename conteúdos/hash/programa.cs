using System;
using System.Collections.Generic;
using System.Diagnostics;
using Senac.ED.Hash;

public class Programa
{
    public static void Main(string[] args)
    {
        // ===========================================
        // PARTE 1: Insercao basica
        // ===========================================
        Console.WriteLine("=== Parte 1: Insercao basica ===\n");

        TabelaHash tabela = new TabelaHash(7);

        Console.WriteLine("Criando tabela hash com tamanho 7...");
        Console.WriteLine("Inserindo contatos:\n");

        string[][] contatos = new string[][]
        {
            new[] { "Ana", "ana@email.com" },
            new[] { "Bruno", "bruno@email.com" },
            new[] { "Carlos", "carlos@email.com" },
            new[] { "Diana", "diana@email.com" },
            new[] { "Eduardo", "edu@email.com" },
            new[] { "Fernanda", "fer@email.com" },
        };

        foreach (var contato in contatos)
        {
            int indice = tabela.FuncaoHash(contato[0]);
            Console.WriteLine($"  Inserindo \"{contato[0]}\" -> indice {indice}");
            tabela.Inserir(contato[0], contato[1]);
        }

        Console.WriteLine($"\nEstado da tabela (fator de carga: {tabela.FatorDeCarga():F2}):");
        tabela.Imprimir();

        // ===========================================
        // PARTE 2: Demonstracao de colisoes
        // ===========================================
        Console.WriteLine("\n=== Parte 2: Colisoes ===\n");

        TabelaHash tabela2 = new TabelaHash(5);
        Console.WriteLine("Tabela com tamanho 5 (colisoes mais frequentes):\n");

        string[] nomes = { "Ana", "Bia", "Caio", "Dan", "Eva", "Flu", "Gil", "Hugo" };
        foreach (string nome in nomes)
        {
            int indice = tabela2.FuncaoHash(nome);
            Console.WriteLine($"  \"{nome}\" -> hash = {indice}");
            tabela2.Inserir(nome, $"{nome.ToLower()}@email.com");
        }

        Console.WriteLine("\nEstado da tabela com colisoes:");
        tabela2.Imprimir();
        Console.WriteLine($"Fator de carga: {tabela2.FatorDeCarga():F2}");

        // ===========================================
        // PARTE 3: Busca e remocao
        // ===========================================
        Console.WriteLine("\n=== Parte 3: Busca e Remocao ===\n");

        Console.WriteLine("Buscando \"Carlos\": " + tabela.Buscar("Carlos"));
        Console.WriteLine("Buscando \"Zeca\":   " + (tabela.Buscar("Zeca") ?? "(nao encontrado)"));

        Console.WriteLine("\nRemovendo \"Carlos\"...");
        tabela.Remover("Carlos");

        Console.WriteLine("Buscando \"Carlos\": " + (tabela.Buscar("Carlos") ?? "(nao encontrado)"));
        Console.WriteLine("\nTabela apos remocao:");
        tabela.Imprimir();

        // ===========================================
        // PARTE 4: Comparacao de desempenho
        // ===========================================
        Console.WriteLine("\n=== Parte 4: Desempenho Hash vs Lista ===\n");

        int n = 100000;
        Console.WriteLine($"Inserindo {n} elementos e buscando 1000 deles...\n");

        // Preparar tabela hash grande
        TabelaHash tabelaGrande = new TabelaHash(10007); // primo para melhor distribuicao
        List<string> lista = new List<string>();
        string[] chavesTeste = new string[1000];

        Random rng = new Random(42);
        for (int i = 0; i < n; i++)
        {
            string chave = $"chave_{i}";
            tabelaGrande.Inserir(chave, $"valor_{i}");
            lista.Add(chave);
            if (i % 100 == 0 && i / 100 < 1000)
            {
                chavesTeste[i / 100] = chave;
            }
        }

        // Busca na tabela hash
        Stopwatch sw = Stopwatch.StartNew();
        for (int i = 0; i < chavesTeste.Length; i++)
        {
            tabelaGrande.Buscar(chavesTeste[i]);
        }
        sw.Stop();
        long tempoHash = sw.ElapsedTicks;

        // Busca na lista (linear)
        sw.Restart();
        for (int i = 0; i < chavesTeste.Length; i++)
        {
            lista.Contains(chavesTeste[i]);
        }
        sw.Stop();
        long tempoLista = sw.ElapsedTicks;

        Console.WriteLine($"  Busca em TabelaHash: {tempoHash} ticks");
        Console.WriteLine($"  Busca em Lista:      {tempoLista} ticks");
        Console.WriteLine($"  Lista foi {(double)tempoLista / tempoHash:F1}x mais lenta");
        Console.WriteLine($"  Fator de carga da tabela grande: {tabelaGrande.FatorDeCarga():F2}");

        // ===========================================
        // PARTE 5: Dictionary do C#
        // ===========================================
        Console.WriteLine("\n=== Parte 5: Dictionary<TKey, TValue> do C# ===\n");

        Dictionary<string, string> dicionario = new Dictionary<string, string>();
        dicionario["Ana"] = "ana@email.com";
        dicionario["Bruno"] = "bruno@email.com";
        dicionario["Carlos"] = "carlos@email.com";

        Console.WriteLine("Dictionary criado com 3 contatos:");
        foreach (var par in dicionario)
        {
            Console.WriteLine($"  {par.Key} -> {par.Value}");
        }

        Console.WriteLine($"\nBuscando \"Bruno\": {dicionario["Bruno"]}");

        // Demonstrando GetHashCode
        Console.WriteLine("\nGetHashCode() de algumas strings:");
        string[] exemplos = { "Ana", "Bruno", "Carlos", "ana" };
        foreach (string s in exemplos)
        {
            Console.WriteLine($"  \"{s}\".GetHashCode() = {s.GetHashCode()}");
        }

        Console.WriteLine("\nNota: GetHashCode() pode variar entre execucoes do programa!");
        Console.WriteLine("      Por isso nao deve ser persistido em arquivos ou bancos de dados.");
    }
}
