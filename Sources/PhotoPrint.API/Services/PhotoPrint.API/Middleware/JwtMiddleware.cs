﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPT.PhotoPrint.API.Helpers;
using PPT.Services.Dal;
using PPT.Services.Common.Helpers;

namespace PPT.PhotoPrint.API.MiddleWare
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserDal dalUser)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                setUserContext(context, token, dalUser);
            }

            await _next(context);
        }

        private void setUserContext(HttpContext context, string token, IUserDal dalUser)
        { 
            Interfaces.Entities.User currentUser = JWTHelper.GetUserFromToken(token, _appSettings.Secret, dalUser);
            
            context.Items["User"] = currentUser;
        }
    }
}
