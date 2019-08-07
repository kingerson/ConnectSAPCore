using Autofac;
using ConnectSAPCore.Infra.CrossCutting.Sap;

namespace ConnectSAPCore.Service.Infrastructure.AutofacModule
{
    public class ServicesModule : Autofac.Module
    {
        public string _sapConnectionString { get; }
        public string _license { get; }
        public ServicesModule(string sapConnectionString,string license)
        {
            _sapConnectionString = sapConnectionString;
            _license = license;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new SapService(_sapConnectionString, _license))
                          .As<ISapService>()
                          .InstancePerLifetimeScope();
        }
    }
}
