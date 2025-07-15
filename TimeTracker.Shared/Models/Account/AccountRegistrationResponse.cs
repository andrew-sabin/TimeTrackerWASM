using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Shared.Models.Account
{
    public record struct AccountRegistrationResponse(bool IsSuccessful, IEnumerable<string>? Errors = null);
}
