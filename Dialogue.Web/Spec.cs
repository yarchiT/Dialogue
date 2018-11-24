using System;
using SutStartup = Dialogue.Web.Startup;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Dialogue.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using System.Net;

namespace Dialogue.Web
{
    public class Spec :
    IDisposable,
    IClassFixture<WebApplicationFactory<SutStartup>>
    {
        private readonly WebApplicationFactory<SutStartup> _factory;

        public Spec(WebApplicationFactory<SutStartup> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseSolutionRelativeContentRoot("./Dialogue.Web/");
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<DialogueAppContext, TestContext>(options =>
                    options.UseSqlite("Data Source=test.db"));
                });
            });
        }

        [Fact]
        public async Task CanCrudStoriesAndFeatures()
        {
            var client = new LeanwareTestClient(_factory);

            var featureTitle = RandomFeatureTitle;
            var feature = new
            {
                Id = 0,
                Title = featureTitle,
                Tags = RandomFeatureTags
            };

            feature = await client.PostSelf("api/features", feature);

            feature.Id.Should().BeGreaterThan(0);
            feature.Title.Should().Be(featureTitle);

            var featurePath = $"api/features/{feature.Id}";

            var updatedFeature = new
            {
                Title = $"{featureTitle} and {featureTitle}"
            };

            await client.Patch(featurePath, updatedFeature);

            feature = await client.Get(featurePath, feature);

            feature.Title.Should().Be($"{featureTitle} and {featureTitle}");

            var storyTitle = RandomStoryTitle;

            var story = new
            {
                Id = 0,
                Title = storyTitle,
                Description = nameof(CanCrudStoriesAndFeatures),
                Tags = RandomStoryTags,
                FeatureId = feature.Id
            };

            story = await client.PostSelf("api/stories", story);

            story.Id.Should().BeGreaterThan(0);

            var storyPath = $"api/stories/{story.Id}";

            story.Title.Should().Be(storyTitle);

            var updatedStory = new
            {
                Title = $"{storyTitle} and {storyTitle}"
            };

            await client.Patch(storyPath, updatedStory);

            story = await client.Get(storyPath, story);

            story.Title.Should().Be($"{storyTitle} and {storyTitle}");

            await client.Delete(storyPath);

            var deletedStory = await client.Get(storyPath);
            deletedStory.StatusCode.Should().Be(HttpStatusCode.NotFound);

            await client.Delete(featurePath);

            var deletedFeature = await client.Get(featurePath);
            deletedFeature.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        public void Dispose()
        {
            using (var db = new TestContext())
            {
            }
        }


        string RandomStoryTitle => $"US{DateTime.Now.Ticks}";
        List<string> RandomStoryTags => Guid.NewGuid().ToString().Split('-').ToList();

        string RandomFeatureTitle => $"US{DateTime.Now.Ticks}";
        List<string> RandomFeatureTags => Guid.NewGuid().ToString().Split('-').ToList();

    }

    internal class LeanwareTestClient
    {
        private readonly HttpClient _client;

        public LeanwareTestClient(WebApplicationFactory<SutStartup> factory)
        {
            this._client = factory.CreateClient();
        }


        internal async Task<HttpResponseMessage> Post<T>(string path, T dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, path);

            request.Content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return response;
        }

        internal async Task<T> PostSelf<T>(string path, T dto)
        {
            var response = await Post(path, dto);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeAnonymousType(content, dto);
        }

        internal async Task<HttpResponseMessage> Patch<T>(string path, T dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, path);

            request.Content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return response;
        }

        internal async Task<HttpResponseMessage> Delete(string path)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, path);

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return response;
        }

        internal async Task<T> Get<T>(string path, T obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, path);

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeAnonymousType(responseContent, obj);
        }

        internal async Task<HttpResponseMessage> Get(string path)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, path);

            var response = await _client.SendAsync(request);

            return response;
        }
    }

    internal class TestContext : DialogueAppContext
    {
        public TestContext()
        {
        }

        public TestContext(DbContextOptions<DialogueAppContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=test.db");
        }
    }
}