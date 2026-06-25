using application.DTOs.Entrada;
using FluentValidation;

namespace application.DTOs.Validations
{
    public class AtualizarNegocioRequestValidator : AbstractValidator<AtualizarNegocioRequest>
    {
        public AtualizarNegocioRequestValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome do negócio é obrigatório.")
                .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O formato do e-mail informado é inválido.");

            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("O documento (CPF/CNPJ) é obrigatório.")
                .Length(11, 14).WithMessage("O documento deve conter 11 dígitos para CPF ou 14 dígitos para CNPJ.");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .Length(10, 11).WithMessage("O telefone deve ter entre 10 e 11 caracteres.");
        }
    }
}
