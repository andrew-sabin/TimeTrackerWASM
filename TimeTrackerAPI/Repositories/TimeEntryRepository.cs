using TimeTracker.Shared.Entities;

namespace TimeTrackerAPI.Repositories
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        public static List<TimeEntry> _timeEntries = new List<TimeEntry>
        {
            new TimeEntry
            {
                Id = 1,
                Project = "Time Tracker App",
                End = DateTime.Now.AddHours(1)
            }
        };
        public List<TimeEntry> CreateTimeEntry(TimeEntry timeEntry)
        {
            _timeEntries.Add(timeEntry);
            return _timeEntries;
        }

        public List<TimeEntry>? DeleteTimeEntry(int id)
        {
            var entryToDelete = _timeEntries.FirstOrDefault(e => e.Id == id);
            if (entryToDelete == null)
            {
                return null;
            }
            _timeEntries.Remove(entryToDelete);
            return _timeEntries;
        }

        public List<TimeEntry> GetAllTimeEntries()
        {
            return _timeEntries;
        }

        public TimeEntry? GetTimeEntryById(int id)
        {
            return _timeEntries.FirstOrDefault(e => e.Id == id);
        }

        public List<TimeEntry>? UpdateTimeEntry(int id, TimeEntry timeEntry)
        {
            var entryToUpdateIndex = _timeEntries.FindIndex(e => e.Id == id);
            if (entryToUpdateIndex == -1)
            {
                return null;
            }
            _timeEntries[entryToUpdateIndex] = timeEntry;
            return _timeEntries;
        }
    }
}
