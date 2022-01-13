


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

namespace PPT.Functions.Address.V1
{
    public class GetDetails : FunctionBase
    {
        private readonly IAddressDal _dalAddress;

        public GetDetails(IHttpContextAccessor httpContextAccessor, IAddressDal dalAddress) : base(httpContextAccessor)
        {
            _dalAddress = dalAddress;
        }

        [Authorize]    
        [FunctionName("AddressesGetDetails")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/addresses/{id}")] HttpRequest req,
            System.Int64? id,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new PPT.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var e = _dalAddress.Get(id);
                if (e != null)
                {
                    var dtos = AddressConvertor.Convert(e, null);

                    result = new OkObjectResult(funHelper.ToJosn(dtos));
                }
                else
                {
                    result = new ObjectResult(funHelper.ToJosn(new PPT.DTO.Error()
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        Message = $"Address was found, but item was not deleted [ids:{id}]"
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