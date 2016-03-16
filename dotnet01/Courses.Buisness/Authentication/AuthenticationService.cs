using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Buisness.Services;
using Courses.Models.Repositories;
using Courses.Models;
using System.Security.Claims;
namespace Courses.Buisness.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountRepository repository;
        private readonly IPasswordHasher passwordHasher;
        public AuthenticationService(Models.Repositories.IAccountRepository repository, IPasswordHasher hasher)
        {
            if (repository == null)
                throw new ArgumentNullException("Repository is null!");
            if (hasher == null)
                throw new ArgumentNullException("Hasher is null");
            this.repository = repository;
            this.passwordHasher = hasher;
        }
        public User Find(int id)
        {
            if (id<=0)
            {
                throw new ArgumentException("userId");
            }

            return repository.Get(id);
        }
        public User Find(string username)
        {
            if(String.IsNullOrEmpty(username))
            {
                throw new ArgumentException("username");
            }

            return repository.GetUserByName(username);
        }

        public User Find(string username, string password)
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new ArgumentException("username");
            }
            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentException("password");
            }
            var user = repository.GetUserByPassword(username, passwordHasher.Hash(password));

            return user;
        }
        public User FindExternal(string authKey)
        {
            var user = repository.GetUserByAuthKey(authKey);
            return user;
        }
        public bool Register(string username,string password)
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new ArgumentException("username");
            }
            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentException("password");
            }
            if(Find(username)!=null)
            {
                return false;
            }
            var user = new User()
            {
                Login = username,
                Email = username,
                PasswordHash = passwordHasher.Hash(password),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Role = Roles.Default.ToString()
            };

            repository.Add(user);
            repository.SaveChanges();

            return true;
        }

        public bool Register(string username, string authkey, string provider)
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new ArgumentException("username");
            }
            if (String.IsNullOrEmpty(authkey))
            {
                throw new ArgumentException("authkey");
            }
            if (String.IsNullOrEmpty(provider))
            {
                throw new ArgumentException("provider");
            }
            if (Find(username) != null)
            {
                return false;
            }
            var user = new User()
            {
                Login = username,
                AuthKey = authkey,
                ProviderName = provider,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Role = Roles.Default.ToString()
            };

            repository.Add(user);
            repository.SaveChanges();

            return true;
        }

        public void LinkExternalLogin(User user, string authkey, string providername)
        {
            user.ProviderName = providername;
            user.AuthKey = authkey;

            repository.Update(user);
            repository.SaveChanges();
        }

        public void UnlinkExternalLogin(User user)
        {
            user.ProviderName = null;
            user.AuthKey = null;

            repository.Update(user);
            repository.SaveChanges();
        }

        public ClaimsIdentity GetIdentity(User user)
        {
            if(user==null)
            {
                throw new ArgumentNullException("user");
            }
 
            var claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login, ClaimValueTypes.String));
            claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                "OWIN Provider", ClaimValueTypes.String));
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role, ClaimValueTypes.String));
            if(user.AuthKey!=null)
            {
                claim.AddClaim(new Claim("AuthKey",user.AuthKey, ClaimValueTypes.String));
            }
            return claim;
        }
 
    }
}
