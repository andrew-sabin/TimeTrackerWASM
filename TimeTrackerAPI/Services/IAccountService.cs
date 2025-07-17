using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Shared.Models.Account
{
    public interface IAccountService
    {
        Task<AccountRegistrationResponse> RegisterAsync(AccountRegistrationRequest request);
        Task AssignRole(string userName, string roleName);
    }
}
