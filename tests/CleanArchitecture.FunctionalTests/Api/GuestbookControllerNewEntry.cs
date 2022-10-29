using CleanArchitecture.Core.Entities;
using CleanArchitecture.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.FunctionalTests.Api
{
    public class GuestbookControllerNewEntry : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private string _emailAddress = "mehdi6384@gmail.com";
        private string _message = "this message is test";

        public GuestbookControllerNewEntry(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        private StringContent GetEntryAsJson()
        {
            var entryToPost = new GuestbookEntry { EmailAddress = _emailAddress, Message = _message };
            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(entryToPost), 
                Encoding.UTF8, 
                "application/json");
            return jsonContent;
        }

        [Fact]
        public async Task Returns404GivenInvalidGuestbookId()
        {
            var invalidId = -1;
            var jsonContent = GetEntryAsJson();
            var response = await _client.PostAsync($"/api/guestbook/{invalidId}/NewEntry", jsonContent);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsGuestbookAndEntryGivenValidId()
        {
            var validId = 1;

            var jsonContent = GetEntryAsJson();
            var response = await _client.PostAsync($"/api/guestbook/{validId}/NewEntry", jsonContent);

            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Guestbook>(stringResponse);

            Assert.Equal(_emailAddress, result.Entries.Last().EmailAddress);
            Assert.Equal(_message, result.Entries.Last().Message);
        }
    }
}
