using Microsoft.AspNet.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Courses.Authorization
{
    public class IdentityUser:IUser<int>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync( UserManager<IdentityUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public IdentityUser(int id, string userName)
        {
            this.Id = id;
            this.UserName = userName;
        }
        public int Id
        {
            get;
            private set;
        }

        public string UserName
        {
            get;
            set;
        }
        public string PasswordHash 
        { 
            get; set;
        }
        public string Email
        {
            get;
            set;
        }
        
    }
}
