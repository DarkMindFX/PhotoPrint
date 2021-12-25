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
using PPT.Interfaces;
using PPT.Services.Common.Helpers;

namespace PhotoPrint.Functions.Address.V1
{
    public static class Update
    {
        [FunctionName("AddressesUpdate")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "v1/addresses")] HttpRequest req,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new PPT.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var dal = funHelper.CreateDal<IAddressDal>();

                var content = await new StreamReader(req.Body).ReadToEndAsync();

                var dto = JsonConvert.DeserializeObject<PPT.DTO.Address>(content);

                var newEntity = AddressConvertor.Convert(dto);

                var existingEntity = dal.Get(newEntity.ID);

                if (existingEntity != null)
                {
                    newEntity.CreatedDate = existingEntity.CreatedDate;

                    funHelper.SetCreatedModifiedProperties(newEntity,
                                            "ModifiedDate",
                                            "ModifiedByID");
                    PPT.Interfaces.Entities.Address entity = dal.Update(newEntity);

                    result = new ObjectResult(funHelper.ToJosn(AddressConvertor.Convert(newEntity, null)));
                }
                else
                {
                    result = new ObjectResult(funHelper.ToJosn(new PPT.DTO.Error()
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        Message = $"Address was not found [ids:{newEntity.ID}]"
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
