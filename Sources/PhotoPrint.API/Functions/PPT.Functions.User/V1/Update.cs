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
using System.Net;
using PPT.Services.Dal;
using PPT.Services.Common.Helpers;
using PPT.Functions.Common;

namespace PPT.Functions.User.V1
{
    public class Update : FunctionBase
    {
        private readonly IUserDal _dalUser;

        public Update(IHttpContextAccessor httpContextAccessor,
            IUserDal dalUser) : base(httpContextAccessor)
        {
            _dalUser = dalUser;
        }

        [Authorize]
        [FunctionName("UsersUpdate")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "v1/users")] HttpRequest req,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new PPT.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var content = await new StreamReader(req.Body).ReadToEndAsync();

                var dto = JsonConvert.DeserializeObject<PPT.DTO.User>(content);

                var newEntity = UserConvertor.Convert(dto);

                var existingEntity = _dalUser.Get(newEntity.ID);

                if (existingEntity != null)
                {
                    if (!string.IsNullOrEmpty(dto.Password))
                    {
                        newEntity.PwdHash = PasswordHelper.GenerateHash(dto.Password, existingEntity.Salt);
                    }

                    newEntity.CreatedDate = existingEntity.CreatedDate;

                    funHelper.SetCreatedModifiedProperties(newEntity,
                                            "ModifiedDate",
                                            "ModifiedByID",
                                            (PPT.Interfaces.Entities.User)req.HttpContext.Items["User"]);
                    PPT.Interfaces.Entities.User entity = _dalUser.Update(newEntity);

                    result = new ObjectResult(funHelper.ToJosn(UserConvertor.Convert(newEntity, null)))
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }
                else
                {
                    result = new ObjectResult(funHelper.ToJosn(new PPT.DTO.Error()
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        Message = $"User was not found [ids:{newEntity.ID}]"
                    }))
                    {
                        StatusCode = (int)HttpStatusCode.NotFound
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
