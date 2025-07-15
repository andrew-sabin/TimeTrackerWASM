using TimeTracker.Shared.Entities;

namespace TimeTrackerAPI.Repositories
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;

        public TimeEntryRepository(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<List<TimeEntry>> CreateTimeEntry(TimeEntry timeEntry)
        {
            var user = await _userContextService.GetUserAsync();
            if (user == null)
            {
                throw new EntityNotFoundException("User not found.");
            }

            timeEntry.User = user;

            _context.TimeEntries.Add(timeEntry);
            await _context.SaveChangesAsync();

            return await GetAllTimeEntries();
        }

        public async Task<List<TimeEntry>?> DeleteTimeEntry(int id)
        {
            var userId = _userContextService.GetUserId();
            if (userId == null)
            {
                return null;
            }

            var dbTimeEntry = await _context.TimeEntries
                .FirstOrDefaultAsync(te => te.Id == id && te.User.Id == userId);

            if (dbTimeEntry == null)
            {
                return null;
            }
            _context.TimeEntries.Remove(dbTimeEntry);
            await _context.SaveChangesAsync();

            return await GetAllTimeEntries();
        }

        public async Task<List<TimeEntry>> GetAllTimeEntries()
        {
            var UserId = _userContextService.GetUserId();
            if (UserId == null)
            {
                return new List<TimeEntry>();
            }

            return await _context.TimeEntries
                .Where(te => te.User.Id == UserId)
                .Include(te=> te.Project)
                .ThenInclude(te => te.ProjectDetails)
                .ToListAsync();
        }

        public async Task<List<TimeEntry>?> GetTimeEntriesByProjectId(int projectId)
        {
            var userId = _userContextService.GetUserId();
            if (userId == null)
            {
                throw new EntityNotFoundException("User not found.");
            }

            return await _context.TimeEntries.Where(te => te.ProjectId == projectId && te.User.Id == userId)
                .Include(te => te.Project)
                .ThenInclude(te => te.ProjectDetails)
                .ToListAsync();
        }

        public async Task<TimeEntry?> GetTimeEntryById(int id)
        {
            var userId = _userContextService.GetUserId();
            if (userId == null)
            {
                return null;
            }



            var timeEntry = await _context.TimeEntries
                .Include(te => te.Project)
                .ThenInclude(te => te.ProjectDetails)
                .FirstOrDefaultAsync(te => te.Id == id && te.User.Id == userId);
            if (timeEntry == null)
            {
                return null;
            }

            return timeEntry;
        }

        public async Task<List<TimeEntry>?> UpdateTimeEntry(int id, TimeEntry timeEntry)
        {
            var userId = _userContextService.GetUserId();
            if (userId == null)
            {
                throw new EntityNotFoundException("User not found.");
            }

            var dbTimeEntry = await _context.TimeEntries.FirstOrDefaultAsync(te => te.Id == id && te.User.Id == userId);

            if (dbTimeEntry == null)
            {
                throw new EntityNotFoundException($"Time entry with ID {id} not found.");
            }

            dbTimeEntry.ProjectId = timeEntry.ProjectId;
            dbTimeEntry.Start = timeEntry.Start;
            dbTimeEntry.End = timeEntry.End;
            dbTimeEntry.DateUpdated = DateTime.Now;

            await _context.SaveChangesAsync();

            return await GetAllTimeEntries();
        }
    }
}
