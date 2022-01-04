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
using System.Collections.Generic;
using PPT.Utils.Convertors;
using System.Net;

namespace PhotoPrint.Functions.Address.V1
{
    public class GetDetails
    {
        [FunctionName("AddressesGetDetails")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/addresses/{id}")] HttpRequest req,
            long id,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new PPT.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var dal = funHelper.CreateDal<IAddressDal>();

                var user = dal.Get(id);
                if (user != null)
                {
                    var dtos = AddressConvertor.Convert(user, null);

                    result = new OkObjectResult(funHelper.ToJosn(dtos));
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