using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PPT.Services.Dal;
using System.Collections.Generic;
using PPT.Utils.Convertors;
using System;
using PPT.Functions.Common;

namespace PPT.Functions.User.V1
{
    public class GetAll : FunctionBase
    {
        private readonly IUserDal _dalUser;
        public GetAll(IHttpContextAccessor httpContextAccessor, IUserDal dalUser) : base(httpContextAccessor)
        {
            _dalUser = dalUser;
        }

        [Authorize]
        [FunctionName("UsersGetAll")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/users")] HttpRequest req,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new PPT.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var users = _dalUser.GetAll();
                var dtos = new List<PPT.DTO.User>();
                foreach (var user in users)
                {
                    dtos.Add(UserConvertor.Convert(user, null));
                }

                result = new OkObjectResult(funHelper.ToJosn(dtos));
            }
            catch(Exception ex)
            {
                log.LogError(ex.ToString());
            }

            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return result;
        }
    }
}
