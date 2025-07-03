using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Shared.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
