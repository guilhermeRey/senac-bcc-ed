using System;
using senac.ed.bst;

public class HelloWorld {
    public static void Main(string[] args) {
        BST tree = new BST();
        
        tree.Insert(15);
        tree.Insert(10);
        tree.Insert(20);
        tree.Insert(8);

        Console.WriteLine("\n");
        tree.PrintNicely();

        tree.Insert(12);
        tree.Insert(21);

        Console.WriteLine("Percurso InOrder(ordernado):");
        tree.PrintInOrder();
        
        Console.WriteLine("\n");
        tree.PrintNicely();
        
        Console.WriteLine("\nDepois de deletar 10:\n");
        bst.Delete(10);
        bst.PrintNicely();
    }
}