using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SI.Endpoints.Core;
using SI.Endpoints.Sample;
using SI.Endpoints.Sample.Endpoints.TodoItem;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace SI.Endpoints.Tests
{
    public class EndpointTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public EndpointTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Theory]
        [InlineData(true, false, "", "/TodoItem/Get/")]
        [InlineData(false, false, "", "/Get/")]
        [InlineData(true, true, "", "/TodoItem/")]
        [InlineData(true, false, "API", "/API/TodoItem/Get/")]
        [InlineData(true, true, "API", "/API/TodoItem/")]
        public async Task GetResponseShouldSuccessfulWhenIdExists(bool useFeatures, bool ignoreEndpointNames, string prefix, string expectedUri)
        {
            // Arrange
            var id = 42;
            using var client = factory.WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(EndpointRoutingConvention));
                services.Remove(descriptor);

                services.AddEndpointsRouting(x =>
                {
                    x.UseFeatures(useFeatures);
                    x.IgnoreEndpointNames(ignoreEndpointNames);
                    x.WithPrefix(prefix);
                });
            })).CreateClient();

            // Act
            var response = await client.GetAsync(expectedUri + id);
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
