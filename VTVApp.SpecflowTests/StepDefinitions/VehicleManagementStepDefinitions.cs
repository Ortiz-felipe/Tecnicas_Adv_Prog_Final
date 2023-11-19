using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using VTVApp.Api;
using VTVApp.Api.Commands.Vehicles.CreateVehicle;
using VTVApp.Api.Models.DTOs.Vehicle;

namespace VTVApp.SpecflowTests.StepDefinitions
{
    [Binding]
    public class VehicleManagementStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HttpClient _client;
        private HttpResponseMessage _response;
        private readonly string _baseUrl = "/api/Vehicles";
        private Guid _userId;
        private Guid _vehicleId;

        public VehicleManagementStepDefinitions(ScenarioContext scenarioContext, CustomWebApplicationFactory<Program> client)
        {
            _scenarioContext = scenarioContext;
            _client = client.CreateClient();
        }

        [Given(@"a user with a specific ID")]
        public void GivenAUserWithASpecificID()
        {
            _userId = _scenarioContext.Get<Guid>("RegisteredUserId");
        }

        [When(@"I submit a request to create a vehicle with valid details for the user")]
        public async Task WhenISubmitARequestToCreateAVehicleWithValidDetailsForTheUser()
        {
            var vehicle = new CreateVehicleDto()
            {
                UserId = _userId,
                Brand = "Honda",
                Model = "Civic",
                Year = 2015,
                LicensePlate = "ABC123",
                Color = "Black",
            };
            var content = new StringContent(JsonConvert.SerializeObject(new CreateVehicleCommand() { Body = vehicle }), Encoding.UTF8, "application/json");
            _response = await _client.PostAsync($"{_baseUrl}", content);
        }

        [Then(@"the vehicle should be created successfully")]
        public async Task ThenTheVehicleShouldBeCreatedSuccessfully()
        {
            _response.EnsureSuccessStatusCode();
        }

        [Then(@"I should receive the details of the newly created vehicle")]
        public async Task ThenIShouldReceiveTheDetailsOfTheNewlyCreatedVehicle()
        {
            var content = await _response.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<VehicleOperationResultDto>(content);
            Assert.NotNull(response);
            Assert.True(response.Success);
        }

        [Given(@"a vehicle with a specific ID exists")]
        public void GivenAVehicleWithASpecificIDExists()
        {
            _vehicleId = _scenarioContext.Get<Guid>("RegisteredVehicleId");
        }

        [When(@"I request the details of the vehicle with that ID")]
        public async Task WhenIRequestTheDetailsOfTheVehicleWithThatID()
        {
            _response = await _client.GetAsync($"{_baseUrl}/{_vehicleId}");
        }

        [Then(@"I should receive the details of the specified vehicle")]
        public void ThenIShouldReceiveTheDetailsOfTheSpecifiedVehicle()
        {
            _response.EnsureSuccessStatusCode();
            var content = _response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<VehicleDto>(content);
            Assert.NotNull(result);
            Assert.Equal(_vehicleId, result.Id);
        }

        [Given(@"a user with a specific ID has registered vehicles")]
        public void GivenAUserWithASpecificIDHasRegisteredVehicles()
        {
            _userId = _scenarioContext.Get<Guid>("RegisteredUserId");
            _vehicleId = _scenarioContext.Get<Guid>("RegisteredVehicleId");
        }

        [When(@"I request a list of all vehicles for the user")]
        public async Task WhenIRequestAListOfAllVehiclesForTheUser()
        {
            _response = await _client.GetAsync($"{_baseUrl}/user/{_userId}");
        }

        [Then(@"I should receive a list of vehicles associated with that user")]
        public async Task ThenIShouldReceiveAListOfVehiclesAssociatedWithThatUser()
        {
            _response.EnsureSuccessStatusCode();
            var content = _response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<IEnumerable<VehicleDto>>(content);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Contains(result, v => v.Id == _vehicleId);
        }


        [Given(@"a user with a specific ID has marked a vehicle as favorite")]
        public void GivenAUserWithASpecificIDHasMarkedAVehicleAsFavorite()
        {
            _vehicleId = _scenarioContext.Get<Guid>("RegisteredVehicleId");
        }

        [When(@"I request the favorite vehicle for the user")]
        public async Task WhenIRequestTheFavoriteVehicleForTheUser()
        {
            _response = await _client.GetAsync($"{_baseUrl}/user/{_userId}/favorite");
        }

        [Then(@"I should receive the details of the user's favorite vehicle")]
        public void ThenIShouldReceiveTheDetailsOfTheUsersFavoriteVehicle()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
