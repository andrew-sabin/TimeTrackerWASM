using TimeTracker.Shared.Entities;
using TimeTracker.Shared.Models.TimeEntry;

namespace TimeTrackerAPI.Services
{
    public interface ITimeEntryService
    {
        Task<TimeEntryResponse?> GetTimeEntryById(int id);
        Task<List<TimeEntryResponse>> GetAllTimeEntries();
        Task<List<TimeEntryResponse>> CreateTimeEntry(TimeEntryCreateRequest timeEntry);
        Task<List<TimeEntryResponse>?> UpdateTimeEntry(int id, TimeEntryUpdateRequest timeEntry);
        Task<List<TimeEntryResponse>?> DeleteTimeEntry(int id);
        Task<List<TimeEntryByProjectResponse>?> GetTimeEntriesByProjectId(int projectId);
    }
}
