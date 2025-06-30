using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Shared.Models.TimeEntry
{
    public record struct TimeEntryCreateRequest(
        string Project,
        DateTime Start,
        DateTime? End
    );
}