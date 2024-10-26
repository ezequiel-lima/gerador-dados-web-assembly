namespace GeradorDadosWebAssembly.Services.Interfaces
{
    public interface IGeradorCpfService
    {
        string Gerar(bool comPontuacao = true);
    }
}
