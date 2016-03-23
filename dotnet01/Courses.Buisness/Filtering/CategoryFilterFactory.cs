using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Buisness.Filtering
{
    public class CategoryFilterFactory : IFilterFactory<Models.Category>
    {
        public static Func<T, bool> And<T>(params Func<T, bool>[] predicates)
        {
            return t => predicates.All(predicate => predicate(t));
        }
        public Func<Models.Category, bool> GetFilterExpression(IEnumerable<FieldFilter> fieldFilters)
        {
            Func<Models.Category, bool> filterExp = acc => acc.CategoryId >= 0;

            foreach (var fieldFilter in fieldFilters)
            {
                switch (fieldFilter.Name)
                {
                    case "Name":
                        filterExp = acc => acc.Name.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper());
                        break;
                    case "CreatedDate":
                        filterExp = And(acc => acc.CreatedDate.ToString().Equals(fieldFilter.Value),filterExp);
                        break;
                    case "UpdatedDate":
                        filterExp = And(acc => acc.UpdatedDate.ToString().Equals(fieldFilter.Value),filterExp);
                        break;
                    case "Active":
                        filterExp = And(acc => acc.Active.ToString().Equals(fieldFilter.Value),filterExp);
                        break;
                    case "ParentId":
                        filterExp = And(acc => acc.ParentCategory.CategoryId.ToString().Equals(fieldFilter.Value),filterExp);
                        break;
                    default:
                        filterExp = And(acc => acc.CategoryId >= 0,filterExp);
                        break;
                }
            }
            return filterExp;
        }
    }
}
