using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using GeradorDadosWebAssembly.Dtos;
using GeradorDadosWebAssembly.Extensions;
using GeradorDadosWebAssembly.Services.Interfaces;
using GeradorDadosWebAssembly.Utils;
using SecureIdentity.Password;

namespace GeradorDadosWebAssembly.Services
{
    public sealed class GeradorPessoaService : IGeradorPessoaService
    {
        private readonly IEnderecoService _enderecoService;

        public GeradorPessoaService(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        public async Task<PessoaDto> Gerar(bool comPontuacao = true, int? idade = null)
        {
            var endereco = new Faker<EnderecoDto>("pt_BR")
                .RuleFor(e => e.Cep, f => CepsBrasilExtension.GetRandomCep())
                .RuleFor(e => e.Estado, f => f.Address.StateAbbr())
                .RuleFor(e => e.Cidade, f => f.Address.City())
                .RuleFor(e => e.Bairro, f => f.Address.StreetName())
                .RuleFor(e => e.Rua, f => GeradorDadosUtil.GerarEnderecoPadraoBrasil(f))
                .Generate();

            var pessoa = new Faker<PessoaDto>("pt_BR")
               .RuleFor(p => p.Nome, f => f.Person.FullName.ToString())
               .RuleFor(p => p.Cpf, f => f.Person.Cpf())
               .RuleFor(p => p.Rg, f => GeradorDadosUtil.GerarRgValido())
               .RuleFor(p => p.DataNascimento, f => f.Date.Past(50, DateTime.Now.AddYears(-18)).ToString("dd/MM/yyyy"))
               .RuleFor(p => p.Genero, f => GeradorDadosUtil.TraduzirGenero(f.Person.Gender))
               .RuleFor(p => p.Signo, f => GeradorDadosUtil.GerarSigno(f))
               .RuleFor(p => p.NomeMae, f => f.Name.FullName(Name.Gender.Female))
               .RuleFor(p => p.NomePai, f => f.Name.FullName(Name.Gender.Male))
               .RuleFor(p => p.Email, (f, p) => GeradorDadosUtil.GerarEmail(f, p.Nome))
               .RuleFor(p => p.Senha, f => PasswordGenerator.Generate())
               .RuleFor(p => p.Endereco, f => endereco)
               .RuleFor(p => p.NumeroEndereco, f => f.Address.BuildingNumber())
               .RuleFor(p => p.Telefone, f => GeradorDadosUtil.GerarTelefone(f))
               .RuleFor(p => p.Celular, f => GeradorDadosUtil.GerarCelular(f))
               .RuleFor(p => p.Altura, f => (f.Random.Number(130, 200) / 100.0).ToString("0.00").Replace('.', ','))
               .RuleFor(p => p.Peso, f => $"{f.Random.Number(50, 120)}kg")
               .RuleFor(p => p.TipoSanguineo, f => GeradorDadosUtil.GerarTipoSanguineo(f))
               .Generate();

            try
            {
                var enderecoApi = await TaskUtil.ExecuteWithTimeout(ObterEnderecoApi, TimeSpan.FromSeconds(4));
                AtribuirEnderecoApi(pessoa, enderecoApi);
            }
            catch
            {
                Console.WriteLine("API falhou, usando dados gerados.");
            }

            VerificarPontuacao(comPontuacao, pessoa);
            VerificarIdade(idade, pessoa);

            return pessoa;
        }

        private async Task<EnderecoDto> ObterEnderecoApi()
        {
            var cep = ObterCep();
            var enderecoApi = await _enderecoService.ObterEnderecoPorCep(cep);
            return enderecoApi;
        }

        private static long ObterCep()
        {
            var cepObtido = CepsBrasilExtension.GetRandomCep();
            return long.Parse(cepObtido.Replace("-", ""));
        }

        private void AtribuirEnderecoApi(PessoaDto pessoa, EnderecoDto? enderecoApi)
        {
            pessoa.Endereco.Rua = enderecoApi?.Rua ?? pessoa.Endereco.Rua;
            pessoa.Endereco.Bairro = enderecoApi?.Bairro ?? pessoa.Endereco.Bairro;
            pessoa.Endereco.Cidade = enderecoApi?.Cidade ?? pessoa.Endereco.Cidade;
            pessoa.Endereco.Estado = enderecoApi?.Estado ?? pessoa.Endereco.Estado;
        }

        private void VerificarPontuacao(bool comPontuacao, PessoaDto pessoa)
        {
            if (comPontuacao is false)
            {
                pessoa.Cpf = FormatadorDeTextoUtil.RemoverPontuacao(pessoa.Cpf);
                pessoa.Rg = FormatadorDeTextoUtil.RemoverPontuacao(pessoa.Rg);
                pessoa.Celular = FormatadorDeTextoUtil.RemoverPontuacao(pessoa.Celular);
                pessoa.Telefone = FormatadorDeTextoUtil.RemoverPontuacao(pessoa.Telefone);
                pessoa.Altura = FormatadorDeTextoUtil.RemoverPontuacao(pessoa.Altura);
                pessoa.Endereco.Cep = FormatadorDeTextoUtil.RemoverPontuacao(pessoa.Endereco.Cep);
            }
        }

        private void VerificarIdade(int? idade, PessoaDto pessoa)
        {
            if (idade is not null)
            {
                pessoa.DataNascimento = DateTime.Now.AddYears((int) -idade).ToString("dd/MM/yyyy");
            }
        }
    }
}
