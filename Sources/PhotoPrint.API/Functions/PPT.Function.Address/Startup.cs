


using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PPT.Functions.Common;

[assembly: FunctionsStartup(typeof(PPT.Functions.Address.Startup))]
namespace PPT.Functions.Address
{
    public class Startup : FunctionStartupBase
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            base.Configure(builder);

            var dalAddressDal = InitDal<Interfaces.IAddressDal>();
            builder.Services.AddSingleton<Interfaces.IAddressDal>(dalAddressDal);
            builder.Services.AddSingleton<PPT.Services.Dal.IAddressDal, PPT.Services.Dal.AddressDal>();            
        }
    }
}