using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Shared.Models.Project
{
    public class ProjectRequest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter in a valid Project Name.")]
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
