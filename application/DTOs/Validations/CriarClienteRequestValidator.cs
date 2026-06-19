using application.DTOs.Entrada;
using FluentValidation;

namespace application.DTOs.Validations
{
    public class CriarClienteRequestValidator : AbstractValidator<CriarClienteRequest>
    {
        public CriarClienteRequestValidator() 
        {
            RuleFor(x => x.nome)
                .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
                .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");
            
            RuleFor(x => x.nomeFantasia)
                .MaximumLength(100).WithMessage("O nome fantasia deve ter no máximo 100 caracteres.");

            RuleFor(x => x.documento)
                .NotEmpty().WithMessage("O documento do cliente é obrigatório.")
                .Length(11, 14).WithMessage("O documento deve conter 11 dígitos para CPF ou 14 dígitos para CNPJ.");

            RuleFor(x => x.telefone)
                .NotEmpty().WithMessage("O telefone do cliente é obrigatório.")
                .Length(12, 13).WithMessage("O telefone deve ter entre 12 e 13 caracteres.");

            RuleFor(x => x.email)
                .NotEmpty().WithMessage("O e-mail do cliente é obrigatório.")
                .EmailAddress().WithMessage("O e-mail do cliente é inválido.");
        }
    }
}
