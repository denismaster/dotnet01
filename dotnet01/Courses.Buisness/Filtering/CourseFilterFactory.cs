using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Buisness.Filtering
{
    public class CourseFilterFactory : IFilterFactory<Models.Course>
    {
        public Func<Models.Course, bool> GetFilterExpression(IEnumerable<FieldFilter> fieldFilters)
        {
            Func<Models.Course, bool> filterExp = acc => acc.Id >= 0;
            foreach (var fieldFilter in fieldFilters)
            {
                switch (fieldFilter.Name)
                {
                    case "Title":
                        filterExp += acc => acc.Title.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper());
                        break;
                    case "Location":
                        filterExp += acc => acc.Location.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper());
                        break;
                    case "Organizer":
                        filterExp += acc => acc.Organizer.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper());
                        break;
                    default:
                        filterExp += acc => acc.Id >= 0;
                        break;
                }
            }
            return filterExp;
        }
    }
}
