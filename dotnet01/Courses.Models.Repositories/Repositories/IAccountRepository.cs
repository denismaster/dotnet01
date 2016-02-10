using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models.Repositories
{
    public interface IAccountRepository: IRepository<User>,IDisposable
    {
        //Дополнительные действия, специфичные для аккаунтов.
        User GetUser(string login, string password);
        Task<User> GetUserByID(int id);
        Task<User> GetUserByName(string username);
       
    }
}
