using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Shared.Entities;

namespace TimeTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeEntryController : ControllerBase
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

        [HttpGet]
        public ActionResult<List<TimeEntry>> GetAllTimeEntries()
        {
            return Ok(_timeEntries);
        }

        [HttpPost]
        public ActionResult<List<TimeEntry>> CreateTimeEntry(TimeEntry timeEntry)
        {
            //timeEntry.Id = _timeEntries.Count + 1;
            //timeEntry.DateCreated = DateTime.Now;
            _timeEntries.Add(timeEntry);
            return Ok(_timeEntries);
        }
    }
}
