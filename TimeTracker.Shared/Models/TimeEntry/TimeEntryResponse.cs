using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Shared.Models.TimeEntry
{
    public record struct TimeEntryResponse(
        int Id,
        string Project,
        DateTime Start,
        DateTime? End
    );
}
