using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models.Repositories
{
    public interface ICommentsRepository : IRepository<Comment>, IDisposable
    {
        Comment GetOnlyOne();
    }
}
