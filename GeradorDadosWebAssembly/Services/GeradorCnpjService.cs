using Bogus;
using Bogus.Extensions.Brazil;
using GeradorDadosWebAssembly.Services.Interfaces;
using GeradorDadosWebAssembly.Utils;

namespace GeradorDadosWebAssembly.Services
{
    public sealed class GeradorCnpjService : IGeradorCnpjService
    {
        public string Gerar(bool comPontuacao = true)
        {
            var faker = new Faker("pt_BR");
            var cnpj = faker.Company.Cnpj();

            if (comPontuacao == false)
            {
                cnpj = FormatadorDeTextoUtil.RemoverPontuacao(cnpj);
            }

            return cnpj;
        }
    }
}
