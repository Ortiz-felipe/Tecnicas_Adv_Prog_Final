using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VTVApp.Api.Data;
using VTVApp.Api.Models.DTOs;
using VTVApp.Api.Models.DTOs.Users;
using VTVApp.Api.Models.Entities;
using VTVApp.Api.Repositories.Interfaces;
using VTVApp.Api.Services.Interfaces;

namespace VTVApp.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly VTVDataContext _context;
        private readonly IMapper _mapper;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly ITokenService _tokenService;

        public UserRepository(VTVDataContext context, IMapper mapper, IPasswordHashingService passwordHashingService, ITokenService tokenService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _passwordHashingService = passwordHashingService ?? throw new ArgumentNullException(nameof(passwordHashingService));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _context.Users
                .Include(u => u.Vehicles)
                .Include(u => u.City)
                .Include(u => u.Province)
                .Include(u => u.Appointments)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDetailsDto?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.Vehicles)
                .Include(u => u.City)
                .Include(u => u.Province)
                .Include(u => u.Appointments)
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            return _mapper.Map<UserDetailsDto>(user);
        }

        public async Task<UserOperationResultDto> AddUserAsync(UserRegistrationDto createUserDto, CancellationToken cancellationToken)
        {
            // Check if the email is already in use
            if (await _context.Users.AnyAsync(u => u.Email == createUserDto.Email, cancellationToken))
            {
                return new UserOperationResultDto
                {
                    Success = false,
                    Message = "Email is already in use."
                };
            }

            // Hash the password
            var hashedPassword = await _passwordHashingService.HashPassword(createUserDto.Password);

            // Create the User entity
            var user = new User
            {
                FullName = createUserDto.Name,
                Email = createUserDto.Email,
                PasswordHash = hashedPassword,
                CityId = createUserDto.CityId,
                ProvinceId = createUserDto.ProvinceId,
                Role = UserRole.RegularUser,
                PhoneNumber = createUserDto.PhoneNumber
            };

            // Add the new user to the context and save the changes
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new UserOperationResultDto
            {
                Success = true,
                Message = "User created successfully.",
                Id = user.Id,
            };
        }

        public async Task<UserOperationResultDto> UpdateUserAsync(UserUpdateDto userUpdateDto,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResultDto> DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UserAuthenticationResultDto> AuthenticateUserAsync(UserAuthenticationDto credentials, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.Province)
                .Include(u => u.City)
                .Where(u => u.Email == credentials.Email)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                return new UserAuthenticationResultDto
                {
                    IsAuthenticated = false,
                    ErrorMessage = "User not found."
                };
            }

            var isValidPassword = await _passwordHashingService.VerifyPassword(credentials.Password, user.PasswordHash);
            if (!isValidPassword)
            {
                return new UserAuthenticationResultDto
                {
                    IsAuthenticated = false,
                    ErrorMessage = "Invalid password."
                };
            }

            var token = _tokenService.CreateToken(_mapper.Map<UserDto>(user)); // The CreateToken method will be defined in the ITokenService interface.
            
            return new UserAuthenticationResultDto
            {
                IsAuthenticated = true,
                Token = token,
                User = _mapper.Map<UserDto>(user)
            };
        }
    }
}
