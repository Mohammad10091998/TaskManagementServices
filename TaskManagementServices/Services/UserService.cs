using TaskManagementServices.DTOs;
using TaskManagementServices.Services.Interface;
using TaskManagementServices.Repositories.Interface;
using TaskManagementServices.Domain;
using Microsoft.AspNetCore.Identity;

namespace TaskManagementServices.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
            _logger = logger;   
        }

        public async Task<UserModel?> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null) return null;

                return new UserModel
                {
                    Id = id,
                    Email = user.Email,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user with ID {Id}", id);
                throw;
            }
        }

        public async Task<UserModel> RegisterUserAsync(UserCreateModel userModel)
        {

            try
            {
                var user = new User
                {
                    Email = userModel.Email,
                    Password = userModel.Password,
                };

                var userExist = await _userRepository.GetUserByEmailAsync(userModel.Email);
                if (userExist != null)
                {
                    throw new Exception($"User Exist with email id {userModel.Email}");
                }
                user.Password = _passwordHasher.HashPassword(user, userModel.Password);
                var createdUser = await _userRepository.AddUserAsync(user);

                return new UserModel
                {
                    Id = createdUser.Id,
                    Email = createdUser.Email,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user with Email {Email}", userModel.Email);
                throw;
            }
        }

        public async System.Threading.Tasks.Task RemoveUserByIdAsync(int id)
        {
            try
            {
                await _userRepository.DeleteUserAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting user with ID {Id}", id);
                throw;
            }
        }
    }
}
