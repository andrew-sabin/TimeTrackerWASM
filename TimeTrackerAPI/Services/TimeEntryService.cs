using Mapster;
using TimeTracker.Shared.Entities;
using TimeTracker.Shared.Models.TimeEntry;
using TimeTrackerAPI.Repositories;


namespace TimeTrackerAPI.Services
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly ITimeEntryRepository _timeEntryRepo;

        public TimeEntryService(ITimeEntryRepository timeEntryRepo)
        {
            _timeEntryRepo = timeEntryRepo;
        }

        public List<TimeEntryResponse> CreateTimeEntry(TimeEntryCreateRequest request)
        {
            var newEntry = request.Adapt<TimeEntry>();
            var result = _timeEntryRepo.CreateTimeEntry(newEntry);
            return result.Adapt<List<TimeEntryResponse>>();
        }

        public List<TimeEntryResponse>? DeleteTimeEntry(int id)
        {
            var result = _timeEntryRepo.DeleteTimeEntry(id);
            if (result == null)
            {
                return null;
            }
            return result.Adapt<List<TimeEntryResponse>>();
        }

        public List<TimeEntryResponse> GetAllTimeEntries()
        {
            var result = _timeEntryRepo.GetAllTimeEntries();
            return result.Adapt<List<TimeEntryResponse>>();
        }

        public TimeEntryResponse? GetTimeEntryById(int id)
        {
            var result = _timeEntryRepo.GetTimeEntryById(id);
            if (result == null)
            {
                return null;
            }
            return result.Adapt<TimeEntryResponse>();
        }

        public List<TimeEntryResponse>? UpdateTimeEntry(int id, TimeEntryUpdateRequest request)
        {
            var updatedEntry = request.Adapt<TimeEntry>();
            var result = _timeEntryRepo.UpdateTimeEntry(id, updatedEntry);
            if (result == null)
            {
                return null;
            }
            return result.Adapt<List<TimeEntryResponse>>();
        }
    }
}
