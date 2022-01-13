


using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PPT.Services.Dal;
using System.Net;
using PPT.Functions.Common;

namespace PPT.Functions.Address.V1
{
    public class Delete : FunctionBase
    {
        private readonly IAddressDal _dalAddress;

        public Delete(IHttpContextAccessor httpContextAccessor, IAddressDal dalAddress) : base(httpContextAccessor)
        {
            _dalAddress = dalAddress;
        }

        [Authorize]
        [FunctionName("AddressesDelete")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "v1/addresses/{id}")] HttpRequest req,
            System.Int64? id,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new PPT.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var user = _dalAddress.Get(id);
                if (user != null)
                {
                    bool isRemoved = _dalAddress.Delete(id);

                    if (isRemoved)
                    {
                        result = new OkResult();
                    }
                    else
                    {
                        result = new ObjectResult(funHelper.ToJosn(new PPT.DTO.Error()
                        {
                            Code = (int)HttpStatusCode.InternalServerError,
                            Message = $"Address was found, but item was not deleted [ids:{id}]"
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
                        Message = $"Address was not found [ids:{id}]"
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
