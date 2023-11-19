using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using VTVApp.Api;
using VTVApp.Api.Commands.Users.CreateUser;
using VTVApp.Api.Commands.Users.LoginUser;
using VTVApp.Api.Models.DTOs.Users;

namespace VTVApp.SpecflowTests.StepDefinitions
{
    [Binding]
    public class UserManagementStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HttpClient _client;
        private HttpResponseMessage _response;
        private readonly string _baseUrl = "/api/Users";
        private UserRegistrationDto _userRegistrationDto;
        private UserAuthenticationDto _userAuthenticationDto;
        private string _userToken;
        private Guid _userId;


        public UserManagementStepDefinitions(ScenarioContext scenarioContext, CustomWebApplicationFactory<Program> factory)
        {
            _scenarioContext = scenarioContext;
            _client = factory.CreateClient();
        }

        [Given(@"I have user registration details")]
        public void GivenIHaveUserRegistrationDetails()
        {
            _userRegistrationDto = new()
            {
                Name = "Jane Doe",
                Email = "janedoe@gmail.com",
                PhoneNumber = "1234567890",
                Password = "Password123!",
                CityId = _scenarioContext.Get<Guid>("CityId"),
                ProvinceId = 1
            };
        }

        [When(@"I register the user")]
        public async Task WhenIRegisterTheUser()
        {
            var content = new StringContent(JsonConvert.SerializeObject(new CreateUserCommand() { Body = _userRegistrationDto }), Encoding.UTF8, "application/json");
            _response = await _client.PostAsync($"{_baseUrl}", content);
        }

        [Then(@"the user should be registered successfully")]
        public void ThenTheUserShouldBeRegisteredSuccessfully()
        {
            _response.EnsureSuccessStatusCode();
        }

        [Then(@"the user details should be returned")]
        public async Task ThenTheUserDetailsShouldBeReturned()
        {
            var content = await _response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserOperationResultDto>(content);
            if (result != null)
            {
                _userId = result.Id;
                Assert.NotNull(result);
                Assert.True(result.Success);
            }
        }

        [Given(@"I have a valid user ID")]
        public void GivenIHaveAValidUserID()
        {
            _userId = _scenarioContext.Get<Guid>("RegisteredUserId");
        }

        [When(@"I request details for the user by ID")]
        public async Task WhenIRequestDetailsForTheUserByID()
        {
            _response = await _client.GetAsync($"{_baseUrl}/{_userId}");
        }

        [Then(@"the user's details should be returned")]
        public void ThenTheUsersDetailsShouldBeReturned()
        {
            _response.EnsureSuccessStatusCode();
            var content = _response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<UserDto>(content);
            Assert.NotNull(result);
            Assert.Equal(_userId, result.Id);
        }

        [Given(@"I have an invalid user ID")]
        public void GivenIHaveAnInvalidUserID()
        {
            _userId = Guid.NewGuid();
        }

        [When(@"I request details for the invalid user by ID")]
        public void WhenIRequestDetailsForTheInvalidUserByID()
        {
            _response = _client.GetAsync($"{_baseUrl}/{_userId}").Result;
        }


        [Then(@"I should receive a not found response")]
        public void ThenIShouldReceiveANotFoundResponse()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Given(@"there are users in the system")]
        public void GivenThereAreUsersInTheSystem()
        {
            return;
        }

        [When(@"I request a list of all users")]
        public async Task WhenIRequestAListOfAllUsers()
        {
            _response = await _client.GetAsync($"{_baseUrl}");
        }

        [Then(@"I should receive a list of users")]
        public void ThenIShouldReceiveAListOfUsers()
        {
            _response.EnsureSuccessStatusCode();
            var content = _response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(content);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Given(@"I have valid login credentials")]
        public void GivenIHaveValidLoginCredentials()
        {
            _userAuthenticationDto = new()
            {
                Email = "johndoe@gmail.com",
                Password = "Password123!"
            };
        }

        [When(@"I log in")]
        public void WhenILogIn()
        {
            var content = new StringContent(JsonConvert.SerializeObject(new LoginUserCommand() { Body = _userAuthenticationDto }), Encoding.UTF8, "application/json");
            _response = _client.PostAsync($"{_baseUrl}/login", content).Result;
        }

        [Then(@"I should be logged in successfully")]
        public void ThenIShouldBeLoggedInSuccessfully()
        {
            _response.EnsureSuccessStatusCode();
            var content = _response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<UserAuthenticationResultDto>(content);
            Assert.NotNull(result);
            Assert.True(result.IsAuthenticated);
        }

        [Then(@"receive a user authentication token")]
        public void ThenReceiveAUserAuthenticationToken()
        {
            _response.EnsureSuccessStatusCode();
            var content = _response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<UserAuthenticationResultDto>(content);
            Assert.NotNull(result);
            Assert.NotEmpty(result.Token);
        }

    }
}
