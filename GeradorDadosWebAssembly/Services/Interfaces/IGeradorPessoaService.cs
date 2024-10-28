using GeradorDadosWebAssembly.Dtos;

namespace GeradorDadosWebAssembly.Services.Interfaces
{
    public interface IGeradorPessoaService
    {
        Task<PessoaDto> Gerar(bool comPontuacao = true);
    }
}
