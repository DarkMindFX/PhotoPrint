using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PPT.Interfaces;
using PPT.PhotoPrint.API.Helpers;
using PPT.Utils.Convertors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PPT.PhotoPrint.API.Controllers.V1
{
    [Route("api/v1/thumbnails")]
    [ApiController]
    public class RpcThumbnailOperationsController : ControllerBase
    {
        private readonly Dal.IImageDal _dalImage;
        private readonly Dal.IImageThumbnailDal _dalImageThumbnail;
        private readonly ILogger<RpcThumbnailOperationsController> _logger;
        private readonly IBinaryStorage _storage;
        private readonly IOptions<AppSettings> _appSettings;


        public RpcThumbnailOperationsController(Dal.IImageDal dalImage,
                                        Dal.IImageThumbnailDal dalImageThumbnail,
                                        ILogger<RpcThumbnailOperationsController> logger,
                                        IBinaryStorage storage,
                                        IOptions<AppSettings> appSettings)
        {
            _dalImage = dalImage;
            _dalImageThumbnail = dalImageThumbnail;
            _logger = logger;
            _storage = storage;
            _appSettings = appSettings;
        }

        [Authorize]
        [HttpPost("images/{id}"), ActionName("UploadImage")]
        public IActionResult UploadImageThumbnail(System.Int64 id, List<IFormFile> files)
        {
            IActionResult result = null;
            var image = _dalImage.Get(id);
            if (image != null)
            {
                if (files.Count < 1)
                {
                    result = StatusCode((int)HttpStatusCode.BadRequest, $"No humbnail content was provided for image {id}");
                }
                else
                {
                    foreach (var file in files)
                    {
                        string blobName = BuildBlobFileName(file.FileName);
                        var url = _storage.Upload(blobName, file.OpenReadStream());

                        var thumbnail = new Interfaces.Entities.ImageThumbnail()
                        {
                            ImageID = (long)image.ID,
                            Order = 0,
                            Url = url
                        };
                        var newThumbnail = _dalImageThumbnail.Insert(thumbnail);

                        result = StatusCode((int)HttpStatusCode.Created, ImageThumbnailConvertor.Convert(newThumbnail, this.Url));
                    }
                }
            }

            return result;
        }

        #region Support methods
        private string BuildBlobFileName(string fileName)
        {
            return System.IO.Path.GetFileNameWithoutExtension(fileName) + "-" + Guid.NewGuid() + System.IO.Path.GetExtension(fileName); 
        }
        #endregion
    }
}
