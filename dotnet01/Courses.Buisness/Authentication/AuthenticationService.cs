using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Buisness.Services;
using Courses.Models.Repositories;

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

        public Boolean IsValid(string login, string password)
        {
            var user = repository.GetUser(login, password);
            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}
