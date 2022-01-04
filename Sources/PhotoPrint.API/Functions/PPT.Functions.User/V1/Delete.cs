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
using System.Net;

namespace PhotoPrint.Functions.User.V1
{
    public static class Delete
    {
        [FunctionName("UsersDelete")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "v1/users/{id}")] HttpRequest req,
            long id,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new PPT.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var dal = funHelper.CreateDal<IUserDal>();

                var user = dal.Get(id);
                if (user != null)
                {
                    bool isRemoved = dal.Delete(id);

                    if (isRemoved)
                    {
                        result = new OkResult();
                    }
                    else
                    {
                        result = new ObjectResult(funHelper.ToJosn(new PPT.DTO.Error()
                        {
                            Code = (int)HttpStatusCode.InternalServerError,
                            Message = $"Something went wrong. User was found, but was not deleted [ids:{id}]"
                        }))
                        {
                            StatusCode = (int)HttpStatusCode.InternalServerError
                        };
                    }
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