﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Shared.Models.TimeEntry
{
    public class TimeEntryCreateRequest
    {
        public required string Project { get; set; } = string.Empty;
        public DateTime Start { get; set; } = DateTime.Now;
        public DateTime End { get; set; }
    }
}