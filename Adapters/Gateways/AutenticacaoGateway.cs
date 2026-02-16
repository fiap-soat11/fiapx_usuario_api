using System.Collections.Generic;
using System.Threading.Tasks;
using Adapters.Gateways.Interfaces;
using Domain;

namespace Adapters.Gateways
{
    public class AutenticacaoGateway : IAutenticacaoGateway
    {
        private readonly IDataSource _autenticacaoDataSource;

        public AutenticacaoGateway(IDataSource autenticacaoDataSource)
        {
            _autenticacaoDataSource = autenticacaoDataSource;
        }

        public Task GerarToken(Usuario cliente)
        {
            throw new NotImplementedException();
        }
    }
}