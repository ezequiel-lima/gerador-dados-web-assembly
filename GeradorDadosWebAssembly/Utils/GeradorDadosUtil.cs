﻿using Bogus;
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

        public static string GerarNif(Faker faker)
        {
            // Gera os primeiros 8 dígitos aleatórios (1 a 8 representam contribuinte individual)
            string nifBase = faker.Random.Number(10000000, 99999999).ToString();

            // Calcula o dígito de controle
            int soma = 0;
            for (int i = 0; i < nifBase.Length; i++)
            {
                int peso = 9 - i;
                soma += (nifBase[i] - '0') * peso;
            }

            int resto = soma % 11;
            int digitoControle = (resto == 0 || resto == 1) ? 0 : 11 - resto;

            // Retorna o NIF completo
            return $"{nifBase}{digitoControle}";
        }

        public static string GerarCnh(Faker faker)
        {
            // Gerar os nove primeiros dígitos da CNH
            string numeroBase = faker.Random.ReplaceNumbers("#########");

            int dv1 = 0, dv2 = 0;
            int nM1 = 9, nM2 = 1;

            // Cálculo inicial de DV1 e DV2
            for (int i = 0; i < numeroBase.Length; i++)
            {
                int nVL = numeroBase[i] - '0';
                dv1 += nVL * nM1;
                dv2 += nVL * nM2;
                nM1--;
                nM2++;
            }

            // Cálculo do primeiro dígito verificador (DV1)
            dv1 %= 11;
            bool lMaior = dv1 > 9;
            if (lMaior) dv1 = 0;

            // Cálculo do segundo dígito verificador (DV2) com ajuste
            dv2 %= 11;
            if (lMaior)
            {
                // Regras para ajuste de DV2 se DV1 > 9
                if (dv2 - 2 < 0)
                    dv2 += 9;
                else
                    dv2 -= 2;
            }

            if (dv2 > 9) dv2 = 0;

            // Retornar o número completo da CNH com os dígitos verificadores
            return $"{numeroBase}{dv1}{dv2}";
        }
    }
}
