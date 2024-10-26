using Bogus;
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
    }
}
