using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PncUniform.Shopping.UniformInventory.Application.Domain.Entities;
using PncUniform.Shopping.UniformInventory.Application.Uniforms.Commands;
using Xunit;

namespace PncUniform.Shopping.UniformInventory.Application.Tests.Uniforms.QueryTests
{
    public class FindAllUnifromsIntergrationTest : BaseIntegrationTest
    {
        [Fact]
        public async Task CanSucsessfullyFindAllUniforms_WhenGivenValidData()
        {
            // Arrange
            var client = _testServer.CreateClient();
            var uniformsToFind = await CreateUniformsToFindForTestAsync(client, numberOfUniforms: 2);

            // Act
            var response = await client.GetAsync("api/uniform/findAll");

            // Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            var uniforms = JsonConvert.DeserializeObject<IEnumerable<Uniform>>(responseContent);
            Assert.Equal(uniformsToFind.Count(), uniforms.Count());

            var actualUniforms = uniforms.OrderBy(u => u.UniformId).ToList();
            var expectedUniforms = uniformsToFind.OrderBy(u => u.UniformId).ToList();

            for (int i = 0; i < actualUniforms.Count; i++)
            {
                var actualUniform = actualUniforms[i];
                var expectedUniform = expectedUniforms[i];
                Assert.Equal(expectedUniform.UniformId, actualUniform.UniformId);
                Assert.Equal(expectedUniform.Description, actualUniform.Description);
                Assert.Equal(expectedUniform.Size, actualUniform.Size);
                Assert.Equal(expectedUniform.Price, actualUniform.Price);
                Assert.Equal(expectedUniform.StockLevel, actualUniform.StockLevel);
                Assert.Equal(expectedUniform.Campus, actualUniform.Campus);
                Assert.Equal(expectedUniform.Barcode, actualUniform.Barcode);
                Assert.Equal(expectedUniform.VendorBarcode, actualUniform.VendorBarcode);
            }
        }

        private async Task<IEnumerable<Uniform>> CreateUniformsToFindForTestAsync(HttpClient client, int numberOfUniforms)
        {
            var uniformCommands = new List<CreateUniformCommand>();
            var sizes = new string[] { "Medium", "L", "XXL" };
            var campuses = new string[] { "Primary", "Middle", "Senior" };

            for (int i = 0; i < numberOfUniforms; i++)
            {
                var description = $"description{i}";
                var size = sizes[i % sizes.Length];
                var price = 44.50m;
                var stockLevel = 16;
                var campus = campuses[i % campuses.Length];
                var barcode = $"{i % 10}{i % 3}{i % 10}{i % 6}451657773";
                var vendorBarcode = $"{i % 3}{i % 10}{i % 6}{i % 10}456157123";

                var createUniformCommand = new CreateUniformCommand
                {
                    Description = description,
                    Size = size,
                    Price = price,
                    StockLevel = stockLevel,
                    Campus = campus,
                    Barcode = barcode,
                    VendorBarcode = vendorBarcode
                };

                uniformCommands.Add(createUniformCommand);
            }

            var uniforms = new List<Uniform>();

            foreach (var uniformCommand in uniformCommands)
            {
                var uniform = await CreateUniformForTestAsync(client, uniformCommand);
                uniforms.Add(uniform);
            }

            return uniforms;
        }

        private async Task<Uniform> CreateUniformForTestAsync(HttpClient client, CreateUniformCommand uniformCommand)
        {
            var requestContent = new StringContent(JsonConvert.SerializeObject(uniformCommand), Encoding.UTF8, MediaTypeNames.Application.Json);
            var responseMessage = await client.PostAsync("api/uniform/create", requestContent);
            var responceContent = await responseMessage.Content.ReadAsStringAsync();
            var createdUniform = JsonConvert.DeserializeObject<Uniform>(responceContent);

            return createdUniform;
        }
    }
}