using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PPT.Interfaces;
using System.Collections.Generic;
using PPT.Utils.Convertors;
using System;

namespace PPT.Functions.Address.V1
{
    public class GetAll
    {
        [FunctionName("AddressesGetAll")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/addresses")] HttpRequest req,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new PPT.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var dal = funHelper.CreateDal<IAddressDal>();

                var users = dal.GetAll();
                var dtos = new List<PPT.DTO.Address>();
                foreach (var user in users)
                {
                    dtos.Add(AddressConvertor.Convert(user, null));
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
