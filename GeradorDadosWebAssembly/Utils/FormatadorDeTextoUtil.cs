namespace GeradorDadosWebAssembly.Utils
{
    public static class FormatadorDeTextoUtil
    {
        public static string RemoverPontuacao(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            texto = texto
                .Replace("/", "")
                .Replace("-", "")
                .Replace(".", "")
                .Replace(",", "")
                .Replace("(", "")
                .Replace(")", "");

            return texto;
        }
    }
}
