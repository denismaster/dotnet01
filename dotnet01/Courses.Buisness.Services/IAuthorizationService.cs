using Courses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Buisness.Services
{
    public interface IAuthorizationService
    {
        User FindByName(string username);

        User FindById(int id);
    }
}
