namespace Senac.ED.Hash;

public class No
{
    public string Chave { get; set; }
    public string Valor { get; set; }
    public No Proximo { get; set; }

    public No(string chave, string valor)
    {
        this.Chave = chave;
        this.Valor = valor;
        this.Proximo = null;
    }
}
