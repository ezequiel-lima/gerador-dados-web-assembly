using Bogus;
using Bogus.Extensions.Brazil;
using GeradorDadosWebAssembly.Dtos;
using GeradorDadosWebAssembly.Extensions;
using GeradorDadosWebAssembly.Services.Interfaces;
using GeradorDadosWebAssembly.Utils;

namespace GeradorDadosWebAssembly.Services
{
    public sealed class GeradorEmpresaService : IGeradorEmpresaService
    {
        private readonly IEnderecoService _enderecoService;

        public GeradorEmpresaService(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        public async Task<EmpresaDto> Gerar(bool comPontuacao = true)
        {
            var socios = new Faker<SocioDto>()
                .RuleFor(s => s.Nome, f => f.Person.FullName.ToString())
                .RuleFor(s => s.Cpf, f => f.Person.Cpf())
                .Generate(2)
                .ToList();

            var empresa = new Faker<EmpresaDto>("pt_BR")
                .RuleFor(e => e.Nome, f => f.Company.CompanyName())
                .RuleFor(e => e.Cnpj, f => f.Company.Cnpj())
                .RuleFor(e => e.DataAbertura, f => f.Date.Past(10).ToString("dd/MM/yyyy"))
                .RuleFor(e => e.Site, (f, e) => GeradorDadosUtil.GerarSite(e.Nome))
                .RuleFor(e => e.Email, (f, e) => GeradorDadosUtil.GerarEmail(f, e.Nome))
                .RuleFor(e => e.Cep, f => CepsBrasilExtension.GetRandomCep())
                .RuleFor(e => e.Endereco, f => GeradorDadosUtil.GerarEnderecoPadraoBrasil(f))
                .RuleFor(e => e.NumeroEndereco, f => f.Address.BuildingNumber())
                .RuleFor(e => e.Bairro, f => f.Address.StreetName())
                .RuleFor(e => e.Cidade, f => f.Address.City())
                .RuleFor(e => e.Estado, f => f.Address.StateAbbr())
                .RuleFor(e => e.InscricaoEstadual, f => f.Random.Number(111111111, 999999999).ToString())
                .RuleFor(e => e.Telefone, f => GeradorDadosUtil.GerarTelefone(f))
                .RuleFor(e => e.Celular, f => GeradorDadosUtil.GerarCelular(f))
                .RuleFor(e => e.Socio, f => socios)
                .Generate();

            try
            {
                var cep = long.Parse(empresa.Cep.Replace("-", ""));
                var enderecoApi = await TaskUtil.ExecuteWithTimeout(() => ObterEnderecoApi(cep), TimeSpan.FromSeconds(4));
                AtribuirEnderecoApi(empresa, enderecoApi);
                empresa.InscricaoEstadual = InscricaoEstadualExtension.ObterInscricaoPorEstado(empresa.Estado);
            }
            catch
            {
                Console.WriteLine("API falhou, usando dados gerados.");
            }

            VerificarPontuacao(comPontuacao, empresa);

            return empresa;
        }

        private async Task<EnderecoDto> ObterEnderecoApi(long cep)
        {
            return await _enderecoService.ObterEnderecoPorCep(cep);
        }

        private void AtribuirEnderecoApi(EmpresaDto empresa, EnderecoDto? enderecoApi)
        {
            empresa.Endereco = enderecoApi?.Rua ?? empresa.Endereco;
            empresa.Bairro = enderecoApi?.Bairro ?? empresa.Bairro;
            empresa.Cidade = enderecoApi?.Cidade ?? empresa.Cidade;
            empresa.Estado = enderecoApi?.Estado ?? empresa.Estado;
        }

        private void VerificarPontuacao(bool comPontuacao, EmpresaDto empresa)
        {
            if (comPontuacao is false)
            {
                empresa.Nome = FormatadorDeTextoUtil.RemoverPontuacao(empresa.Nome);
                empresa.Cnpj = FormatadorDeTextoUtil.RemoverPontuacao(empresa.Cnpj);
                empresa.Cep = FormatadorDeTextoUtil.RemoverPontuacao(empresa.Cep);
                empresa.Telefone = FormatadorDeTextoUtil.RemoverPontuacao(empresa.Telefone);
                empresa.Celular = FormatadorDeTextoUtil.RemoverPontuacao(empresa.Celular);

                foreach (var socio in empresa.Socio)
                {
                    socio.Cpf = FormatadorDeTextoUtil.RemoverPontuacao(socio.Cpf);
                }
            }
        }
    }
}
