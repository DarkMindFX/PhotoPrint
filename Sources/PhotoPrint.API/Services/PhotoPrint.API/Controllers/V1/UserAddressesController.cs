


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using PPT.PhotoPrint.API.Filters;
using PPT.Interfaces.Entities;
using PPT.Utils.Convertors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;


namespace PPT.PhotoPrint.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class UserAddressesController : BaseController
    {
        private readonly Dal.IUserAddressDal _dalUserAddress;
        private readonly ILogger<UserAddressesController> _logger;


        public UserAddressesController( Dal.IUserAddressDal dalUserAddress,
                                    ILogger<UserAddressesController> logger)
        {
            _dalUserAddress = dalUserAddress; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalUserAddress.GetAll();

            IList<DTO.UserAddress> dtos = new List<DTO.UserAddress>();

            foreach (var p in entities)
            {
                var dto = UserAddressConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{userid}/{addressid}"), ActionName("GetUserAddress")]
        public IActionResult Get(System.Int64 userid, System.Int64 addressid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalUserAddress.Get(userid, addressid);
            if (entity != null)
            {
                var dto = UserAddressConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"UserAddress was not found [ids:{userid}, {addressid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

                //[Authorize]
        [HttpGet("/byuserid/:userid")]
        public IActionResult GetByUserID(System.Int64 userid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalUserAddress.GetByUserID(userid);

            IList<DTO.UserAddress> dtos = new List<DTO.UserAddress>();

            foreach (var p in entities)
            {
                var dto = UserAddressConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
                //[Authorize]
        [HttpGet("/byaddressid/:addressid")]
        public IActionResult GetByAddressID(System.Int64 addressid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalUserAddress.GetByAddressID(addressid);

            IList<DTO.UserAddress> dtos = new List<DTO.UserAddress>();

            foreach (var p in entities)
            {
                var dto = UserAddressConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        //[Authorize]
        [HttpDelete("{userid}/{addressid}"), ActionName("DeleteUserAddress")]
        public IActionResult Delete(System.Int64 userid, System.Int64 addressid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalUserAddress.Get(userid, addressid);

            if (existingEntity != null)
            {
                bool removed = _dalUserAddress.Delete(userid, addressid);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete UserAddress [ids:{userid}, {addressid}]");
                }
            }
            else
            {
                response = NotFound($"UserAddress not found [ids:{userid}, {addressid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertUserAddress")]
        public IActionResult Insert(DTO.UserAddress dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = UserAddressConvertor.Convert(dto);

            UserAddress newEntity = _dalUserAddress.Insert(entity);

            
            response = StatusCode((int)HttpStatusCode.Created, UserAddressConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateUserAddress")]
        public IActionResult Update(DTO.UserAddress dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = UserAddressConvertor.Convert(dto);

            var existingEntity = _dalUserAddress.Get(newEntity.UserID, newEntity.AddressID);           

            if (existingEntity != null)
            {
                                                    UserAddress entity = _dalUserAddress.Update(newEntity);

                response = Ok(UserAddressConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"UserAddress not found [ids:{newEntity.UserID}, {newEntity.AddressID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

