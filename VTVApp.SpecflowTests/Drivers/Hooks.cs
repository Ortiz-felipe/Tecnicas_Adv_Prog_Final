using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VTVApp.Api;
using VTVApp.Api.Commands.Appointments.CreateAppointment;
using VTVApp.Api.Commands.Users.CreateUser;
using VTVApp.Api.Commands.Vehicles.CreateVehicle;
using VTVApp.Api.Models.DTOs.Appointments;
using VTVApp.Api.Models.DTOs.Cities;
using VTVApp.Api.Models.DTOs.Users;
using VTVApp.Api.Models.DTOs.Vehicle;

namespace VTVApp.SpecflowTests.Drivers
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HttpClient _client;

        public Hooks(ScenarioContext scenarioContext, CustomWebApplicationFactory<Program> client)
        {
            _scenarioContext = scenarioContext;
            _client = client.CreateClient();
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            var cities = await _client.GetAsync("api/Cities?ProvinceId=1");
            var response = await cities.Content.ReadAsStringAsync();

            var content = JsonConvert.DeserializeObject<IEnumerable<CityDetailsDto>>(response);
            _scenarioContext["CityId"] = content.FirstOrDefault().Id;

            if (!_scenarioContext.ContainsKey("RegisteredUserId"))
            {
                var users = await _client.GetAsync("api/Users");
                var userResponseAsync = await users.Content.ReadAsStringAsync();

                var userContent = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(userResponseAsync);

                if (userContent is not null && !userContent.Any())
                {

                    var newUser = new CreateUserCommand()
                    {
                        Body = new UserRegistrationDto
                        {
                            Name = "John Doe",
                            Email = "johndoe@gmail.com",
                            Password = "Password123!",
                            PhoneNumber = "1152642123",
                            CityId = content.FirstOrDefault().Id,
                            ProvinceId = 1,
                        }
                    };

                    var newUserContent = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

                    // Send POST request to register user
                    var userResponse = await _client.PostAsync("api/Users", newUserContent);
                    userResponse.EnsureSuccessStatusCode();

                    // Optionally read the response (if you need to use it later)
                    var userResponseContent = await userResponse.Content.ReadAsStringAsync();
                    var registeredUser = JsonConvert.DeserializeObject<UserDto>(userResponseContent);

                    // Store the registered user or user ID in the ScenarioContext if needed
                    _scenarioContext["RegisteredUserId"] = registeredUser.Id;
                }
                else
                {
                    // Store the registered user or user ID in the ScenarioContext if needed
                    _scenarioContext["RegisteredUserId"] = userContent.FirstOrDefault().Id;
                }

            }

            if (!_scenarioContext.ContainsKey("RegisteredVehicle"))
            {
                var vehicle = new CreateVehicleCommand()
                {
                    Body = new CreateVehicleDto()
                    {
                        UserId = _scenarioContext.Get<Guid>("RegisteredUserId"),
                        Brand = "Honda",
                        Model = "Civic",
                        Year = 2015,
                        LicensePlate = "ABC123",
                        Color = "Black",
                    }
                };

                var vehicleContent = new StringContent(JsonConvert.SerializeObject(vehicle), Encoding.UTF8, "application/json");

                var vehicleResponse = await _client.PostAsync("api/Vehicles", vehicleContent);
                vehicleResponse.EnsureSuccessStatusCode();

                var vehicleResponseContent = await vehicleResponse.Content.ReadAsStringAsync();
                var registeredVehicle = JsonConvert.DeserializeObject<VehicleOperationResultDto>(vehicleResponseContent);

                _scenarioContext["RegisteredVehicleId"] = registeredVehicle.Id;
            }
            else
            {
                var userVehicles = await _client.GetAsync($"api/Vehicles/user/{_scenarioContext.Get<Guid>("RegisteredUserId")}");
                var userVehiclesResponse = await userVehicles.Content.ReadAsStringAsync();

                var userVehiclesContent = JsonConvert.DeserializeObject<IEnumerable<VehicleDto>>(userVehiclesResponse);

                if (userVehiclesContent is not null && !userVehiclesContent.Any())
                {
                    var vehicle = new CreateVehicleCommand()
                    {
                        Body = new CreateVehicleDto()
                        {
                            UserId = _scenarioContext.Get<Guid>("RegisteredUserId"),
                            Brand = "Honda",
                            Model = "Civic",
                            Year = 2015,
                            LicensePlate = "ABC123",
                            Color = "Black",
                        }
                    };

                    var vehicleContent = new StringContent(JsonConvert.SerializeObject(vehicle), Encoding.UTF8, "application/json");

                    var vehicleResponse = await _client.PostAsync("api/Vehicles", vehicleContent);
                    vehicleResponse.EnsureSuccessStatusCode();

                    var vehicleResponseContent = await vehicleResponse.Content.ReadAsStringAsync();
                    var registeredVehicle = JsonConvert.DeserializeObject<VehicleOperationResultDto>(vehicleResponseContent);

                    _scenarioContext["RegisteredVehicleId"] = registeredVehicle.Id;
                }
            }

            if (!_scenarioContext.ContainsKey("RegisteredAppointmentId"))
            {
                var userAppointment = new CreateAppointmentCommand()
                {
                    Body = new CreateAppointmentDto()
                    {
                        UserId = _scenarioContext.Get<Guid>("RegisteredUserId"),
                        VehicleId = _scenarioContext.Get<Guid>("RegisteredVehicleId"),
                        AppointmentDate = DateTime.UtcNow.AddDays(1),
                    }
                };

                var userAppointmentContent = new StringContent(JsonConvert.SerializeObject(userAppointment), Encoding.UTF8, "application/json");
                var appointmentResponse = await _client.PostAsync("api/Appointments", userAppointmentContent);

                appointmentResponse.EnsureSuccessStatusCode();

                var appointmentContent = await appointmentResponse.Content.ReadAsStringAsync();
                var registeredAppointment = JsonConvert.DeserializeObject<AppointmentOperationResultDto>(appointmentContent);

                _scenarioContext["RegisteredAppointmentId"] = registeredAppointment.AppointmentId;
            }
            else
            {
                var appointment =
                    _client.GetAsync($"api/Appointments/latest/{_scenarioContext.Get<Guid>("RegisteredUserId")}");
                var appointmentResponse = await appointment.Result.Content.ReadAsStringAsync();

                var appointmentContent = JsonConvert.DeserializeObject<AppointmentDetailsDto>(appointmentResponse);

                _scenarioContext["RegisteredAppointmentId"] = appointmentContent.Id;
            }

        }

        //[BeforeScenario("UserAndVehicleExists")]
        //public async Task BeforeScenarioUserAndVehicleExists()
        //{
        //    if (!_scenarioContext.ContainsKey("RegisteredUserId"))
        //    {
        //        var cities = await _client.GetAsync("api/Cities?ProvinceId=1");
        //        var response = await cities.Content.ReadAsStringAsync();

        //        var content = JsonConvert.DeserializeObject<IEnumerable<CityDetailsDto>>(response);

        //        var newUser = new CreateUserCommand()
        //        {
        //            Body = new UserRegistrationDto
        //            {
        //                Name = "John Doe",
        //                Email = "johndoe@gmail.com",
        //                Password = "Password123!",
        //                CityId = content.FirstOrDefault().Id,
        //                ProvinceId = 1,
        //            }
        //        };

        //        var newUserContent = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

        //        // Send POST request to register user
        //        var userResponse = await _client.PostAsync("api/Users", newUserContent);
        //        userResponse.EnsureSuccessStatusCode();

        //        // Optionally read the response (if you need to use it later)
        //        var userResponseContent = await userResponse.Content.ReadAsStringAsync();
        //        var registeredUser = JsonConvert.DeserializeObject<UserDto>(userResponseContent);

        //        // Store the registered user or user ID in the ScenarioContext if needed
        //        _scenarioContext["RegisteredUserId"] = registeredUser.Id;
        //    }

        //    if (!_scenarioContext.ContainsKey("RegisteredVehicle"))
        //    {
        //        var vehicle = new CreateVehicleCommand()
        //        {
        //            Body = new CreateVehicleDto()
        //            {
        //                UserId = _scenarioContext.Get<Guid>("RegisteredUserId"),
        //                Brand = "Honda",
        //                Model = "Civic",
        //                Year = 2015,
        //                LicensePlate = "ABC123",
        //                Color = "Black",
        //            }
        //        };

        //        var vehicleContent = new StringContent(JsonConvert.SerializeObject(vehicle), Encoding.UTF8, "application/json");

        //        var vehicleResponse = await _client.PostAsync("api/Vehicles", vehicleContent);
        //        vehicleResponse.EnsureSuccessStatusCode();

        //        var vehicleResponseContent = await vehicleResponse.Content.ReadAsStringAsync();
        //        var registeredVehicle = JsonConvert.DeserializeObject<VehicleOperationResultDto>(vehicleResponseContent);

        //        _scenarioContext["RegisteredVehicleId"] = registeredVehicle.Id;
        //    }
        //}

        //[BeforeScenario("UserAndVehicleAndAppointmentExists")]
        //public async Task BeforeScenarioUserAndVehicleAndAppointmentExists()
        //{
        //    if (!_scenarioContext.ContainsKey("RegisteredUserId") && !_scenarioContext.ContainsKey("RegisteredVehicleId"))
        //    {
        //        var users = await _client.GetAsync("api/Users");
        //        var userResponseAsync = await users.Content.ReadAsStringAsync();

        //        var userContent = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(userResponseAsync);

        //        if (userContent is not null && !userContent.Any())
        //        {
        //            var cities = await _client.GetAsync("api/Cities?ProvinceId=1");
        //            var response = await cities.Content.ReadAsStringAsync();

        //            var content = JsonConvert.DeserializeObject<IEnumerable<CityDetailsDto>>(response);

        //            var newUser = new CreateUserCommand()
        //            {
        //                Body = new UserRegistrationDto
        //                {
        //                    Name = "John Doe",
        //                    Email = "johndoe@gmail.com",
        //                    Password = "Password123!",
        //                    PhoneNumber = "1152642123",
        //                    CityId = content.FirstOrDefault().Id,
        //                    ProvinceId = 1,
        //                }
        //            };

        //            var newUserContent = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

        //            // Send POST request to register user
        //            var userResponse = await _client.PostAsync("api/Users", newUserContent);
        //            userResponse.EnsureSuccessStatusCode();

        //            // Optionally read the response (if you need to use it later)
        //            var userResponseContent = await userResponse.Content.ReadAsStringAsync();
        //            var registeredUser = JsonConvert.DeserializeObject<UserDto>(userResponseContent);

        //            // Store the registered user or user ID in the ScenarioContext if needed
        //            _scenarioContext["RegisteredUserId"] = registeredUser.Id;
        //        }

        //        var userVehicles = await _client.GetAsync($"api/Vehicles/user/{_scenarioContext.Get<Guid>("RegisteredUserId")}");
        //        var userVehiclesResponse = await userVehicles.Content.ReadAsStringAsync();

        //        var userVehiclesContent = JsonConvert.DeserializeObject<IEnumerable<VehicleDto>>(userVehiclesResponse);

        //        if (userVehiclesContent is not null && !userVehiclesContent.Any())
        //        {
        //            var vehicle = new CreateVehicleCommand()
        //            {
        //                Body = new CreateVehicleDto()
        //                {
        //                    UserId = _scenarioContext.Get<Guid>("RegisteredUserId"),
        //                    Brand = "Honda",
        //                    Model = "Civic",
        //                    Year = 2015,
        //                    LicensePlate = "ABC123",
        //                    Color = "Black",
        //                }
        //            };

        //            var vehicleContent = new StringContent(JsonConvert.SerializeObject(vehicle), Encoding.UTF8, "application/json");

        //            var vehicleResponse = await _client.PostAsync("api/Vehicles", vehicleContent);
        //            vehicleResponse.EnsureSuccessStatusCode();

        //            var vehicleResponseContent = await vehicleResponse.Content.ReadAsStringAsync();
        //            var registeredVehicle = JsonConvert.DeserializeObject<VehicleOperationResultDto>(vehicleResponseContent);

        //            _scenarioContext["RegisteredVehicleId"] = registeredVehicle.Id;
        //        }
        //    }
        //    else
        //    {
        //        var userAppointment = new CreateAppointmentCommand()
        //        {
        //            Body = new CreateAppointmentDto()
        //            {
        //                UserId = _scenarioContext.Get<Guid>("RegisteredUserId"),
        //                VehicleId = _scenarioContext.Get<Guid>("RegisteredVehicleId"),
        //                AppointmentDate = DateTime.UtcNow.AddDays(1),
        //            }
        //        };

        //        var userAppointmentContent = new StringContent(JsonConvert.SerializeObject(userAppointment), Encoding.UTF8, "application/json");
        //        var appointmentResponse = await _client.PostAsync("api/Appointments", userAppointmentContent);

        //        appointmentResponse.EnsureSuccessStatusCode();

        //        var appointmentContent = await appointmentResponse.Content.ReadAsStringAsync();
        //        var registeredAppointment = JsonConvert.DeserializeObject<AppointmentOperationResultDto>(appointmentContent);

        //        _scenarioContext["RegisteredAppointmentId"] = registeredAppointment.AppointmentId;
        //    }
        //}
    }
}
