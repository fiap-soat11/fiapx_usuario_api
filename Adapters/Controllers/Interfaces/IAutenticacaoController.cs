using Adapters.Presenters.Autenticacao;
using Adapters.Presenters.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapters.Controllers.Interfaces
{
    public interface IAutenticacaoController
    {
        Task<AutenticacaoResponse> GerarToken(AutenticacaoRequest autenticacao);
    }
}
