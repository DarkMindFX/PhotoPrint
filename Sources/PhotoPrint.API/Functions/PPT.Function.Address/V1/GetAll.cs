


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

namespace PPT.Functions.Address.V1
{
    public class GetAll : FunctionBase
    {
        private readonly IUserDal _dalAddress;
        public GetAll(IHttpContextAccessor httpContextAccessor, IUserDal dalAddress) : base(httpContextAccessor)
        {
            _dalAddress = dalAddress;
        }

        [Authorize]
        [FunctionName("AddressesGetAll")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/addresses")] HttpRequest req,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new PPT.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var entities = _dalAddress.GetAll();
                var dtos = new List<PPT.DTO.Address>();
                foreach (var e in entities)
                {
                    dtos.Add(AddressConvertor.Convert(e, null));
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

