using Bogus;
using Bogus.DataSets;
using GeradorDadosWebAssembly.Extensions;
using System.Text.RegularExpressions;

namespace GeradorDadosWebAssembly.Utils
{
    public static class GeradorDadosUtil
    {
        public static string GerarTelefone(Faker f)
        {
            var ddd = DddExtension.GetRandomDdd();
            var telefone = f.Phone.PhoneNumber("####-####");
            return $"({ddd.GetDddValue()}) {telefone}";
        }

        public static string GerarCelular(Faker f)
        {
            var ddd = DddExtension.GetRandomDdd();
            var celular = f.Phone.PhoneNumber("9####-####");
            return $"({ddd.GetDddValue()}) {celular}";
        }

        public static string GerarSite(string enderecoDoSite)
        {
            string site = Regex.Replace(enderecoDoSite.Trim().ToLower(), @"[^a-z0-9]", "");
            return $"www.{site}.com";
        }

        public static string GerarEmail(Faker f, string enderecoEmail)
        {
            string[] result = f.Internet.Email().Split("@");
            string email = Regex.Replace(enderecoEmail.Trim().ToLower(), @"[^a-z0-9]", "");
            return $"{email}@{result[1]}";
        }

        public static string GerarEnderecoPadraoBrasil(Faker f)
        {
            string[] result = f.Address.StreetName().Split(" ");
            return $"{result[1]} {result[0]}";
        }

        public static string TraduzirGenero(Name.Gender genero)
        {
            return genero == Name.Gender.Male ? "Masculino" : "Feminino";
        }

        public static string GerarTipoSanguineo(Faker f)
        {
            var tiposSanguineos = new[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            return f.PickRandom(tiposSanguineos);
        }

        public static string GerarSigno(Faker f)
        {
            var signos = new[]
            {
                "Áries", "Touro", "Gêmeos", "Câncer", "Leão", "Virgem",
                "Libra", "Escorpião", "Sagitário", "Capricórnio", "Aquário", "Peixes"
            };
            return f.PickRandom(signos);
        }

        public static string GerarRgValido()
        {
            var random = new Random();
            var rgBase = random.Next(10000000, 99999999).ToString(); 
            int[] pesos = { 2, 3, 4, 5, 6, 7, 8, 9 };
            int soma = 0;

            for (int i = 0; i < rgBase.Length; i++)
            {
                soma += (rgBase[i] - '0') * pesos[i];
            }

            int resto = soma % 11;
            int digitoVerificador = 11 - resto;

            if (digitoVerificador == 10 || digitoVerificador == 11)
            {
                digitoVerificador = 0;
            }

            return $"{rgBase.Substring(0, 2)}.{rgBase.Substring(2, 3)}.{rgBase.Substring(5, 3)}-{digitoVerificador}";
        }
    }
}
