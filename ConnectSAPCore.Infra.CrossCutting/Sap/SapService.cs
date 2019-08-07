using ConnectSAPCore.Infra.CrossCutting.Models;
using ERPConnect;
using ERPConnect.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectSAPCore.Infra.CrossCutting.Sap
{
    public class SapService : ISapService
    {
        public string _sapConnectionString { get; }
        public string _license { get; }
        public SapService(string sapConnectionString, string license)
        {
            _sapConnectionString = !string.IsNullOrWhiteSpace(sapConnectionString) ? sapConnectionString : throw new ArgumentException(nameof(sapConnectionString));
            _license = !string.IsNullOrWhiteSpace(license) ? license : throw new ArgumentException(nameof(license));
            LIC.SetLic(_license);
        }

        public async Task<IEnumerable<TipoMoneda>> GetMoneda()
        {
            await Task.FromResult(0);
            IEnumerable<TipoMoneda> result;
            using (var sapHR = new ERPDataContext(_sapConnectionString))
            {
                var table = sapHR.GetTable<TipoMoneda>(false);
                result = table.Where(x => x.SPRAS.Equals("S")).ToList();
            }
            return result;
        }
    }
}
