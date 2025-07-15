using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Shared.Models.Login
{
    public record struct LoginResponse(bool IsSuccessful, string? Error = null, string? Token = null);
}
