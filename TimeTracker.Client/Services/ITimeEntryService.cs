using TimeTracker.Shared.Models.TimeEntry;

namespace TimeTracker.Client.Services
{
    public interface ITimeEntryService
    {
        event Action? OnChange;

        public List<TimeEntryResponse> TimeEntries { get; set; }

        Task GetTimeEntriesByProjectId(int projectId);
        Task<TimeEntryResponse?> GetTimeEntryById(int id);
        Task CreateTimeEntry(TimeEntryRequest timeEntry);
        Task UpdateTimeEntry(int id, TimeEntryRequest timeEntry);
        Task DeleteTimeEntry(int id);
    }
}
