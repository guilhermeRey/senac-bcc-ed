using System;

namespace Senac.ED.BST;

public class No
{
    public int Key { get; set; }
    public No Esq { get; set; }
    public No Dir { get; set; }

    public No(int valor)
    {
        this.Key = valor;
    }
}
