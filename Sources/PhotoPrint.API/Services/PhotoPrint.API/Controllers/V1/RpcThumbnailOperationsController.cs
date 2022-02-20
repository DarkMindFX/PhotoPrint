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
        private readonly PPT.Services.Dal.IImageDal _dalImage;
        private readonly PPT.Services.Dal.IImageThumbnailDal _dalImageThumbnail;
        private readonly PPT.Services.Dal.IFrameTypeDal _dalFrameType;
        private readonly PPT.Services.Dal.IMatDal _dalMat;
        private readonly PPT.Services.Dal.IMaterialTypeDal _dalMaterialType;
        private readonly PPT.Services.Dal.IMountingTypeDal _dalMountingType;
        private readonly PPT.Services.Dal.IUserInteriorThumbnailDal _dalUserInteriorThumbnail;
        private readonly PPT.Services.Dal.IUserDal _dalUser;
        private readonly ILogger<RpcThumbnailOperationsController> _logger;
        private readonly IBinaryStorage _storage;
        private readonly IOptions<AppSettings> _appSettings;

        private IDictionary<Type, object> _dals = new Dictionary<Type, object>();


        public RpcThumbnailOperationsController(PPT.Services.Dal.IImageDal dalImage,
                                        PPT.Services.Dal.IImageThumbnailDal dalImageThumbnail,
                                        PPT.Services.Dal.IFrameTypeDal dalFrameType,
                                        PPT.Services.Dal.IMatDal dalMat,
                                        PPT.Services.Dal.IMaterialTypeDal dalMaterialType,
                                        PPT.Services.Dal.IMountingTypeDal dalMountingType,
                                        PPT.Services.Dal.IUserInteriorThumbnailDal dalUserInteriorThumbnail,
                                        PPT.Services.Dal.IUserDal dalUser,
                                        ILogger<RpcThumbnailOperationsController> logger,
                                        IBinaryStorage storage,
                                        IOptions<AppSettings> appSettings)
        {
            _dalImage = dalImage;
            _dalImageThumbnail = dalImageThumbnail;
            _dalFrameType = dalFrameType;
            _dalMat = dalMat;
            _dalMaterialType = dalMaterialType;
            _dalMountingType = dalMountingType;
            _dalUser = dalUser;
            _dalUserInteriorThumbnail = dalUserInteriorThumbnail;

            _logger = logger;
            _storage = storage;
            _appSettings = appSettings;

            _dals.Add(typeof(PPT.Services.Dal.IFrameTypeDal), _dalFrameType);
            _dals.Add(typeof(PPT.Services.Dal.IMatDal), _dalMat);
            _dals.Add(typeof(PPT.Services.Dal.IMaterialTypeDal), _dalMaterialType);
            _dals.Add(typeof(PPT.Services.Dal.IMountingTypeDal), _dalMountingType);

        }

        [Authorize]
        [HttpPost("images/{id}"), ActionName("UploadImageTHumbnails")]
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

        [Authorize]
        [HttpPost("frametypes/{id}"), ActionName("UploadFrameTypeThumbnails")]
        public IActionResult UploadFrameTypeThumbnail(System.Int64 id, List<IFormFile> files)
        {
            IActionResult result = null;
            var frameType = _dalFrameType.Get(id);
            if (frameType != null)
            {
                if (files.Count < 1)
                {
                    result = StatusCode((int)HttpStatusCode.BadRequest, $"No thumbnail content was provided for frame type {id}");
                }
                else
                {
                    string blobName = BuildBlobFileName(files[0].FileName);
                    var url = _storage.Upload(blobName, files[0].OpenReadStream());

                    frameType.ThumbnailUrl = url;

                    var newThumbnail = _dalFrameType.Update(frameType);

                    result = StatusCode((int)HttpStatusCode.Created, FrameTypeConvertor.Convert(newThumbnail, this.Url));
                }
            }

            return result;
        }

        [Authorize]
        [HttpPost("mats/{id}"), ActionName("UploadMatThumbnails")]
        public IActionResult UploadMatThumbnail(System.Int64 id, List<IFormFile> files)
        {
            IActionResult result = null;
            var mat = _dalMat.Get(id);
            if (mat != null)
            {
                if (files.Count < 1)
                {
                    result = StatusCode((int)HttpStatusCode.BadRequest, $"No thumbnail content was provided for mat {id}");
                }
                else
                {
                    string blobName = BuildBlobFileName(files[0].FileName);
                    var url = _storage.Upload(blobName, files[0].OpenReadStream());

                    mat.ThumbnailUrl = url;

                    var newThumbnail = _dalMat.Update(mat);

                    result = StatusCode((int)HttpStatusCode.Created, MatConvertor.Convert(newThumbnail, this.Url));
                }
            }

            return result;
        }

        [Authorize]
        [HttpPost("materialtypes/{id}"), ActionName("UploadMaterialTypesThumbnails")]
        public IActionResult UploadMaterialTypeThumbnail(System.Int64 id, List<IFormFile> files)
        {
            IActionResult result = null;
            var materialType = _dalMaterialType.Get(id);
            if (materialType != null)
            {
                if (files.Count < 1)
                {
                    result = StatusCode((int)HttpStatusCode.BadRequest, $"No thumbnail content was provided for material type {id}");
                }
                else
                {
                    string blobName = BuildBlobFileName(files[0].FileName);
                    var url = _storage.Upload(blobName, files[0].OpenReadStream());

                    materialType.ThumbnailUrl = url;

                    var newThumbnail = _dalMaterialType.Update(materialType);

                    result = StatusCode((int)HttpStatusCode.Created, MaterialTypeConvertor.Convert(newThumbnail, this.Url));
                }
            }

            return result;
        }

        [Authorize]
        [HttpPost("mountingtypes/{id}"), ActionName("UploadMountingTypesThumbnails")]
        public IActionResult UploadMountingTypeThumbnail(System.Int64 id, List<IFormFile> files)
        {
            IActionResult result = null;
            var mountingType = _dalMountingType.Get(id);
            if (mountingType != null)
            {
                if (files.Count < 1)
                {
                    result = StatusCode((int)HttpStatusCode.BadRequest, $"No thumbnail content was provided for mounting type {id}");
                }
                else
                {
                    string blobName = BuildBlobFileName(files[0].FileName);
                    var url = _storage.Upload(blobName, files[0].OpenReadStream());

                    mountingType.ThumbnailUrl = url;

                    var newThumbnail = _dalMountingType.Update(mountingType);

                    result = StatusCode((int)HttpStatusCode.Created, MountingTypeConvertor.Convert(newThumbnail, this.Url));
                }
            }

            return result;
        }

        [Authorize]
        [HttpPost("userinteriors/{id}"), ActionName("UploadUserInteriorThumbnails")]
        public IActionResult UploadUserInteriorThumbnails(System.Int64 id, List<IFormFile> files)
        {
            IActionResult result = null;
            var user = _dalUser.Get(id);
            if (user != null)
            {
                if (files.Count < 1)
                {
                    result = StatusCode((int)HttpStatusCode.BadRequest, $"No humbnail content was provided for user {id}");
                }
                else
                {
                    foreach (var file in files)
                    {
                        string blobName = BuildBlobFileName(file.FileName);
                        var url = _storage.Upload(blobName, file.OpenReadStream());

                        var thumbnail = new Interfaces.Entities.UserInteriorThumbnail()
                        {
                            UserID = (long)user.ID,
                            Url = url
                        };
                        var newThumbnail = _dalUserInteriorThumbnail.Insert(thumbnail);

                        result = StatusCode((int)HttpStatusCode.Created, UserInteriorThumbnailConvertor.Convert(newThumbnail, this.Url));
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
