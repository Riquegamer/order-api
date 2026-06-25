using application.DTOs.Entrada;
using FluentValidation;

namespace application.DTOs.Validations
{
    public class AtualizarClienteRequestValidator : AbstractValidator<AtualizarClienteRequest>
    {
        public AtualizarClienteRequestValidator() 
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
                .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");

            RuleFor(x => x.NomeFantasia)
                .MaximumLength(100).WithMessage("O nome fantasia deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("O documento do cliente é obrigatório.")
                .Length(11, 14).WithMessage("O documento deve conter 11 dígitos para CPF ou 14 dígitos para CNPJ.");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("O telefone do cliente é obrigatório.")
                .Length(10, 11).WithMessage("O telefone deve ter entre 10 e 11 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail do cliente é obrigatório.")
                .EmailAddress().WithMessage("O e-mail do cliente é inválido.");
        }
    }
}
