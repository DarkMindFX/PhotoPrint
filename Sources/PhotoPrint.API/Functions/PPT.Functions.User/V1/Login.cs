using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PPT.Interfaces;
using System.Linq;
using PPT.Services.Common.Helpers;
using PhotoPrint.Services.Common.Helpers;
using PPT.Utils.Convertors;
using System.Net;

namespace PhotoPrint.Functions.User.V1
{
    public static class Login
    {
        [FunctionName("Login")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/users/login")] HttpRequest req,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new PPT.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var dalUsers = funHelper.CreateDal<IUserDal>();                

                var content = await new StreamReader(req.Body).ReadToEndAsync();

                var dtoLogin = JsonConvert.DeserializeObject<PPT.DTO.LoginRequest>(content);

                var existingEntity = dalUsers.GetAll().FirstOrDefault(u => u.Login.ToLower() == dtoLogin.Login.ToLower());
                if (existingEntity != null)
                {
                    string pwdHash = PasswordHelper.GenerateHash(dtoLogin.Password, existingEntity.Salt);
                    if (pwdHash.Equals(existingEntity.PwdHash))
                    {
                        // Creating token
                        var jwtHelper = new JWTHelper();
                        var dtExpires = DateTime.Now.AddSeconds(funHelper.GetEnvironmentVariable<int>(PPT.Functions.Common.Constants.ENV_JWT_SECRET));
                        var sToken = jwtHelper.GenerateToken(existingEntity, dtExpires, funHelper.GetEnvironmentVariable<string>(PPT.Functions.Common.Constants.ENV_SESSION_TIMEOUT));

                        // Creating response object
                        var dtoResponse = new PPT.DTO.LoginResponse()
                        {
                            User = UserConvertor.Convert(existingEntity, null),
                            Token = sToken,
                            Expires = dtExpires
                        };

                        result = funHelper.CreateResult(HttpStatusCode.OK, dtoResponse);
                    }
                    else
                    {
                        result = funHelper.CreateResult(HttpStatusCode.Forbidden);
                    }
                }
                else
                {
                    result = funHelper.CreateResult(HttpStatusCode.NotFound);
                }

            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
            }

            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return result;
        }
    }
}
