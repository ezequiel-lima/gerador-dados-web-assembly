using System.Text.Json.Serialization;

namespace GeradorDadosWebAssembly.Dtos
{
    public class EnderecoDto
    {
        [JsonPropertyName("cep")]
        public string Cep { get; set; }

        [JsonPropertyName("state")]
        public string Estado { get; set; }

        [JsonPropertyName("city")]
        public string Cidade { get; set; }

        [JsonPropertyName("neighborhood")]
        public string Bairro { get; set; }

        [JsonPropertyName("street")]
        public string Rua { get; set; }
    }
}
