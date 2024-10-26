using GeradorDadosWebAssembly.Dtos;
using Refit;

namespace GeradorDadosWebAssembly.Services.Interfaces
{
    [Headers("accept: application/json")]

    public interface IEnderecoService
    {
        [Get("/cep/v2/{cep}")]
        Task<EnderecoDto> ObterEnderecoPorCep(long cep);
    }
}
