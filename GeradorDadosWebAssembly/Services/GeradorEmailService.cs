using Bogus;
using GeradorDadosWebAssembly.Services.Interfaces;
using GeradorDadosWebAssembly.Utils;

namespace GeradorDadosWebAssembly.Services
{
    public sealed class GeradorEmailService : IGeradorEmailService
    {
        public string Gerar()
        {
            var faker = new Faker("pt_BR");
            var nick = faker.Random.Word() + faker.Random.Number(1, 999).ToString();
            var email = GeradorDadosUtil.GerarEmail(faker, nick);
            return email;
        }
    }
}
