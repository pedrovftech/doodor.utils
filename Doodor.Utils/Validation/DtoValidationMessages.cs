namespace Doodor.Utils.Utilities.Validation
{
    public static class DtoValidationMessages
    {
        public static string CaracteresInvalidos { get; } = "Caracteres inválidos";
        public static string NaoPodeSerNuloMessage { get; } = "Não pode ser nulo.";
        public static string NaoPodeSerNuloOuVazioMessage { get; } = "Não pode ser nulo ou vazio.";       
        public static string CpfInvalido { get; } = "CPF inválido.";
        public static string CnpjInvalido { get; } = "CNPJ inválido.";
        public static string EnumInvalido { get; } = "Enum inválido.";
        public static string EmailInvalido { get; } = "E-mail inválido.";
        public static string TelefoneInvalido { get; } = "Telefone inválido.";
        public static string DataInvalida { get; set; } = "Data inválida";
        public static string SenhaInvalida { get; } = "Senha não atende aos critérios. (A senha deve conter de 8 à 32 caracteres, no mínimo 1 letra maiúscula, 1 letra minúscula, 1 número e 1 caractere especial)";
        public static string CamposDevemSerIguais { get; } = "Os campos devem ser iguais.";
        public static string ValorDeveraSerMaiorQueZero { get; } = "O Valor informado deverá ser maior que ZERO.";
        public static string CepInvalido { get; } = "CEP inválido.";
        public static string CaractereMaximo { get; } = "Foi atingindo o número máximo de caractere.";      
    }
}