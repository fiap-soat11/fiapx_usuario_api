using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.UseCases;
using Application.Configurations;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Test.UseCases
{
    [TestClass]
    public class AutenticacaoUseCaseTests
    {
        private AutenticacaoUseCase _autenticacaoUseCase;
        private JwtSettings _jwtSettings;

        [TestInitialize]
        public void Setup()
        {
            _jwtSettings = new JwtSettings
            {
                Key = "ChaveSecretaSuperSeguraComMaisDe256Bits1234567890",
                Issuer = "FiapUsuarioApi",
                Audience = "FiapUsuarioApiClients",
                ExpirationInMinutes = 60
            };

            var options = Options.Create(_jwtSettings);
            _autenticacaoUseCase = new AutenticacaoUseCase(options);
        }

        [TestMethod]
        public async Task GerarToken_ValidCredentials_ReturnsToken()
        {
            var email = "teste@email.com";
            var senha = "senha123";

            var token = await _autenticacaoUseCase.GerarToken(email, senha);

            Assert.IsNotNull(token);
            Assert.IsTrue(token.Length > 0);
        }

        [TestMethod]
        public async Task GerarToken_ValidCredentials_TokenHasCorrectClaims()
        {
            var email = "teste@email.com";
            var senha = "senha123";

            var token = await _autenticacaoUseCase.GerarToken(email, senha);

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            Assert.AreEqual(_jwtSettings.Issuer, jwtToken.Issuer);
            Assert.IsTrue(jwtToken.Claims.Any(c => c.Type == "email" && c.Value == email));
            Assert.IsTrue(jwtToken.Claims.Any(c => c.Type == "sub" && c.Value == email));
        }

        [TestMethod]
        public async Task GerarToken_ValidCredentials_TokenHasCorrectExpiration()
        {
            var email = "teste@email.com";
            var senha = "senha123";

            var beforeGeneration = DateTime.UtcNow;
            var token = await _autenticacaoUseCase.GerarToken(email, senha);
            var afterGeneration = DateTime.UtcNow;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var expectedExpiration = beforeGeneration.AddMinutes(_jwtSettings.ExpirationInMinutes);
            var timeDifference = Math.Abs((jwtToken.ValidTo - expectedExpiration).TotalSeconds);

            Assert.IsTrue(timeDifference < 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GerarToken_EmptyEmail_ThrowsException()
        {
            await _autenticacaoUseCase.GerarToken("", "senha123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GerarToken_NullEmail_ThrowsException()
        {
            await _autenticacaoUseCase.GerarToken(null, "senha123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GerarToken_WhitespaceEmail_ThrowsException()
        {
            await _autenticacaoUseCase.GerarToken("   ", "senha123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GerarToken_EmptyPassword_ThrowsException()
        {
            await _autenticacaoUseCase.GerarToken("teste@email.com", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GerarToken_NullPassword_ThrowsException()
        {
            await _autenticacaoUseCase.GerarToken("teste@email.com", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GerarToken_WhitespacePassword_ThrowsException()
        {
            await _autenticacaoUseCase.GerarToken("teste@email.com", "   ");
        }
    }
}
