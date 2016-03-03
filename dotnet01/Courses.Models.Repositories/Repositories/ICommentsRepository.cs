using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models.Repositories.Repositories
{
    interface ICommentsRepository
    {
        IEnumerable<Models.Comment> Get();

        IEnumerable<Models.Comment> Get(int page, int pageSize, Func<Models.Comment, bool> expression);

        Models.Comment Get(int id);

        void Add(Models.Comment entity);

        void Update(Models.Comment entity);

        void Delete(Models.Comment entity);


        void SaveChanges();



        int Count(Func<Models.Comment, bool> expression);


        void Dispose();
    }
}
