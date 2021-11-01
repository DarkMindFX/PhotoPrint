using Microsoft.AspNetCore.Mvc.Testing;
using PhotoPrint.Test.E2E.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.E2E.API.Controllers.V1
{
    public class TestRpcUserOperationsController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestRpcUserOperationsController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void User_Register_Success()
        {
        }

        [Fact]
        public void User_Register_LoginAlreadyExists()
        {
        }

        [Fact]
        public void User_Register_EmailAlreadyExists()
        {
        }

        [Fact]
        public void User_Register_InvalidUserTypeID()
        {
        }

        [Fact]
        public void User_Register_InvalidUserStatusID()
        {
        }

        [Fact]
        public void User_Login_Success()
        {
        }

        [Fact]
        public void User_Login_InvalidLogin()
        {
        }


        [Fact]
        public void User_Login_InvalidPassword()
        {
        }

        [Fact]
        public void User_Login_InvalidEmpty()
        {
        }

        [Fact]
        public void User_Login_PasswordEmpty()
        {
        }
    }
}
