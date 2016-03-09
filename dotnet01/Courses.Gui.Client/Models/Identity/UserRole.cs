using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
namespace Courses.Gui.Client.Models.Identity
{
    public class UserRole: IRole<int>
    {
        public UserRole()
        {
            this.Id = 0;
        }

        public UserRole(string name)
            : this()
        {
            this.Name = name;
        }

        public UserRole(string name, int id)
        {
            this.Name = name;
            this.Id = id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}