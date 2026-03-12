using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Adapters.Presenters.Autenticacao
{
    public class AutenticacaoResponse
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
        public string Token { get; set; } = null!;
        public string TokenType { get; set; } = "Bearer";
        public DateTime ExpiresAt { get; set; }
        public string Email { get; set; } = null!;
        public string Nome { get; set; } = null!;
    }
}
