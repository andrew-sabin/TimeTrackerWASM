using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Shared.Entities;

namespace TimeTracker.Shared.Models.Account
{
    internal class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;

        public AccountService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AccountRegistrationResponse> RegisterAsync(AccountRegistrationRequest request)
        {
            var newUser = new User { UserName = request.Username, Email = request.Email, EmailConfirmed = true };
            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return new AccountRegistrationResponse(false, errors);
            }

            return new AccountRegistrationResponse(true);
        }
    }
}
