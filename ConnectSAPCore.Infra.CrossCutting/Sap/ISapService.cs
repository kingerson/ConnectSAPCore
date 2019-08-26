using System.Threading.Tasks;

namespace ConnectSAPCore.Infra.CrossCutting.Sap
{
    public interface ISapService
    {
        Task<string> GetMoneda();
    }
}
