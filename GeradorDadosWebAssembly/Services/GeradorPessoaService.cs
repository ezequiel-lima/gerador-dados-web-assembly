using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using GeradorDadosWebAssembly.Dtos;
using GeradorDadosWebAssembly.Extensions;
using GeradorDadosWebAssembly.Services.Interfaces;
using GeradorDadosWebAssembly.Utils;

namespace GeradorDadosWebAssembly.Services
{
    public sealed class GeradorPessoaService : IGeradorPessoaService
    {
        private readonly IEnderecoService _enderecoService;

        public GeradorPessoaService(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        public async Task<PessoaDto> Gerar(bool comPontuacao = true)
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
               .RuleFor(p => p.Rg, f => GerarRg(f)) // TO DO
               .RuleFor(p => p.DataNascimento, f => f.Date.Past(1, DateTime.Now.AddYears(-18)).ToString("dd/MM/yyyy")) // TO DO
               .RuleFor(p => p.Genero, f => f.Person.Gender.ToString())
               .RuleFor(p => p.Signo, f => f.Person.Avatar) // TO DO
               .RuleFor(p => p.NomeMae, f => f.Name.FullName(Name.Gender.Female))
               .RuleFor(p => p.NomePai, f => f.Name.FullName(Name.Gender.Male))
               .RuleFor(p => p.Email, (f, p) => GeradorDadosUtil.GerarEmail(f, p.Nome))
               .RuleFor(p => p.Senha, f => f.Person.UserName) // TO DO
               .RuleFor(p => p.Endereco, f => endereco)
               .RuleFor(p => p.NumeroEndereco, f => f.Address.BuildingNumber())
               .RuleFor(p => p.Telefone, f => GeradorDadosUtil.GerarTelefone(f))
               .RuleFor(p => p.Celular, f => GeradorDadosUtil.GerarCelular(f))
               .RuleFor(p => p.Altura, f => f.Random.Number(100, 210).ToString()) // TO DO
               .RuleFor(p => p.Peso, f => f.Random.Number(100, 210).ToString()) // TO DO
               .RuleFor(p => p.TipoSanguineo, f => GerarTipoSanguineo(f))
               .Generate();

            return pessoa;
        }

        private static string GerarRg(Faker f)
        {
            // Gera um número de RG base sem o dígito de controle (8 dígitos)
            var rgBase = f.Random.Replace("########");

            // Calcula o dígito de controle usando o módulo 11
            int digitoControle = CalcularDigitoControle(rgBase);
           
            return $"{rgBase}-{digitoControle}";
        }

        private static int CalcularDigitoControle(string rgBase)
        {
            int soma = 0;
            int peso = 9;

            // Calcula a soma ponderada dos dígitos do RG
            for (int i = 0; i < rgBase.Length; i++)
            {
                soma += (rgBase[i] - '0') * peso;
                peso--;
            }

            // Calcula o dígito de controle com base no módulo 11
            int resto = soma % 11;
            int digitoControle = 11 - resto;

            // Ajusta o dígito para os casos onde é igual a 10 ou 11
            if (digitoControle == 10) digitoControle = 0;
            else if (digitoControle == 11) digitoControle = 1;

            return digitoControle;
        }

        private static string GerarTipoSanguineo(Faker f)
        {
            var tiposSanguineos = new[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            return f.PickRandom(tiposSanguineos);
        }
    }
}
