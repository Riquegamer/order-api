using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace domain.ValueObjects
{
    public record Email
    {
        public string Valor { get; private init; }
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
        public static implicit operator string(Email email) => email.Valor;
        public static implicit operator Email(string valor) => new(valor);
    }
}
