using ERPConnect;
using ERPConnect.Utils;
using System;
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

        public async Task<string> GetMoneda()
        {
            await Task.FromResult(0);
            var mensaje = string.Empty;
            var trans = new Transaction();
            var r3Connection1 = new R3Connection(_sapConnectionString);
            trans.Connection = r3Connection1;
            trans.TCode = "PA30";
            trans.AddStepSetNewDynpro("SAPMP50A", "1000");
            trans.AddStepSetOKCode("=INS");
            trans.AddStepSetField("RP50G-PERNR", "20007955");
            trans.AddStepSetField("BDC_SUBSCR", "SAPMP50A                                0800SUBSCR_HEADER");
            trans.AddStepSetField("BDC_SUBSCR", "SAPMP50A                                0320SUBSCR_ITMENU");
            trans.AddStepSetField("BDC_SUBSCR", "SAPMP50A                                0330SUBSCR_TIME");
            trans.AddStepSetField("RP50G-TIMR6", "X");
            trans.AddStepSetField("RP50G-BEGDA", DateTime.Now.ToString("dd.MM.yyyy"));
            trans.AddStepSetField("RP50G-ENDDA", DateTime.Now.ToString("dd.MM.yyyy"));
            trans.AddStepSetField("BDC_SUBSCR", "SAPMP50A                                0350SUBSCR_ITKEYS");
            trans.AddStepSetCursor("RP50G-SUBTY");
            trans.AddStepSetField("RP50G-CHOIC", "2001");
            trans.AddStepSetField("RP50G-SUBTY", "1013");

            trans.AddStepSetNewDynpro("MP200000", "2000");
            trans.AddStepSetCursor("P2001-BEGDA");
            trans.AddStepSetOKCode("=UPD");
            trans.AddStepSetField("P2001-BEGDA", DateTime.Now.ToString("dd.MM.yyyy"));
            trans.AddStepSetField("P2001-ENDDA", DateTime.Now.ToString("dd.MM.yyyy"));

            trans.Connection.Open(false);

            trans.Execute();

            if (trans.Returns != null && trans.Returns.Count > 0)
            {
                foreach (BatchReturn item in trans.Returns)
                {
                    if (item.MessageID == "PG" && item.MessageNumber == "102")
                    {
                        mensaje = "OK";
                        break;
                    }
                    else
                    {
                        mensaje = mensaje + "|" + item.Message;
                    }
                }
            }

            return mensaje;
        }
    }
}
