


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
using PPT.Services.Common.Helpers;
using PPT.Services.Dal;
using System.Net;
using PPT.Functions.Common;

namespace PPT.Functions.Address.V1
{
    public class Insert : FunctionBase
    {
        private readonly IAddressDal _dalAddress;

        public Insert(IHttpContextAccessor httpContextAccessor, IAddressDal dalAddress) : base(httpContextAccessor)
        {
            _dalAddress = dalAddress;
        }

        [Authorize]
        [FunctionName("AddressesInsert")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/addresses")] HttpRequest req,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new PPT.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var content = await new StreamReader(req.Body).ReadToEndAsync();

                var dto = JsonConvert.DeserializeObject<PPT.DTO.Address>(content);

                var entity = AddressConvertor.Convert(dto);

								funHelper.SetCreatedModifiedProperties(entity, 
										"CreatedDate", 
										"CreatedByID",
										(PPT.Interfaces.Entities.User)req.HttpContext.Items["User"]); 
				
                PPT.Interfaces.Entities.Address newEntity = _dalAddress.Insert(entity);

                if (newEntity != null)
                {
                    result = new ObjectResult(funHelper.ToJosn(AddressConvertor.Convert(newEntity, null)))
                    {
                        StatusCode = (int)HttpStatusCode.Created
                    };
                }
                else
                {
                    result = new ObjectResult(funHelper.ToJosn(new PPT.DTO.Error()
                    {
                        Code = (int)HttpStatusCode.InternalServerError,
                        Message = $"Something went wrong. Address was not inserted."
                    }))
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
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