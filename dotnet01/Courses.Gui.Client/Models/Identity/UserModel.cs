using Microsoft.AspNet.Identity;
using System;

namespace Courses.Gui.Client.Models.Identity
{
    public class UserModel : IUser<int>
    {
        public UserModel()
        {
            this.Id=0;
        }

        public UserModel(string userName)
            : this()
        {
            this.UserName = userName;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public virtual string PasswordHash { get; set; }
    }
}