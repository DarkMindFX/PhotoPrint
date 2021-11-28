using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PPT.PhotoPrint.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PPT.PhotoPrint.API.Controllers.V1
{
    [Route("api/v1/images")]
    [ApiController]
    public class RpcImageOperationsController : ControllerBase
    {
        private readonly Dal.IImageDal _dalImage;
        private readonly ILogger<RpcImageOperationsController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public RpcImageOperationsController(Dal.IImageDal dalImage,
                                        ILogger<RpcImageOperationsController> logger,
                                        IOptions<AppSettings> appSettings)
        {
            _dalImage = dalImage;
            _logger = logger;
            _appSettings = appSettings;
        }

        [Authorize]
        [HttpPost("{id}/thumbnail"), ActionName("UploadImage")]
        public async Task<IActionResult> UploadImageThumbnail(System.Int64 id, List<IFormFile> files)
        {
            IActionResult result = null;
            var entity = _dalImage.Get(id);
            if (entity != null)
            {
                if (files.Count < 1)
                {
                    result = StatusCode((int)HttpStatusCode.BadRequest, $"No humbnail content was provided for image {id}");
                }
                else
                {
                    foreach (var file in files)
                    {
                    }
                }
            }

            return result;
        }
    }
}
