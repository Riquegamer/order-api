using application.DTOs.Entrada;
using FluentValidation;

namespace application.DTOs.Validations
{
    public class CreateNegocioRequestValidator : AbstractValidator<CreateNegocioRequest>
    {
        public CreateNegocioRequestValidator()
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
                .Length(12, 13).WithMessage("O telefone deve ter entre 12 e 13 caracteres.");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.");
                //.Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$").WithMessage("A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula e um número.");
        }
    
}
}
