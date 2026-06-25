using System.Text.RegularExpressions;

namespace domain.ValueObjects
{
    public record Telefone
    {
        public string Valor { get; private set; }

        private static readonly Regex TelefoneRegex = new Regex(@"^(?:[1-9]{2})(?:9[1-9][0-9]{3}[0-9]{4}|[2-8][0-9]{3}[0-9]{4})$", RegexOptions.Compiled);

        private Telefone(string valor)
        {
            Valor = valor;
        }

        public static Telefone Criar(string numero)
        {
            if (string.IsNullOrEmpty(numero))
                throw new ArgumentNullException("O telefone não pode ser vazio.");

            string apenasNumeros = new string(numero.Where(char.IsDigit).ToArray());

            if (!TelefoneRegex.IsMatch(apenasNumeros))
                throw new ArgumentException("O número de telefone informado é invalido");

            return new Telefone(apenasNumeros);
        }

        public string ObterFormatado()
        {
            // Celular
            if (Valor.Length == 11)
                return Regex.Replace(Valor, @"^(\d{2})(\d{5})(\d{4})$", "($1) $2-$3");

            // Telefone Fixo
            return Regex.Replace(Valor, @"^(\d{2})(\d{4})(\d{4})$", "($1) $2-$3");
        }

        public override string ToString() => Valor;
    }
}
