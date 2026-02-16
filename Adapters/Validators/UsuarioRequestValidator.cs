using FluentValidation;
using System.Net;
using Adapters.Presenters.Usuario;

namespace Adapters.Validators
{
    public class UsuarioRequestValidator : AbstractValidator<UsuarioRequest>
    {

        public UsuarioRequestValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("A senha é obrigatória.")
                .MinimumLength(8)
                .WithMessage("A senha deve conter no mínimo 8 caracteres.")
                .Must(IsValidPassword)
                .WithMessage("A senha deve conter ao menos uma letra e um número.");
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O Nome é obrigatório.");
            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("O e-mail deve ser válido.");
        }

        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            bool hasLetter = password.Any(char.IsLetter);
            bool hasDigit = password.Any(char.IsDigit);

            return hasLetter && hasDigit;
        }
    }
}
