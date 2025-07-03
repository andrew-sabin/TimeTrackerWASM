using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Shared.Entities;
using TimeTrackerAPI.Repositories;
//using TimeTracker.Shared.Models.TimeEntry;
using TimeTrackerAPI.Services;

namespace TimeTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeEntryController : ControllerBase
    {
        private readonly ITimeEntryService _timeEntryService;

        public TimeEntryController(ITimeEntryService timeEntryService)
        {
            _timeEntryService = timeEntryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TimeEntryResponse>>> GetAllTimeEntries()
        {
            return Ok(await _timeEntryService.GetAllTimeEntries());
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<List<TimeEntryResponse>>> GetTimeEntriesByProjectId(int projectId)
        {
            return Ok(await _timeEntryService.GetTimeEntriesByProjectId(projectId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeEntryResponse>> GetTimeEntryById(int id)
        {
            var result = await _timeEntryService.GetTimeEntryById(id);
            if (result == null)
            {
                return NotFound($"Time entry with ID {id} not found.");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<TimeEntryResponse>>> CreateTimeEntry(TimeEntryCreateRequest timeEntry)
        {
            return Ok(await _timeEntryService.CreateTimeEntry(timeEntry));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<TimeEntryResponse>>> UpdateTimeEntry(int id, TimeEntryUpdateRequest timeEntry)
        {
            var result = await _timeEntryService.UpdateTimeEntry(id, timeEntry);
            if (result == null)
            {
                return NotFound($"Time entry with ID {id} not found.");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<TimeEntryResponse>>> DeleteTimeEntry(int id)
        {
            var result = await _timeEntryService.DeleteTimeEntry(id);
            if (result == null)
            {
                return NotFound($"Time entry with ID {id} not found.");
            }
            return Ok(result);
        }
    }
}
