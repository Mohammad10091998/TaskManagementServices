using TaskManagementServices.DTOs;

namespace TaskManagementServices.Services.Interface
{
    public interface IUserService
    {
        Task<UserModel?> GetUserByIdAsync(int id);
        Task<UserModel> RegisterUserAsync(UserCreateModel user);
        Task RemoveUserByIdAsync(int id);
    }
}
