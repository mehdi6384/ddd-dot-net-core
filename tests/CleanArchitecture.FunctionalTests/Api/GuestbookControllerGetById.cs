using CleanArchitecture.Core.Entities;
using CleanArchitecture.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.FunctionalTests.Api
{
    public class GuestbookControllerGetById : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GuestbookControllerGetById(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Returns404GivenInvalidGuestbookId()
        {
            var invalidId = -1;
            var response = await _client.GetAsync($"/api/guestbook/{invalidId}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsGuestbookAndEntryGivenValidId()
        {
            var validId = 1;
            var response = await _client.GetAsync($"/api/guestbook/{validId}");

            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Guestbook>(stringResponse);

            Assert.Equal("Test", result.Name);
            Assert.Equal("test@test.com", result.Entries.First().EmailAddress);
            Assert.Equal("Test", result.Entries.First().Message);
        }
    }
}
