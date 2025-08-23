using AdminPanel.Application.Interfaces;
using AdminPanel.Data.Entities.Identity;
using AdminPanel.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Persistence.Data
{
    public class IdentityRepository<T> : IIdentityRepository<T> where T : class
    {
        private readonly UserManager<User> _userManager;
        private readonly StoreContext _context;

        public IdentityRepository(UserManager<User> userManager, StoreContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string roleName)
        {
            return await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> FindByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<User> FindByNameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<IReadOnlyList<User>> GetAllUsersAsync()
        {
            return await _userManager.Users.Where(u => !u.IsDeleted).ToListAsync();
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName)
        {
            return await _userManager.RemoveFromRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task<IdentityResult> UpdateAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IReadOnlyList<T>> ListWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<int> CountActiveAsync()
        {
            return await _userManager.Users.CountAsync(u => !u.IsDeleted);
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
            //var query = _userManager.Users.AsQueryable().Where(u => !u.IsDeleted);

            //if (spec.Criteria != null)
            //{
            //    query = query.Where(spec.Criteria);
            //}
            //if (spec.OrderBy != null)
            //{
            //    query = query.OrderBy(spec.OrderBy);
            //}
            //if (spec.OrderByDescending != null)
            //{
            //    query = query.OrderByDescending(spec.OrderByDescending);
            //}
            //if (spec.IsPagingEnabled)
            //{
            //    query = query.Skip(spec.Skip).Take(spec.Take);
            //}
            //return query;
        }
    }
}