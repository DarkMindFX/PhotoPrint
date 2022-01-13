



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

namespace PPT.Functions.Address.V1
{
    public class Update : FunctionBase
    {
        private readonly IAddressDal _dalAddress;

        public Update(IHttpContextAccessor httpContextAccessor,
            IAddressDal dalAddress) : base(httpContextAccessor)
        {
            _dalAddress = dalAddress;
        }

        [Authorize]
        [FunctionName("AddressesUpdate")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "v1/addresses")] HttpRequest req,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new PPT.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var content = await new StreamReader(req.Body).ReadToEndAsync();

                var dto = JsonConvert.DeserializeObject<PPT.DTO.Address>(content);

                var newEntity = AddressConvertor.Convert(dto);

                var existingEntity = _dalAddress.Get(newEntity.ID);

                if (existingEntity != null)
                {
                    newEntity.CreatedDate = existingEntity.CreatedDate;
                    newEntity.CreatedByID = existingEntity.CreatedByID;

                    funHelper.SetCreatedModifiedProperties(newEntity,
                                            "ModifiedDate",
                                            "ModifiedByID",
                                            (PPT.Interfaces.Entities.User)req.HttpContext.Items["User"]);

                    PPT.Interfaces.Entities.Address entity = _dalAddress.Update(newEntity);

                    result = new ObjectResult(funHelper.ToJosn(AddressConvertor.Convert(newEntity, null)))
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };
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
