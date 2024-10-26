using GeradorDadosWebAssembly.Enums;

namespace GeradorDadosWebAssembly.Extensions
{
    public static class InscricaoEstadualExtension
    {
        public static string ObterNumeroInscricaoEstadualAleatoria()
        {
            return ObterInscricaoEstadualAleatoria().ObterValorNumerico();
        }

        public static string ObterInscricaoPorEstado(string siglaEstado)
        {
            var valores = Enum.GetValues(typeof(EInscricaoEstadualBrasil)).Cast<EInscricaoEstadualBrasil>();

            var inscricoesDoEstado = valores.Where(e => e.ToString().StartsWith(siglaEstado.ToUpper())).ToList();

            if (inscricoesDoEstado is null || !inscricoesDoEstado.Any())
                return ObterNumeroInscricaoEstadualAleatoria();

            var random = new Random();

            var inscricaoSelecionada = inscricoesDoEstado[random.Next(inscricoesDoEstado.Count)];

            return inscricaoSelecionada.ObterValorNumerico();
        }

        private static string ObterValorNumerico(this EInscricaoEstadualBrasil inscricao)
        {
            return inscricao.ToString().Split('_')[1];
        }

        private static EInscricaoEstadualBrasil ObterInscricaoEstadualAleatoria()
        {
            var valores = Enum.GetValues(typeof(EInscricaoEstadualBrasil)).Cast<EInscricaoEstadualBrasil>();
            Random random = new Random();
            return valores.ElementAt(random.Next(valores.Count()));
        }
    }
}
