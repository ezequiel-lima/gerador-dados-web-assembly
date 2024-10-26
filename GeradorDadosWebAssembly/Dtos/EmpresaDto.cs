namespace GeradorDadosWebAssembly.Dtos
{
    public class EmpresaDto
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string InscricaoEstadual { get; set; }
        public string DataAbertura { get; set; }
        public string Site { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string NumeroEndereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public List<SocioDto> Socio { get; set; } = new List<SocioDto>();
    }
}
