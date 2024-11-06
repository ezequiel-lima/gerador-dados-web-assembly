using Bogus;
using GeradorDadosWebAssembly.Services.Interfaces;
using GeradorDadosWebAssembly.Utils;

namespace GeradorDadosWebAssembly.Services
{
    public class GeradorCnhService : IGeradorCnhService
    {
        public string Gerar()
        {
            var faker = new Faker("pt_BR");
            var cnh = GeradorDadosUtil.GerarCnh(faker);
            return cnh;
        }
    }
}
