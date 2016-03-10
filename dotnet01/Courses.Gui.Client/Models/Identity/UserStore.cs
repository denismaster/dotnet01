using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Courses.Models.Repositories;
using Entities = Courses.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
namespace Courses.Gui.Client.Models.Identity
{
    public class UserStore : IUserLoginStore<UserModel>, 
        IUserClaimStore<UserModel>, 
        IUserRoleStore<UserModel>,
        IUserPasswordStore<UserModel>, 
        IUserLockoutStore<UserModel,string>,
        IUserStore<UserModel>, IDisposable
    {
        private readonly IAccountRepository _repository;
        private static int _lastId;
        public UserStore(IAccountRepository repository)
        {
            if (repository != null)
                _repository = repository;
            else
                throw new ArgumentNullException();
        }
        #region Private Methods
        private Entities.User getUser(UserModel identityUser)
        {
            if (identityUser == null)
                return null;

            var user = new Entities.User();
            populateUser(user, identityUser);

            return user;
        }

        private void populateUser(Entities.User user, UserModel identityUser)
        {
            user.Id = identityUser.Id;
            user.Login = identityUser.UserName;
            user.PasswordHash = identityUser.PasswordHash;
            user.CreatedDate = DateTime.Now;
            user.UpdatedDate = DateTime.Now;
        }

        private UserModel getUserModel(Entities.User user)
        {
            if (user == null)
                return null;

            var identityUser = new UserModel(user.Id);
            populateUserModel(identityUser, user);

            return identityUser;
        }

        private void populateUserModel(UserModel identityUser, Entities.User user)
        {
            identityUser.Id = user.Id;
            identityUser.UserName = user.Login;
            identityUser.PasswordHash = user.PasswordHash;
            //identityUser.SecurityStamp = user.SecurityStamp;
        }
        #endregion
        public Task CreateAsync(UserModel user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = getUser(user);

            _repository.Add(u);
            _repository.SaveChanges();
            _lastId = u.Id;
            return Task.FromResult<int>(0);
        }
        public Task DeleteAsync(UserModel user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = getUser(user);

            _repository.Delete(u);
            _repository.SaveChanges();
            return Task.FromResult<int>(0);
        }
        public Task<UserModel> FindByIdAsync(string userId)
        {
            var user = _repository.GetUserByID(userId);
            return Task.FromResult<UserModel>(getUserModel(user));
        }

        public Task<UserModel> FindByNameAsync(string userName)
        {
            var user =_repository.GetUserByName(userName);
            if (user != null) _lastId = user.Id;
            return Task.FromResult<UserModel>(getUserModel(user));
        }

        public Task UpdateAsync(UserModel user)
        {
            if (user == null)
                throw new ArgumentException("user");

            var u = _repository.GetUserByID(user.Id.ToString());
            if (u == null)
                throw new ArgumentException("UserModel does not correspond to a User entity.", "user");

            populateUser(u, user);

            _repository.Update(u);
            _repository.SaveChanges();
            return Task.FromResult<int>(0);
        }

        #region IDisposable Members
        public void Dispose()
        {
            // Dispose does nothing since we want Unity to manage the lifecycle of our Unit of Work
        }
        #endregion

        #region IUserClaimStore<UserModel, int> Members
        public Task AddClaimAsync(UserModel user, Claim claim)
        {
            //if (user == null)
            //    throw new ArgumentNullException("user");
            //if (claim == null)
            //    throw new ArgumentNullException("claim");

            //var u = _repository.GetUserByID(user.Id);
            //if (u == null)
            //    throw new ArgumentException("UserModel does not correspond to a User entity.", "user");

            //var c = new Entities.Claim
            //{
            //    ClaimType = claim.Type,
            //    ClaimValue = claim.Value,
            //    User = u
            //};
            //u.Claims.Add(c);

            //_repository.Update(u);
            //return _unitOfWork.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public Task<IList<Claim>> GetClaimsAsync(UserModel user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = _repository.GetUserByID(user.Id.ToString());
            if (u == null)
                throw new ArgumentException("UserModel does not correspond to a User entity.", "user");



            //return Task.FromResult<IList<Claim>>(u.Claims.Select(x => new Claim(x.ClaimType, x.ClaimValue)).ToList());
            return Task.FromResult<IList<Claim>>(new List<Claim>());
        }

        public Task RemoveClaimAsync(UserModel user, Claim claim)
        {
            //if (user == null)
            //    throw new ArgumentNullException("user");
            //if (claim == null)
            //    throw new ArgumentNullException("claim");

            //var u = _repository.GetUserByID(user.Id);
            //if (u == null)
            //    throw new ArgumentException("UserModel does not correspond to a User entity.", "user");

            //var c = u.Claims.FirstOrDefault(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            //u.Claims.Remove(c);

            //_repository.Update(u);
            //return _unitOfWork.SaveChangesAsync();

            throw new NotImplementedException();

        }
        #endregion

        #region IUserLoginStore<UserModel, int> Members
        public Task AddLoginAsync(UserModel user, UserLoginInfo login)
        {
            //if (user == null)
            //    throw new ArgumentNullException("user");
            //if (login == null)
            //    throw new ArgumentNullException("login");

            //var u = _repository.GetUserByID(user.Id);
            //if (u == null)
            //    throw new ArgumentException("UserModel does not correspond to a User entity.", "user");

            //var l = new Entities.ExternalLogin
            //{
            //    LoginProvider = login.LoginProvider,
            //    ProviderKey = login.ProviderKey,
            //    User = u
            //};
            //u.Logins.Add(l);

            //_repository.Update(u);
            //return _unitOfWork.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public Task<UserModel> FindAsync(UserLoginInfo login)
        {
            //if (login == null)
            //    throw new ArgumentNullException("login");

            //var identityUser = default(UserModel);

            //var l = _unitOfWork.ExternalLoginRepository.GetByProviderAndKey(login.LoginProvider, login.ProviderKey);
            //if (l != null)
            //    identityUser = getUserModel(l.User);

            //return Task.FromResult<UserModel>(identityUser);
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(UserModel user)
        {
            //if (user == null)
            //    throw new ArgumentNullException("user");

            //var u = _repository.GetUserByID(user.Id);
            //if (u == null)
            //    throw new ArgumentException("UserModel does not correspond to a User entity.", "user");

            //return Task.FromResult<IList<UserLoginInfo>>(u.Logins.Select(x => new UserLoginInfo(x.LoginProvider, x.ProviderKey)).ToList());
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(UserModel user, UserLoginInfo login)
        {
            //if (user == null)
            //    throw new ArgumentNullException("user");
            //if (login == null)
            //    throw new ArgumentNullException("login");

            //var u = _repository.GetUserByID(user.Id);
            //if (u == null)
            //    throw new ArgumentException("UserModel does not correspond to a User entity.", "user");

            //var l = u.Logins.FirstOrDefault(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey);
            //u.Logins.Remove(l);

            //_repository.Update(u);
            //return _unitOfWork.SaveChangesAsync();
            throw new NotImplementedException();
        }
        #endregion

        #region IUserRoleStore<UserModel, int> Members
        public Task AddToRoleAsync(UserModel user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: roleName.");

            var u = _repository.GetUserByID(user.Id.ToString());
            if (u == null)
                throw new ArgumentException("UserModel does not correspond to a User entity.", "user");
            u.Role = roleName;
            _repository.Update(u);
            _repository.SaveChanges();
            return Task.FromResult<int>(0);
        }

        public  Task<IList<string>> GetRolesAsync(UserModel user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = _repository.GetUserByID(user.Id.ToString());
            if (u == null)
                throw new ArgumentException("UserModel does not correspond to a User entity.", "user");
            var roles = new List<string>();
            roles.Add(u.Role);
            return Task.FromResult<IList<string>>(roles);
        }

        public Task<bool> IsInRoleAsync(UserModel user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: role.");

            var u =  _repository.GetUserByID(user.Id.ToString());
            if (u == null)
                throw new ArgumentException("UserModel does not correspond to a User entity.", "user");
            return Task.FromResult<bool>(u.Role == roleName);
        }

        public Task RemoveFromRoleAsync(UserModel user, string roleName)
        {
            //if (user == null)
            //    throw new ArgumentNullException("user");
            //if (string.IsNullOrWhiteSpace(roleName))
            //    throw new ArgumentException("Argument cannot be null, empty, or whitespace: role.");

            //var u = _repository.GetUserByID(user.Id);
            //if (u == null)
            //    throw new ArgumentException("UserModel does not correspond to a User entity.", "user");

            //var r = u.Roles.FirstOrDefault(x => x.Name == roleName);
            //u.Roles.Remove(r);

            //_repository.Update(u);
            //return _unitOfWork.SaveChangesAsync();
            throw new NotImplementedException();
        }
        #endregion

        #region IUserPasswordStore<UserModel, int> Members
        public Task<string> GetPasswordHashAsync(UserModel user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<string>(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(UserModel user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<bool>(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(UserModel user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }
        #endregion

        public Task<int> GetAccessFailedCountAsync(UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetLockoutEnabledAsync(UserModel user)
        {
            return Task.FromResult<bool>(false);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEnabledAsync(UserModel user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(UserModel user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }
    }
}