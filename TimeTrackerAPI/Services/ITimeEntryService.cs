using TimeTracker.Shared.Entities;
using TimeTracker.Shared.Models.TimeEntry;

namespace TimeTrackerAPI.Services
{
    public interface ITimeEntryService
    {
        TimeEntryResponse? GetTimeEntryById(int id);
        List<TimeEntryResponse> GetAllTimeEntries();
        List<TimeEntryResponse> CreateTimeEntry(TimeEntryCreateRequest timeEntry);
        List<TimeEntryResponse>? UpdateTimeEntry(int id, TimeEntryUpdateRequest timeEntry);
        List<TimeEntryResponse>? DeleteTimeEntry(int id);
    }
}
