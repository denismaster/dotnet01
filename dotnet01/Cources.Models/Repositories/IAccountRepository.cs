using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models.Repositories
{
    public interface IAccountRepository: IRepository<User>
    {
        //Дополнительные действия, специфичные для аккаунтов.
        IEnumerable<Models.User> GetSorted(int page, int pageSize, Func<Models.User, bool> expression, string sortFilter);
    }
}
