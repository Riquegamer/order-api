using System.Net.Mail;

namespace domain.ValueObjects
{
    public record Email
    {
        #region Propriedades
        
        public string Valor { get; private init; }
        
        #endregion

        #region Construtores
        
        public Email(string valor)
        {
            if (string.IsNullOrEmpty(valor))
            {
                throw new ArgumentException("Email é obrigatório.", nameof(valor));
            }

            try
            {
                var email = new MailAddress(valor);
                Valor = email.Address;
            }
            catch (FormatException)
            {
                throw new ArgumentException("Email inválido.", nameof(valor));
            }
        }
        
        #endregion

        #region Operadores-Implicitos

        public static implicit operator string(Email email) => email.Valor;
        public static implicit operator Email(string valor) => new(valor);
        
        #endregion
    }
}
