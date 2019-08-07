using ERPConnect.Linq;

namespace ConnectSAPCore.Infra.CrossCutting.Models
{
    [ERPTable("TCURT")]
    public class TipoMoneda
    {
        public string MANDT { get; set; }
        public string SPRAS { get; set; }
        public string WAERS { get; set; }
        public string LTEXT { get; set; }
        public string KTEXT { get; set; }
    }
}

