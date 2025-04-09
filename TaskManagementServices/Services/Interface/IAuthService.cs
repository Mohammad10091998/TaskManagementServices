using TaskManagementServices.DTOs;

namespace TaskManagementServices.Services.Interface
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginDto loginDto);
    }
}
