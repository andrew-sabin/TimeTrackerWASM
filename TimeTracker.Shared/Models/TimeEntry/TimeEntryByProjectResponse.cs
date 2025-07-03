using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Shared.Models.Project;

namespace TimeTracker.Shared.Models.TimeEntry
{
    public record struct TimeEntryByProjectResponse(
        int Id,
        DateTime Start,
        DateTime? End
    );
}
