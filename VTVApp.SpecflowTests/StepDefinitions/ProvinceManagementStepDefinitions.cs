using System;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using VTVApp.Api;
using VTVApp.Api.Models.DTOs.Provinces;

namespace VTVApp.SpecflowTests.StepDefinitions
{
    [Binding]
    public class ProvinceManagementStepDefinitions
    {
        private readonly HttpClient _client;
        private HttpResponseMessage _response;
        private string _baseUrl;
        private IEnumerable<ProvinceDto>? _provinces;

        public ProvinceManagementStepDefinitions(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Given(@"the Provinces API is available")]
        public void GivenTheProvincesAPIIsAvailable()
        {
            _baseUrl = "/api/Provinces";
        }

        [When(@"I request a list of all provinces")]
        public async Task WhenIRequestAListOfAllProvinces()
        {
            _response = await _client.GetAsync($"{_baseUrl}");
        }

        [Then(@"I should receive a list of provinces")]
        public async Task ThenIShouldReceiveAListOfProvinces()
        {
            _response.EnsureSuccessStatusCode();
            var content = await _response.Content.ReadAsStringAsync();
            _provinces = JsonConvert.DeserializeObject<IEnumerable<ProvinceDto>>(content);
        }

        [Then(@"each province should include details like name and other relevant information")]
        public void ThenEachProvinceShouldIncludeDetailsLikeNameAndOtherRelevantInformation()
        {
            Assert.NotNull(_provinces);
            Assert.NotEmpty(_provinces);
            Assert.All(_provinces, province =>
            {
                Assert.NotNull(province.Name);
            });
        }
    }
}
