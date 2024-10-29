using Bogus;
using GeradorDadosWebAssembly.Services.Interfaces;
using GeradorDadosWebAssembly.Utils;

namespace GeradorDadosWebAssembly.Services
{
    public sealed class GeradorNifService : IGeradorNifService
    {
        public string Gerar()
        {
            var faker = new Faker("pt_BR");
            var nif = GeradorDadosUtil.GerarNif(faker);
            return nif;
        }
    }
}
