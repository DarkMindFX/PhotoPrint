using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PPT.Functions.Common;
using PPT.Test.Functions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PPT.Test.E2E.Functions
{
    public abstract class FunctionTestBase
    {
        public class TestParams
        {
            public TestParams()
            {
                Settings = new Dictionary<string, object>();
            }

            public Dictionary<string, object> Settings
            {
                get;
                set;
            }
        }

        protected TestParams _testParams;

        protected TestParams GetTestParams(string name)
        {
            TestParams testParams = new TestParams();

            var config = GetConfiguration();

            testParams.Settings = config.GetSection(name).GetChildren().ToDictionary(x => x.Key, x => (object)x.Value);

            return testParams;
        }

        protected IConfiguration GetConfiguration()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return config;
        }

        protected TFunc GetFunction<TFunc>(IHost host)
        {
            Type type = typeof(TFunc);

            ConstructorInfo contructorInfo = type.GetConstructors().FirstOrDefault();

            ParameterInfo[] parametersInfo = contructorInfo.GetParameters();

            object[] parameters = LookupServiceInstances(host, parametersInfo);

            return (TFunc)Activator.CreateInstance(type, parameters);
        }

        private object[] LookupServiceInstances(IHost host, IReadOnlyList<ParameterInfo> parametersInfo)
        {
            return parametersInfo.Select(p => host.Services.GetService(p.ParameterType))
                                 .ToArray();
        }

        protected TFunc GetFunction<TFunc, TParam1, TParam2, TParam3>(IHost host) where TFunc : FunctionBase
        {
            TFunc fun = (TFunc)Activator.CreateInstance(typeof(TFunc), new object[] {
                new HttpContextAccessor(),
                host.Services.GetRequiredService<TParam1>(),
                host.Services.GetRequiredService<TParam2>(),
                host.Services.GetRequiredService<TParam3>()
            });

            return fun;
        }

        protected async Task<PPT.DTO.LoginResponse> Login(string login, string password, IHost host, ILogger logger)
        {

            var dtoLogin = new PPT.DTO.LoginRequest()
            {
                Login = login,
                Password = password
            };
            var request = TestFactory.CreateHttpRequest(dtoLogin, null);
            var response = (ObjectResult)await(GetFunction<PPT.Functions.User.V1.Login>(host)).Run(request, logger);

            var dtoResp = JsonSerializer.Deserialize<PPT.DTO.LoginResponse>(response.Value.ToString());

            return dtoResp;

        }
    }
}
