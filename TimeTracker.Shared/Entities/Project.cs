using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Shared.Entities
{
    public class Project : SoftDeleteableEntity
    {
        public required string Name { get; set; }
        public List<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
        public ProjectDetails? ProjectDetails { get; set; }

    }
}
