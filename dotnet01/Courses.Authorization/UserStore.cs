using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Security.Claims;
using System.Threading.Tasks;
using Courses.Models;
using Courses.Models.Repositories;
namespace Courses.Authorization
{
    public class UserStore : IUserStore<IdentityUser, int>,
                                   IUserEmailStore<IdentityUser, int>,
                                   IUserRoleStore<IdentityUser, int>,
                                   IUserPasswordStore<IdentityUser, int>,
                                   IDisposable
    {
        private readonly IAccountRepository _repository;

        public UserStore(IAccountRepository repository)
        {
            if (repository != null)
                _repository = repository;
            else
                throw new ArgumentNullException();
        }
        private User ToUser(IdentityUser user)
        {
            var usr = _repository.GetUser(user.UserName, user.PasswordHash);
            return usr;
        }
        private User ConvertToUser(IdentityUser user)
        {
            Console.WriteLine(user.Id);
            return new User()
            {
                CreatedDate = DateTime.Now,
                Email = user.UserName,
                PasswordHash = user.PasswordHash,
            };
        }
        public Task CreateAsync(IdentityUser user)
        {
            _repository.Add(ConvertToUser(user));
            var usr = _repository.GetUser(user.UserName, user.PasswordHash);
            return Task.FromResult<int>(0);
        }

        public Task DeleteAsync(IdentityUser user)
        {
            _repository.Delete(ConvertToUser(user));
            return Task.FromResult<int>(0);
        }

        public async Task<IdentityUser> FindByIdAsync(int userId)
        {
            var user = await _repository.GetUserByID(userId);
            if (user != null)
            {
                return new IdentityUser(user.Id, user.Login);
            }
            return null;
        }

        public async Task<IdentityUser> FindByNameAsync(string userName)
        {
            var user = await _repository.GetUserByEmail(userName);
            if (user != null)
            {
                return new IdentityUser(user.Id, user.Login);
            }
            return null;
        }

        public Task UpdateAsync(IdentityUser user)
        {
            var usr = ConvertToUser(user);
            _repository.Update(usr);
            return Task.FromResult<int>(0);
        }

        public void Dispose()
        {

        }

        public Task<IdentityUser> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(IdentityUser user, string email)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            return Task.Factory.StartNew(
            () =>
            {
                var usr = ToUser(user);
                usr.Role = roleName;
                _repository.Update(usr);
            });
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            var usr = ToUser(user);
            var roles = new List<string>();
            roles.Add(usr.Role);
            return Task.FromResult<IList<string>>(roles);
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            var usr = ToUser(user);
            return Task.FromResult<bool>(usr.Role == roleName);
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
               var usr = ToUser(user);
               return Task.FromResult<string>(usr.PasswordHash);
               
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            return Task.FromResult<bool>(true);
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            return Task.Factory.StartNew(
           () =>
           {
               user.PasswordHash = passwordHash;
           });
        }

    }
}
