namespace GeradorDadosWebAssembly.Dtos
{
    public class PessoaDto
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string DataNascimento { get; set; }
        public string Genero { get; set; }
        public string Signo { get; set; }
        public string NomeMae { get; set; }
        public string NomePai { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public EnderecoDto Endereco { get; set; } = new EnderecoDto();
        public string NumeroEndereco { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Altura { get; set; }
        public string Peso { get; set; }
        public string TipoSanguineo { get; set; }
    }
}
