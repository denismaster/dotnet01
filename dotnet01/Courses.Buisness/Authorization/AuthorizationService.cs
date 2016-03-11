using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Buisness.Services;
using Courses.Models;
using Courses.Models.Repositories;

namespace Courses.Buisness.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAccountRepository repository;

        public AuthorizationService(Models.Repositories.IAccountRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("Repository is null!");
            this.repository = repository;
        }

        public User FindById(int id)
        {
            var user = repository.GetUserByID(id);
            return user;
        }

        public User FindByName(string username)
        {
            var user = repository.GetUserByName(username);
            return user;
        }
    }
}
