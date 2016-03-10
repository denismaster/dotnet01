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

        public AuthenticationService(Models.Repositories.IAccountRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("Repository is null!");
            this.repository = repository;
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
            var user = repository.GetUser(username, password);

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
                PasswordHash = password,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Role = Roles.Default.ToString()
            };

            repository.Add(user);
            repository.SaveChanges();

            return true;
        }

        public ClaimsIdentity GetIdentity(User user)
        {
            if(user==null)
            {
                throw new ArgumentNullException("user");
            } 
            var claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));
            claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                "OWIN Provider", ClaimValueTypes.String));
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role, ClaimValueTypes.String));

            return claim;
        }
 
    }
}
