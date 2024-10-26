using GeradorDadosWebAssembly.Enums;

namespace GeradorDadosWebAssembly.Extensions
{
    public static class CepsBrasilExtension
    {
        public static string GetRandomCep()
        {
            var randomCep = (ECepsBrasil)Enum
            .GetValues(typeof(ECepsBrasil))
                .Cast<ECepsBrasil>()
                .OrderBy(_ => Guid.NewGuid())
                .First();

            return randomCep.ToFormattedCep();
        }

        // Método de extensão para formatar o CEP no padrão "#####-###"
        public static string ToFormattedCep(this ECepsBrasil cep)
        {
            return ((int)cep).ToString("00000-000");
        }

        public static long ToLongCep(this ECepsBrasil cep)
        {
            return (long)cep;
        }
    }
}
