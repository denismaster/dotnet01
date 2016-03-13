using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BrockAllen.MembershipReboot;
using Courses.Models;
using Courses.Models.Repositories;
namespace Courses.Gui.Client.Models.MembershipReboot
{
    public class UserAccountRepository:IUserAccountRepository
    {
        private readonly IAccountRepository _repository;

        public UserAccountRepository(IAccountRepository repository)
        {
            if (repository != null)
                _repository = repository;
            else
                throw new ArgumentNullException();
        }

        public UserAccount Convert(User item)
        {
            return new Models.MembershipReboot.UserAccount()
            {
                Created = item.CreatedDate,
                LastUpdated = item.UpdatedDate,
                ID = Guid.Parse(item.AuthKey),
                Email = item.Email,
                Username = item.Login,
                Tenant = item.Role
            };
        }

        public void Add(BrockAllen.MembershipReboot.UserAccount item)
        {
            var user = new User();
            user.AuthKey = item.ID.ToString();
            user.Email = item.Email;
            user.CreatedDate = item.Created;
            user.UpdatedDate = item.LastUpdated;
            user.Login = item.Username;
            user.PasswordHash = item.HashedPassword;

            _repository.Add(user);
            _repository.SaveChanges();
        }

        public BrockAllen.MembershipReboot.UserAccount Create()
        {
            return new Models.MembershipReboot.UserAccount()
            {
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            };
        }

        public BrockAllen.MembershipReboot.UserAccount GetByCertificate(string tenant, string thumbprint)
        {
            throw new NotImplementedException();
        }

        public BrockAllen.MembershipReboot.UserAccount GetByEmail(string tenant, string email)
        {
            throw new NotImplementedException();
        }

        public BrockAllen.MembershipReboot.UserAccount GetByID(Guid id)
        {
            return Convert(_repository.GetUserByAuthKey(id.ToString()));
        }

        public BrockAllen.MembershipReboot.UserAccount GetByLinkedAccount(string tenant, string provider, string id)
        {
            throw new NotImplementedException();
        }

        public BrockAllen.MembershipReboot.UserAccount GetByMobilePhone(string tenant, string phone)
        {
            throw new NotImplementedException();
        }

        public BrockAllen.MembershipReboot.UserAccount GetByUsername(string tenant, string username)
        {
            throw new NotImplementedException();
        }

        public BrockAllen.MembershipReboot.UserAccount GetByUsername(string username)
        {
            return Convert(_repository.GetUserByName(username));
        }

        public BrockAllen.MembershipReboot.UserAccount GetByVerificationKey(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(BrockAllen.MembershipReboot.UserAccount item)
        {
            throw new NotImplementedException();
        }

        public void Update(BrockAllen.MembershipReboot.UserAccount item)
        {
            throw new NotImplementedException();
        }
    }
}