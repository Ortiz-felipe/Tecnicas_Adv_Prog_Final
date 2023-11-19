using System;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using VTVApp.Api;
using VTVApp.Api.Models.DTOs.Cities;

namespace VTVApp.SpecflowTests.StepDefinitions
{
    [Binding]
    public class CityManagementStepDefinitions
    {
        private readonly HttpClient _client;
        private HttpResponseMessage _response;
        private string _baseUrl;
        private IEnumerable<CityDetailsDto>? _cities;

        public CityManagementStepDefinitions(CustomWebApplicationFactory<Program> client)
        {
            _client = client.CreateClient();
        }

        [Given(@"the Cities API is available")]
        public void GivenTheCitiesAPIIsAvailable()
        {
            _baseUrl = "/api/Cities";
        }

        [When(@"I request a list of all cities")]
        public async Task WhenIRequestAListOfAllCities()
        {
            _response = await _client.GetAsync($"{_baseUrl}?ProvinceId=1");
        }

        [Then(@"I should receive a list of cities")]
        public async Task ThenIShouldReceiveAListOfCities()
        {
            _response.EnsureSuccessStatusCode();
            var content = await _response.Content.ReadAsStringAsync();
            _cities = JsonConvert.DeserializeObject<IEnumerable<CityDetailsDto>>(content);
        }

        [Then(@"each city should include details like name and province")]
        public void ThenEachCityShouldIncludeDetailsLikeNameAndProvince()
        {
            Assert.NotNull(_cities);
            Assert.All(_cities, city =>
            {
                Assert.NotNull(city.Name);
                Assert.NotNull(city.Id);
                Assert.NotNull(city.Province);
                Assert.NotNull(city.Province.Name);
            });
        }
    }
}
