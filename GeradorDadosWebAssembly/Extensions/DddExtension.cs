using GeradorDadosWebAssembly.Enums;

namespace GeradorDadosWebAssembly.Extensions
{
    public static class DddExtension
    {
        // Converte o valor do enum para o número do DDD
        public static int GetDddValue(this EDdd ddd)
        {
            return (int)ddd;
        }

        public static EDdd GetRandomDdd()
        {
            var values = Enum.GetValues(typeof(EDdd)).Cast<EDdd>().ToList();
            return values[new Random().Next(values.Count)];
        }
    }
}
