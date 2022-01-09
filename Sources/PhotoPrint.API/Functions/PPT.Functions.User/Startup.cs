using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PPT.Functions.Common;

[assembly: FunctionsStartup(typeof(PPT.Functions.User.Startup))]
namespace PPT.Functions.User
{
    public class Startup : FunctionStartupBase
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            base.Configure(builder);

            var dalUserDal = InitDal<Interfaces.IUserDal>();
            builder.Services.AddSingleton<Interfaces.IUserDal>(dalUserDal);
            builder.Services.AddSingleton<PPT.Services.Dal.IUserDal, PPT.Services.Dal.UserDal>();
            
            var dalContactDal = InitDal<Interfaces.IContactDal>();
            builder.Services.AddSingleton<Interfaces.IContactDal>(dalContactDal);
            builder.Services.AddSingleton<PPT.Services.Dal.IContactDal, PPT.Services.Dal.ContactDal>();
            
            var dalUserContactDal = InitDal<Interfaces.IUserContactDal>();
            builder.Services.AddSingleton<Interfaces.IUserContactDal>(dalUserContactDal);
            builder.Services.AddSingleton<PPT.Services.Dal.IUserContactDal, PPT.Services.Dal.UserContactDal>();
            
        }
    }
}
