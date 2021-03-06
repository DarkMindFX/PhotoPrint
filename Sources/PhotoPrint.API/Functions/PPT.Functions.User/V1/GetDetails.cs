using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PPT.Services.Dal;
using PPT.Utils.Convertors;
using System.Net;
using PPT.Functions.Common;

namespace PPT.Functions.User.V1
{
    public class GetDetails : FunctionBase
    {
        private readonly IUserDal _dalUser;

        public GetDetails(IHttpContextAccessor httpContextAccessor, IUserDal dalUser) : base(httpContextAccessor)
        {
            _dalUser = dalUser;
        }

        [Authorize]    
        [FunctionName("UsersGetDetails")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/users/{id}")] HttpRequest req,
            long id,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new PPT.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var user = _dalUser.Get(id);
                if (user != null)
                {
                    var dtos = UserConvertor.Convert(user, null);

                    result = new OkObjectResult(funHelper.ToJosn(dtos));
                }
                else
                {
                    result = new ObjectResult(funHelper.ToJosn(new PPT.DTO.Error()
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        Message = $"User was not found [ids:{id}]"
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
