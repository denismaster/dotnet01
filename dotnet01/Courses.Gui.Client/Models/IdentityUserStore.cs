﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Courses.Authorization;
using Courses.Models;
using Courses.Models.Repositories;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Courses.Gui.Client.Models;
namespace Courses.Gui.Client
{
    public class ApplicationUserStore : IUserStore<ApplicationUser, int>,
                                   IUserEmailStore<ApplicationUser, int>,
                                   IUserRoleStore<ApplicationUser, int>,
                                   IUserPasswordStore<ApplicationUser, int>,
        IUserClaimStore<ApplicationUser, int>,
                                   IDisposable
    {
        private readonly IAccountRepository _repository;

        public ApplicationUserStore(IAccountRepository repository)
        {
            if (repository != null)
                _repository = repository;
            else
                throw new ArgumentNullException();
        }
        private User ToUser(ApplicationUser user)
        {
            var usr = _repository.GetUser(user.UserName, user.PasswordHash);
            return usr;
        }
        private User ConvertToUser(ApplicationUser user)
        {
            Console.WriteLine(user.Id);
            return new User()
            {
                CreatedDate = DateTime.Now,
                Email = user.UserName,
                PasswordHash = user.PasswordHash,
            };
        }
        public Task CreateAsync(ApplicationUser user)
        {
            _repository.Add(ConvertToUser(user));
            var usr = _repository.GetUser(user.UserName, user.PasswordHash);
            return Task.FromResult<int>(0);
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            _repository.Delete(ConvertToUser(user));
            return Task.FromResult<int>(0);
        }

        public async Task<ApplicationUser> FindByIdAsync(int userId)
        {
            var user = await _repository.GetUserByID(userId);
            if (user != null)
            {
                return new ApplicationUser{Id = user.Id, UserName= user.Login};
            }
            return null;
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var user = await _repository.GetUserByEmail(userName);
            if (user != null)
            {
                return new ApplicationUser { Id = user.Id, UserName = user.Login };
            }
            return null;
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            var usr = ConvertToUser(user);
            _repository.Update(usr);
            return Task.FromResult<int>(0);
        }

        public void Dispose()
        {

        }

        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(ApplicationUser user)
        {
            var usr = _repository.GetUser(user.UserName, user.PasswordHash);
            if(usr!=null)
            return Task.FromResult<string>(usr.Email);
            return Task.FromResult<string>("");
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(ApplicationUser user, string email)
        {
            var usr = _repository.GetUser(user.UserName, user.PasswordHash);
            if (usr != null)
                usr.Email = email;
            return Task.Delay(1);
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(ApplicationUser user, string roleName)
        {
            return Task.Factory.StartNew(
            () =>
            {
                var usr = ToUser(user);
                usr.Role = roleName;
                _repository.Update(usr);
            });
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            var usr = ToUser(user);
            var roles = new List<string>();
            roles.Add(usr.Role);
            return Task.FromResult<IList<string>>(roles);
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            var usr = ToUser(user);
            return Task.FromResult<bool>(usr.Role == roleName);
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            var usr = ToUser(user);
            return Task.FromResult<string>(usr.PasswordHash);

        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            return Task.FromResult<bool>(true);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            return Task.Factory.StartNew(
           () =>
           {
               user.PasswordHash = passwordHash;
           });
        }


        public Task AddClaimAsync(ApplicationUser user, System.Security.Claims.Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task<IList<System.Security.Claims.Claim>> GetClaimsAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(ApplicationUser user, System.Security.Claims.Claim claim)
        {
            throw new NotImplementedException();
        }
    }
}