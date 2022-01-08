using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PPT.Utils.Convertors;
using PPT.Services.Common.Helpers;
using PPT.Interfaces;
using System.Net;
using PPT.Functions.Common;

namespace PPT.Functions.User.V1
{
    public class Insert : FunctionBase
    {
        public Insert(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        [Authorize]
        [FunctionName("UsersInsert")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/users")] HttpRequest req,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new PPT.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var dal = funHelper.CreateDal<IUserDal>();

                var content = await new StreamReader(req.Body).ReadToEndAsync();

                var dto = JsonConvert.DeserializeObject<PPT.DTO.User>(content);

                var entity = UserConvertor.Convert(dto);
                entity.Salt = PasswordHelper.GenerateSalt(12);
                entity.PwdHash = PasswordHelper.GenerateHash(dto.Password, entity.Salt);

                funHelper.SetCreatedModifiedProperties(entity,
                            "CreatedDate",
                            null);

                PPT.Interfaces.Entities.User newEntity = dal.Insert(entity);

                if (newEntity != null)
                {
                    result = new ObjectResult(funHelper.ToJosn(UserConvertor.Convert(newEntity, null)))
                    {
                        StatusCode = (int)HttpStatusCode.Created
                    };
                }
                else
                {
                    result = new ObjectResult(funHelper.ToJosn(new PPT.DTO.Error()
                    {
                        Code = (int)HttpStatusCode.InternalServerError,
                        Message = $"Something went wrong. User was not inserted."
                    }))
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
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
