using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Buisness.Filtering
{
    
    public class AccountFilterFactory : IFilterFactory<Models.User>
    {

        public static Func<T, bool> And<T>(params Func<T, bool>[] predicates)
        {
            return t => predicates.All(predicate => predicate(t));
        }

        public Func<Models.User, bool> GetFilterExpression(IEnumerable<FieldFilter> fieldFilters)
        {
            Func<Models.User, bool> filterExp = acc => acc.Id >= 0;
            foreach (var fieldFilter in fieldFilters)
            {
                switch (fieldFilter.Name)
                {
                    case "LogIn":
                        filterExp = And(acc => acc.Login.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper()), filterExp);
                        break;
                    case "Role":
                        filterExp = And(acc => acc.Role.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper()), filterExp);
                        break;
                    case "Mail":
                        filterExp = And(acc => acc.Email.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper()),filterExp);
                        break;
                    default:
                        filterExp = And(acc => acc.Id >= 0,filterExp);
                        break;
                }
            }
            return filterExp;
        }
    }
}
