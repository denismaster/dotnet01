using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
namespace Courses.Authorization
{
    public class IdentityRole : IRole
    {
        public static IdentityRole Default = new IdentityRole("Guest");
        public IdentityRole()
        {
            Id = Guid.NewGuid().ToString();
        }
        public IdentityRole(string name)
            : this()
        {
            Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
