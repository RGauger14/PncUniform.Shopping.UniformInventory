using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PncUniform.Shopping.UniformInventory.Application.Domain.Entities;
using PncUniform.Shopping.UniformInventory.Application.Uniforms.Commands;
using Shouldly;
using Xunit;

namespace PncUniform.Shopping.UniformInventory.Application.Tests.Uniforms.CommandTests
{

    public class UpdateUniformIntergrationTest : BaseIntegrationTest
    {
        //1. Seed uniform and return it
        //2. Send update uniform request
        //3. Compare the uniform you got back from the update against the one you seeded

        [Fact]
        public async Task CanSucsessfullyUpdateUniform_WhenGivenValidData()
        {
            //ARANGE
            var client = _testServer.CreateClient();
            var createdUniform = await CreateUniformForTestAsync(client);
            var expectedUniform = new UpdateUniformCommand
            {
                UniformId = createdUniform.UniformId,
                Description = "Junior Sports Shirt",
                Size = "14",
                Price = 44.50m,
                StockLevel = 10,
                Campus = "Both",
                Barcode = "1234567890112",
                VendorBarcode = "1234567890112"
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(expectedUniform), Encoding.UTF8, MediaTypeNames.Application.Json);

            // ACT
            var response = await client.PostAsync("api/uniform/update", requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            // I can't validate that the update happened without first gettingg the data back again
            // deserialise the uniform back from the Update request var actualUniform = JsonConvert<Uniform> (response.content blah)
            // validate each property on the uniform is as expected
            

            // actualUniform.Description.ShouldBe(expectedUniform.Description);
        }

        private static async Task<Uniform> CreateUniformForTestAsync(HttpClient client)
        {
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
            var responseMessage = await client.PostAsync("api/uniform/create", requestContent);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var createdUniform = JsonConvert.DeserializeObject<Uniform>(responseContent);

            return createdUniform;
        }
    }
}
