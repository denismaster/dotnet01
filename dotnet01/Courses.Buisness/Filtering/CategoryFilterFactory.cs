using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Buisness.Filtering
{
    public class CategoryFilterFactory : IFilterFactory<Models.Category>
    {
        public Func<Models.Category, bool> GetFilterExpression(IEnumerable<FieldFilter> fieldFilters)
        {
            Func<Models.Category, bool> filterExp = acc => acc.CategoryId >= 0;

            foreach (var fieldFilter in fieldFilters)
            {
                switch (fieldFilter.Name)
                {
                    case "Name":
                        filterExp += acc => acc.Name.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper());
                        break;
                    case "CreatedDate":
                        filterExp += acc => acc.CreatedDate.ToString().Equals(fieldFilter.Value);
                        break;
                    case "UpdatedDate":
                        filterExp += acc => acc.UpdatedDate.ToString().Equals(fieldFilter.Value);
                        break;
                    case "Active":
                        filterExp += acc => acc.Active.ToString().Equals(fieldFilter.Value);
                        break;
                    case "ParentId":
                        filterExp += acc => acc.ParentCategory.CategoryId.ToString().Equals(fieldFilter.Value);
                        break;
                    default:
                        filterExp += acc => acc.CategoryId >= 0;
                        break;
                }
            }
            return filterExp;
        }
    }
}
