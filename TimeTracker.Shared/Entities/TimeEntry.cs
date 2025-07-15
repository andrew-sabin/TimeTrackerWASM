using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Shared.Entities
{
    public class TimeEntry : BaseEntity
    {
        public int? ProjectId { get; set; }
        public Project? Project { get; set; }
        public DateTime Start { get; set; } = DateTime.Now;
        public DateTime End { get; set; }
        public required User User { get; set; }
    }
}
