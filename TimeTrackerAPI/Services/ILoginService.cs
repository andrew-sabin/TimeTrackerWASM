using TimeTracker.Shared.Models.Login;

namespace TimeTrackerAPI.Services
{
    public interface ILoginService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}
