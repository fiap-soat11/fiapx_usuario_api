using Microsoft.VisualStudio.TestTools.UnitTesting;
using Adapters.Presenters.Usuario;
using Adapters.Validators;
using FluentValidation.TestHelper;

namespace Application.Test.Validators
{
    [TestClass]
    public class UsuarioRequestValidatorTests
    {
        private UsuarioRequestValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new UsuarioRequestValidator();
        }

        [TestMethod]
        public void Validate_ValidRequest_PassesValidation()
        {
            var request = new UsuarioRequest
            {
                Nome = "João Silva",
                Email = "joao@email.com",
                Password = "senha123"
            };

            var result = _validator.TestValidate(request);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod]
        public void Validate_EmptyPassword_FailsValidation()
        {
            var request = new UsuarioRequest
            {
                Nome = "João Silva",
                Email = "joao@email.com",
                Password = ""
            };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("A senha é obrigatória.");
        }

        [TestMethod]
        public void Validate_ShortPassword_FailsValidation()
        {
            var request = new UsuarioRequest
            {
                Nome = "João Silva",
                Email = "joao@email.com",
                Password = "abc123"
            };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("A senha deve conter no mínimo 8 caracteres.");
        }

        [TestMethod]
        public void Validate_PasswordWithoutLetter_FailsValidation()
        {
            var request = new UsuarioRequest
            {
                Nome = "João Silva",
                Email = "joao@email.com",
                Password = "12345678"
            };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("A senha deve conter ao menos uma letra e um número.");
        }

        [TestMethod]
        public void Validate_PasswordWithoutDigit_FailsValidation()
        {
            var request = new UsuarioRequest
            {
                Nome = "João Silva",
                Email = "joao@email.com",
                Password = "abcdefgh"
            };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("A senha deve conter ao menos uma letra e um número.");
        }

        [TestMethod]
        public void Validate_EmptyNome_FailsValidation()
        {
            var request = new UsuarioRequest
            {
                Nome = "",
                Email = "joao@email.com",
                Password = "senha123"
            };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Nome)
                .WithErrorMessage("O Nome é obrigatório.");
        }

        [TestMethod]
        public void Validate_NullNome_FailsValidation()
        {
            var request = new UsuarioRequest
            {
                Nome = null,
                Email = "joao@email.com",
                Password = "senha123"
            };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Nome);
        }

        [TestMethod]
        public void Validate_InvalidEmail_FailsValidation()
        {
            var request = new UsuarioRequest
            {
                Nome = "João Silva",
                Email = "emailinvalido",
                Password = "senha123"
            };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("O e-mail deve ser válido.");
        }

        [TestMethod]
        public void Validate_EmptyEmail_PassesValidation()
        {
            var request = new UsuarioRequest
            {
                Nome = "João Silva",
                Email = "",
                Password = "senha123"
            };

            var result = _validator.TestValidate(request);

            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [TestMethod]
        public void Validate_NullEmail_PassesValidation()
        {
            var request = new UsuarioRequest
            {
                Nome = "João Silva",
                Email = null,
                Password = "senha123"
            };

            var result = _validator.TestValidate(request);

            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [TestMethod]
        public void Validate_ValidEmailFormat_PassesValidation()
        {
            var request = new UsuarioRequest
            {
                Nome = "João Silva",
                Email = "joao.silva@example.com",
                Password = "senha123"
            };

            var result = _validator.TestValidate(request);

            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }
    }
}
