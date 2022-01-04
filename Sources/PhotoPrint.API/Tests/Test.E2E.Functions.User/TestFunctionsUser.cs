using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Test.Functions.Common;

namespace Test.E2E.Functions.User
{
    public class TestUserFunctions
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task UsersGetAll_Success()
        {
            var request = TestFactory.CreateHttpRequest();
            var response = (ObjectResult)await PPT.Functions.User.V1.GetAll.Run(request, logger);

            Assert.IsNotNull(response);
            Assert.Equals((int)HttpStatusCode.OK, response.StatusCode);

            var dtos = JsonSerializer.Deserialize<List<PPT.DTO.User>>(response.Value.ToString());
            
        }
    }
}