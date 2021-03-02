using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PncUniform.Shopping.UniformInventory.Application.Customers.Commands;
using Shouldly;
using Xunit;

namespace PncUniform.Shopping.UniformInventory.Application.Tests.Customers
{
    public class CreateCustomerIntegrationTest : BaseIntegrationTest
    {
        [Fact]
        public async Task CanSuccessfullyCreateCustomer_WhenGivenValidData()
        {
            // ARRANGE
            var client = _testServer.CreateClient();
            var createCustomer = new CreateCustomerCommand
            {
                Email = "test@example.com",
                MobileNumber = "(00) 00 000 000",
                Name = "John Citizen"
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, MediaTypeNames.Application.Json);

            // ACT
            var response = await client.PostAsync("api/customer/create", requestContent);

            // ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
