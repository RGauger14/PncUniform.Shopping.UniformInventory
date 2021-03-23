using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PncUniform.Shopping.UniformInventory.Application.Uniforms.Commands;
using Shouldly;
using Xunit;

namespace PncUniform.Shopping.UniformInventory.Application.Tests.Uniforms
{
    public class CreateUniformIntergrationTest : BaseIntegrationTest
    {
        [Fact]
        public async Task CanSucsessfullyCreateUniform_WhenGivenValidData()
        {
            //ARANGE
            var client = _testServer.CreateClient();
            var createUniform = new CreateUniformCommand
            {
                 Description = "Bucket Hat",
                 Size = "Medium",
                 Price = 20,
                 StockLevel = 5,
                 Campus = "Both",
                 Barcode = "1234567890111",
                 VendorBarcode = "1234567890111"
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(createUniform), Encoding.UTF8, MediaTypeNames.Application.Json);

            // ACT
            var response = await client.PostAsync("api/uniform/create", requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
