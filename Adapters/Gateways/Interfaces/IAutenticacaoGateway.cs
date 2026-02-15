using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapters.Gateways.Interfaces
{
    public interface IAutenticacaoGateway
    {
        Task GerarToken(Usuario cliente);
    }
}
