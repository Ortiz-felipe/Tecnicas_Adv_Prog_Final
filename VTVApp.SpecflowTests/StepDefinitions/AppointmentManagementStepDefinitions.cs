using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using VTVApp.Api;
using VTVApp.Api.Commands.Appointments.CreateAppointment;
using VTVApp.Api.Models.DTOs.Appointments;
using VTVApp.Api.Models.Entities;

namespace VTVApp.SpecflowTests.StepDefinitions
{
    [Binding]
    public class AppointmentManagementStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HttpClient _client;
        private HttpResponseMessage _response;
        private readonly string _baseUrl = "/api/Appointments";
        private IEnumerable<AppointmentListDto>? _appointments;
        private AvailableAppointmentSlotsDto _availableAppointmentSlots;
        private Guid _appointmentId;
        private string _userId;
        private string _currentDateInFormat;

        public AppointmentManagementStepDefinitions(CustomWebApplicationFactory<Program> factory, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _client = factory.CreateClient();
        }

        [When(@"I request a list of all appointments")]
        public async Task WhenIRequestAListOfAllAppointments()
        {
            _response = await _client.GetAsync($"{_baseUrl}");
        }

        [Then(@"I should receive a list of appointments")]
        public async Task ThenIShouldReceiveAListOfAppointments()
        {
            _response.EnsureSuccessStatusCode();
            var content = await _response.Content.ReadAsStringAsync();
            _appointments = JsonConvert.DeserializeObject<IEnumerable<AppointmentListDto>>(content);
            Assert.NotNull(_appointments);
        }

        [Then(@"each appointment should include details like date, time, and status"), ]
        public void ThenEachAppointmentShouldIncludeDetailsLikeDateTimeAndStatus()
        {
            if (!_appointments.Any())
            {
                Assert.Empty(_appointments); // Assert that the result set is empty
            }
            else
            {
                Assert.All(_appointments, appointment =>
                {
                    Assert.Equal((int)AppointmentStatus.Pending, appointment.AppointmentStatus);
                    Assert.Equal("John Doe", appointment.UserFullName);
                    Assert.Equal("ABC123", appointment.VehicleLicensePlate);
                });
            }
        }

        [Given(@"I have the current date in YYYY-MM-DD format")]
        public void GivenIHaveTheCurrentDateInYYYY_MM_DDFormat()
        {
            _currentDateInFormat = DateTime.UtcNow.ToString("yyyy-MM-dd");
        }

        [When(@"I request available appointment slots for this date")]
        public async Task WhenIRequestAvailableAppointmentSlotsForThisDate()
        {
            _response = await _client.GetAsync($"{_baseUrl}/availableSlots/{_currentDateInFormat}");
        }

        [Then(@"I should receive a list of available slots for that day")]
        public async Task ThenIShouldReceiveAListOfAvailableSlotsForThatDay()
        {
            _response.EnsureSuccessStatusCode();
            var content = await _response.Content.ReadAsStringAsync();
            _availableAppointmentSlots = JsonConvert.DeserializeObject<AvailableAppointmentSlotsDto>(content);
            Assert.NotNull(_availableAppointmentSlots);
            Assert.NotEmpty(_availableAppointmentSlots.AvailableSlots);
        }

        [When(@"I create a new appointment with the necessary details")]
        public async Task WhenICreateANewAppointmentWithTheNecessaryDetails()
        {
            var newAppointmentData = new CreateAppointmentDto()
            {
                AppointmentDate = DateTime.UtcNow,
                VehicleId = _scenarioContext.Get<Guid>("RegisteredVehicleId"),
                UserId = _scenarioContext.Get<Guid>("RegisteredUserId")
            };

            var newAppointment = new CreateAppointmentCommand()
            {
                Body = newAppointmentData
            };

            var newAppointmentContent = new StringContent(JsonConvert.SerializeObject(newAppointment), Encoding.UTF8, "application/json");
            _response = await _client.PostAsync($"{_baseUrl}", newAppointmentContent);
        }

        [Then(@"the appointment should be successfully created")]
        public void ThenTheAppointmentShouldBeSuccessfullyCreated()
        {
            _response.EnsureSuccessStatusCode();
        }

        [Then(@"I should receive the details of the new appointment")]
        public async Task ThenIShouldReceiveTheDetailsOfTheNewAppointment()
        {
            var content = await _response.Content.ReadAsStringAsync();
            var appointment = JsonConvert.DeserializeObject<AppointmentOperationResultDto>(content);
            Assert.NotNull(appointment);
            Assert.NotEqual(default, appointment.AppointmentId);
            Assert.NotEqual(default, appointment.ScheduledDate);
        }

        [Given(@"a user with ID ""([^""]*)"" has recent appointments")]
        public void GivenAUserWithIDHasRecentAppointments(string userId)
        {
            _userId = userId;
        }

        [When(@"I request the latest appointment for user with ID ""([^""]*)""")]
        public async Task WhenIRequestTheLatestAppointmentForUserWithID(string userId)
        {
            _response = await _client.GetAsync($"{_baseUrl}/latest/{userId}");
        }

        [Then(@"I should receive the details of the most recent appointment for that user")]
        public async Task ThenIShouldReceiveTheLatestAppointmentDetailsForThatUser()
        {
            _response.EnsureSuccessStatusCode();
            var content = await _response.Content.ReadAsStringAsync();
            var appointment = JsonConvert.DeserializeObject<AppointmentDetailsDto>(content);
        }

        [Given(@"an appointment with ID ""([^""]*)"" exists")]
        public void GivenAnAppointmentWithIDExists(string appointmentId)
        {
            _appointmentId = _scenarioContext.Get<Guid>("RegisteredAppointmentId");
        }

        [When(@"I request the details of the appointment with ID ""([^""]*)""")]
        public async Task WhenIRequestTheDetailsOfTheAppointmentWithID(string appointmentId)
        {
            _response = await _client.GetAsync($"{_baseUrl}/{_appointmentId}");
        }

        [Then(@"I should receive the details of the specified appointment")]
        public async Task ThenIShouldReceiveTheDetailsOfTheSpecifiedAppointment()
        {
            _response.EnsureSuccessStatusCode();
            var content = await _response.Content.ReadAsStringAsync();
            var appointment = JsonConvert.DeserializeObject<AppointmentDetailsDto>(content);
            Assert.NotNull(appointment);
            Assert.Equal(_appointmentId, appointment.Id);
        }

        [Given(@"a user with ID ""([^""]*)"" exists")]
        public void GivenAUserWithIDExists(string userId)
        {
            _userId = userId;
        }

        [When(@"I request all appointments for user with ID ""([^""]*)""")]
        public async Task WhenIRequestAllAppointmentsForUserWithID(string userId)
        {
            _response = await _client.GetAsync($"{_baseUrl}/user/{_scenarioContext.Get<Guid>("RegisteredUserId")}");
        }

        [Then(@"I should receive a list of appointments for that user")]
        public async Task ThenIShouldReceiveAListOfAppointmentsForThatUser()
        {
            _response.EnsureSuccessStatusCode();
        }
    }
}
