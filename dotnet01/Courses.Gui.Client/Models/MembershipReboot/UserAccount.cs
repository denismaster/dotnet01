using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BrockAllen.MembershipReboot;
namespace Courses.Gui.Client.Models.MembershipReboot
{
    public class UserAccount:BrockAllen.MembershipReboot.Relational.RelationalUserAccount
    {
        public new Guid ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }
        public new string Username
        {
            get
            {
                return base.Username;
            }
            set
            {
                base.Username = value;
            }
        }
        public new string Email
        {
            get
            {
                return base.Email;
            }
            set
            {
                base.Email = value;
            }
        }
        public new string HashedPassword
        {
            get
            {
                return base.HashedPassword;
            }
            set
            {
                base.HashedPassword = value;
            }
        }
        public new DateTime Created
        {
            get
            {
                return base.Created;
            }
            set
            {
                base.Created = value;
            }
        }
        public new DateTime LastUpdated
        {
            get
            {
                return base.LastUpdated;
            }
            set
            {
                base.LastUpdated = value;
            }
        }
        public new string Tenant
        {
            get
            {
                return base.Tenant;
            }
            set
            {
                base.Tenant = value;
            }
        }
    }
}