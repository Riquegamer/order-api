namespace domain.ValueObjects
{
    public record Documento
    {
        public string Valor { get; private set; }
        public bool IsCnpj => Valor.Length == 14;

        private Documento(string valor)
        {
            Valor = valor;
        }

        public static Documento Criar(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
                throw new ArgumentNullException("O documento não pode ser vazio.");

            string apenasNumeros = new string(numero.Where(char.IsDigit).ToArray());

            if (apenasNumeros.Length == 11)
            {
                if (!ValidarCpf(apenasNumeros))
                    throw new ArgumentException("O CPF informado é inválido.");
                return new Documento(apenasNumeros);
            }
            else if (apenasNumeros.Length == 14)
            {
                if (!ValidarCnpj(apenasNumeros))
                    throw new ArgumentException("O CNPJ informado é inválido.");

                return new Documento(apenasNumeros);
            }
            else
            {
                throw new ArgumentException("O documento deve ser um CPF (11 dígitos) ou CNPJ (14 dígitos)");
            }
        }

        private static bool ValidarCpf(string cpf)
        {
            if (cpf.Distinct().Count() == 1) return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto <2 ? 0 : 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        private static bool ValidarCnpj(string cnpj)
        {
            if (cnpj.Distinct().Count() == 1) return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public string ObterFormatado()
        {
            if (IsCnpj)
                return Convert.ToUInt64(Valor).ToString(@"00\.000\.000\/0000\-00");

            return Convert.ToUInt64(Valor).ToString(@"000\.000\.000\-00");
        }

        public override string ToString() => Valor;
    }

}
