using System;
using Senac.ED.BST;
using Senac.ED.BST.AVL;

public class HelloWorld
{

    private static void TestarCaso(string nome, int[] valores)
    {
        AVL avl = new AVL();
        Console.WriteLine($"\n=== Caso {nome} ===");
        foreach (int v in valores)
        {
            Console.WriteLine($"Inserindo {v}");
            avl.Insert(v);
        }
        Console.WriteLine("AVL final:");
        avl.PrintNicely();
    }
    public static void Main(string[] args)
    {
        // BST tree = new BST();
        // tree.Insert(1);
        // tree.Insert(3);
        // tree.Insert(2);

        // Console.WriteLine("\n");
        // tree.PrintNicely();

        // AVL avlTree = new AVL();

        // avlTree.Insert(1);
        // avlTree.Insert(3);
        // avlTree.Insert(2);

        // Console.WriteLine("\n");
        // avlTree.PrintNicely();

        // // 1) Quatro cenários clássicos de rotação AVL
        // TestarCaso("LL (30,20,10)", new[] { 30, 20, 10 });
        // TestarCaso("RR (10,20,30)", new[] { 10, 20, 30 });
        // TestarCaso("LR (30,10,20)", new[] { 30, 10, 20 });
        // TestarCaso("RL (10,30,20)", new[] { 10, 30, 20 });
        // 2) Comparação prática BST x AVL com sequência crescente
        Console.WriteLine("\n=== Comparacao BST x AVL (1..7) ===");
        BST bst = new BST();
        AVL avl2 = new AVL();
        for (int i = 1; i <= 7; i++)
        {
            bst.Insert(i);
            avl2.Insert(i);
        }
        Console.WriteLine("\nBST (tende a ficar degenerada):");
        bst.PrintNicely();
        Console.WriteLine("\nAVL (deve permanecer balanceada):");
        avl2.PrintNicely();
    }

}