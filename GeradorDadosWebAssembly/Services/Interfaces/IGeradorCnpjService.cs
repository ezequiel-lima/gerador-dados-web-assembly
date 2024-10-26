namespace GeradorDadosWebAssembly.Services.Interfaces
{
    public interface IGeradorCnpjService
    {
        string Gerar(bool comPontuacao = true);
    }
}
