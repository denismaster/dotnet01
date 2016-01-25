using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Buisness.Filtering
{
    public interface IFilterFactory<T>
    {
        Func<T, bool> GetFilterExpression(IEnumerable<FieldFilter> fieldFilters);
    }
}
