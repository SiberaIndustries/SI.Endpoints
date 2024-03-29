using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using SI.Endpoints.Sample;
using SI.Endpoints.Sample.Endpoints.TodoItem;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using Xunit;

namespace SI.Endpoints.Tests
{
    public class EndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;
        private readonly JsonSerializerOptions settings = new(JsonSerializerDefaults.Web);

        public EndpointTests(WebApplicationFactory<Program> factory)
        {
            this.factory = factory;
        }

        private HttpClient CreateCustomClient(bool useFeatures = true, bool ignoreNames = false, bool replaceTags = true, string prefix = "", string mode = "")
        {
            return factory.WithWebHostBuilder(builder => builder
                .UseSetting("mode", mode)
                .UseSetting("useFeatures", useFeatures.ToString())
                .UseSetting("ignoreNames", ignoreNames.ToString())
                .UseSetting("replaceTags", replaceTags.ToString())
                .UseSetting("prefix", prefix))
                .CreateClient();
        }

        [Theory]
        [InlineData(true, false, "", "/TodoItem/Get/", "")]
        [InlineData(false, false, "", "/Get/", "")]
        [InlineData(true, true, "", "/TodoItem/", "")]
        [InlineData(true, false, "API", "/API/TodoItem/Get/", "")]
        [InlineData(true, true, "API", "/API/TodoItem/", "")]
        [InlineData(true, false, "", "/TodoItem/Get/", "nswag")]
        [InlineData(false, false, "", "/Get/", "nswag")]
        [InlineData(true, true, "", "/TodoItem/", "nswag")]
        [InlineData(true, false, "API", "/API/TodoItem/Get/", "nswag")]
        [InlineData(true, true, "API", "/API/TodoItem/", "nswag")]
        public async Task GetResponseShouldSuccessfulWhenIdExists(bool useFeatures, bool ignoreNames, string prefix, string expectedUri, string mode)
        {
            // Arrange
            var id = 42;
            using var client = CreateCustomClient(useFeatures, ignoreNames, false, prefix, mode);
           

            // Act
            var response = await client.GetAsync(expectedUri + id);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var responseContent = JsonSerializer.Deserialize<GetTodoItemResponse>(content, settings);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseContent.Id.Should().Be(42);
        }

        [Theory]
        [InlineData("")]
        [InlineData("nswag")]
        public async Task GetResponseShouldBeNotFoundWhenIdNotExists(string mode)
        {
            // Arrange
            var id = 43;
            using var client = CreateCustomClient(mode: mode);

            // Act
            var response = await client.GetAsync($"/TodoItem/Get/" + id);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("")]
        [InlineData("nswag")]
        public async Task GetSwaggerDocWithFeatureFilterEnabled(string mode)
        {
            // Arrange
            using var client = CreateCustomClient(mode: mode, replaceTags: true);

            // Act
            var response = await client.GetAsync("/swagger/v1/swagger.json");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var responseContent = JsonSerializer.Deserialize<JsonObject>(content, settings);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseContent["info"]?["title"].Should().NotBeNull();
            responseContent["paths"]?["/TodoItem/Create"]?["post"]?["tags"]?.AsArray()?.Select(x => x.ToString())
                .Should().NotBeNull().And.BeEquivalentTo("TodoItem");
        }

        [Theory]
        [InlineData("")]
        [InlineData("nswag")]
        public async Task GetSwaggerDocWithFeatureFilterDisabled(string mode)
        {
            // Arrange
            using var client = CreateCustomClient(mode: mode, replaceTags: false);

            // Act
            var response = await client.GetAsync("/swagger/v1/swagger.json");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var responseContent = JsonSerializer.Deserialize<JsonObject>(content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseContent["info"]?["title"].Should().NotBeNull();
            responseContent["paths"]?["/TodoItem/Create"]?["post"]?["tags"]?.AsArray()?.Select(x => x.ToString())
                .Should().NotBeNull().And.BeEquivalentTo("TodoItem", "Create");
        }

        [Theory]
        [InlineData("")]
        [InlineData("nswag")]
        public async Task ListResponseShouldBeSuccessful(string mode)
        {
            // Arrange
            using var client = CreateCustomClient(mode: mode);

            // Act
            var response = await client.GetAsync($"/TodoItem/List/", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var todos = JsonSerializer.DeserializeAsyncEnumerable<ListTodoItemResponse>(stream, settings);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var count = 0;
            await foreach (var todo in todos)
            {
                todo.Id.Should().Be(++count);
            }
        }
    }
}
