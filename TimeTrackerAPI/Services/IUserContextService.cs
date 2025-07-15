namespace TimeTrackerAPI.Services
{
    public interface IUserContextService
    {
        string? GetUserId();
        Task<User?> GetUserAsync();
    }
}
