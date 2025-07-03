using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Shared.Models.Project
{
    public record struct ProjectResponse(
        int Id,
        string Name,
        string? Description,
        DateTime? Start,
        DateTime? EndDate
    );
}
