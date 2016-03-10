using Microsoft.AspNet.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
namespace Courses.Gui.Client.Models.Identity
{
    public class UserModel : IUser<int>, IUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserModel> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public UserModel(int id)
        {
            this.Id=id;
        }

        public UserModel(int id,string userName)
            : this(id)
        {
            this.UserName = userName;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public virtual string PasswordHash { get; set; }

        string IUser<string>.Id
        {
            get { return Id.ToString(); }
        }
    }
}