using GeradorDadosWebAssembly.Dtos;

namespace GeradorDadosWebAssembly.Services.Interfaces
{
    public interface IGeradorEmpresaService
    {
        Task<EmpresaDto> Gerar(bool comPontuacao = true);
    }
}
