using Bogus;
using Bogus.Extensions.Brazil;
using GeradorDadosWebAssembly.Services.Interfaces;
using GeradorDadosWebAssembly.Utils;

namespace GeradorDadosWebAssembly.Services
{
    public sealed class GeradorCpfService : IGeradorCpfService
    {
        public string Gerar(bool comPontuacao = true)
        {
            var faker = new Faker("pt_BR");
            string cpf = faker.Person.Cpf();

            if (comPontuacao == false)
            {
                cpf = FormatadorDeTextoUtil.RemoverPontuacao(cpf);
            }

            return cpf;
        }
    }
}
