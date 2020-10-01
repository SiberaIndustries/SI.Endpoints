using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SI.Endpoints.Sample;
using SI.Endpoints.Sample.Endpoints.TodoItem;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace SI.Endpoints.Tests
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public UnitTest1(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task GetResponseShouldSuccessfulWhenIdExists()
        {
            // Arrange
            var id = 42;
            using var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/TodoItem/Get/" + id);
            var content = await response.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<GetTodoItemResponse>(content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseContent.Id.Should().Be(42);
        }

        [Fact]
        public async Task GetResponseShouldBeNotFoundWhenIdNotExists()
        {
            // Arrange
            var id = 43;
            using var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/TodoItem/Get/" + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
