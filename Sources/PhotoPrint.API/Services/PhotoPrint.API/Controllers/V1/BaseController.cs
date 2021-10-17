using PPT.Interfaces.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPT.PhotoPrint.API.Controllers.V1
{
    public class BaseController : ControllerBase
    {
        public User CurrentUser
        {
            get
            {
                return HttpContext.Items["User"] as User;
            }
        }
    }
}
