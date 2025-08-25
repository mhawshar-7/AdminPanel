using AdminPanel.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
    }
}
