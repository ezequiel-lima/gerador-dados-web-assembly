using Bogus;
using GeradorDadosWebAssembly.Services.Interfaces;
using GeradorDadosWebAssembly.Utils;

namespace GeradorDadosWebAssembly.Services
{
    public sealed class GeradorCepService : IGeradorCepService
    {
        public string Gerar(bool comPontuacao = true)
        {
            var faker = new Faker("pt_BR");
            var cep = faker.Address.ZipCode();

            if (comPontuacao == false)
            {
                cep = FormatadorDeTextoUtil.RemoverPontuacao(cep);
            }

            return cep;
        }
    }
}
