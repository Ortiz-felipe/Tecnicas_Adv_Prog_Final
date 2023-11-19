using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using VTVApp.Api.Data;
using VTVApp.Api.Models.DTOs.Users;
using VTVApp.Api.Models.Entities;
using VTVApp.Api.Repositories;
using VTVApp.Api.Services;
using VTVApp.Api.Services.Interfaces;

namespace VTVApp.UnitTests
{
    public class UserRepositoryTests
    {
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly IPasswordHashingService _passwordHashingService = Substitute.For<IPasswordHashingService>();
        private readonly ITokenService _tokenService = Substitute.For<ITokenService>();
        private readonly VTVDataContext _context;
        private readonly UserRepository _repository;
        private readonly Guid _userId = Guid.NewGuid();
        private readonly int _provinceId = 1;
        private readonly Guid _cityId = Guid.NewGuid();
        private readonly Guid _appointmentId = Guid.NewGuid();
        private readonly Guid _inspectionId = Guid.NewGuid();

        public UserRepositoryTests()
        {
            _context = CreateInMemoryDataContext();
            _repository = new UserRepository(_context, _mapper, _passwordHashingService, _tokenService);

            // Mock the password hashing service
            _passwordHashingService.HashPassword(Arg.Any<string>())
                .Returns("hashedPassword");

            _passwordHashingService.VerifyPassword(Arg.Any<string>(), Arg.Any<string>())
                .Returns(true); // Always return true for password verification

            // Mock the token service
            _tokenService.CreateToken(Arg.Any<UserDto>())
                .Returns("dummyToken");

            _mapper.Map<UserDto>(Arg.Is<User>(u => u != null)).Returns(callInfo =>
            {
                var user = callInfo.Arg<User>();
                return new UserDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    ProvinceName = user.Province?.Name,
                    CityName = user.City?.Name,
                    UserRole = (int)user.Role
                };
            });

            _mapper.Map<UserDetailsDto>(Arg.Is<User>(u => u != null)).Returns(callInfo =>
            {
                var user = callInfo.Arg<User>();
                return new UserDetailsDto
                {
                    Id = user.Id,
                    Name = user.FullName,
                    Email = user.Email,
                    Role = user.Role.ToString(),
                    CityName = user.City?.Name,
                    ProvinceName = user.Province?.Name,
                    // Map other properties as needed
                };
            });

            _mapper.Map<UserOperationResultDto>(Arg.Any<User>())
                .Returns(callInfo =>
                {
                    var user = callInfo.Arg<User>();
                    return new UserOperationResultDto
                    {
                        Id = user.Id,
                        Success = true,
                        Message = "User created successfully."
                    };
                });

            _mapper.Map<IEnumerable<UserDto>>(Arg.Any<IEnumerable<User>>())
                .Returns(callInfo =>
                {
                    var users = callInfo.Arg<IEnumerable<User>>();
                    return users.Select(user => new UserDto
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        ProvinceName = user.Province.Name,
                        CityName = user.City.Name,
                        UserRole = (int)user.Role
                    }).ToList();
                });


            SeedUsersData();

        }

        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnAllUsers()
        {
            // Act
            var users = await _repository.GetAllUsersAsync(default);

            // Assert
            users.Should().NotBeEmpty();
            users.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser()
        {
            // Act
            var user = await _repository.GetUserByIdAsync(_userId, default);

            // Assert
            user.Should().NotBeNull();
            user!.Id.Should().Be(_userId);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Act
            var user = await _repository.GetUserByIdAsync(Guid.NewGuid(), default);

            // Assert
            user.Should().BeNull();
        }

        [Fact]
        public async Task AddUserAsync_ShouldAddUser()
        {
            // Arrange
            var userRegistrationDto = new UserRegistrationDto
            {
                Name = "Daniel Doe",
                Email = "danieldoe@gmail.com",
                Password = "123456",
                CityId = _cityId,
                ProvinceId = _provinceId,
                PhoneNumber = "1144778855"
            };

            // Act
            var result = await _repository.AddUserAsync(userRegistrationDto, default);

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Message.Should().Be("User created successfully.");
        }

        private void SeedUsersData()
        {
            if (!_context.Provinces.Any())
            {
                _context.Provinces.AddRange(new List<Province>
                {
                    new Province
                    {
                        Id = _provinceId,
                        Name = "Province 1"
                    },
                    new Province
                    {
                        Id = 2,
                        Name = "Province 2"
                    }
                });
                _context.SaveChanges();
            }

            if (!_context.Cities.Any())
            {
                _context.Cities.AddRange(new List<City>
                {
                    new City
                    {
                        Id = _cityId,
                        Name = "City 1",
                        ProvinceId = _provinceId,
                        PostalCode = "12345"
                    },
                    new City
                    {
                        Id = Guid.NewGuid(),
                        Name = "City 2",
                        ProvinceId = _provinceId,
                        PostalCode = "123456"
                    }
                });
                _context.SaveChanges();
            }

            if (!_context.Users.Any())
            {
                _context.Users.AddRange(new List<User>
                {
                    new User
                    {
                        Id = _userId,
                        Email = "johndoe@gmail.com",
                        FullName = "John Doe",
                        CityId = _cityId,
                        ProvinceId = _provinceId,
                        PasswordHash = "123456",
                        PhoneNumber = "1144778855"
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Email = "janedoe@gmail.com",
                        FullName = "Jane Doe",
                        CityId = _cityId,
                        ProvinceId = _provinceId,
                        PasswordHash = "123456",
                        PhoneNumber = "1144778855"
                    }
                });

                _context.SaveChanges();
            }
        }


        private VTVDataContext CreateInMemoryDataContext()
        {
            var options = new DbContextOptionsBuilder<VTVDataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new VTVDataContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
