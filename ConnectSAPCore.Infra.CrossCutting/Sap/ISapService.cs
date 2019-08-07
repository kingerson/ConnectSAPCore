using ConnectSAPCore.Infra.CrossCutting.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectSAPCore.Infra.CrossCutting.Sap
{
    public interface ISapService
    {
        Task<IEnumerable<TipoMoneda>> GetMoneda();
    }
}
