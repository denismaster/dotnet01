using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Buisness.Filtering
{
    public class PartnerFilterFactory : IFilterFactory<Models.Partner>
    {
        public static Func<T, bool> And<T>(params Func<T, bool>[] predicates)
        {
            return t => predicates.All(predicate => predicate(t));
        }
        public Func<Models.Partner, bool> GetFilterExpression(IEnumerable<FieldFilter> fieldFilters)
        {
            Func<Models.Partner, bool> filterExp = acc => acc.PartnerId >= 0;

            foreach (var fieldFilter in fieldFilters)
            {
                switch (fieldFilter.Name)
                {
                    case "Name":
                        filterExp = And(acc => acc.Name.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper()),filterExp);
                        break;
                    case "CreatedDate":
                        filterExp = And(acc => acc.CreatedDate.ToString().Equals(fieldFilter.Value),filterExp);
                        break;
                    case "UpdatedDate":
                        filterExp = And(filterExp,acc => acc.UpdatedDate.ToString().Equals(fieldFilter.Value));
                        break;
                    case "UserId":
                        filterExp = And(filterExp,acc => acc.UserId.ToString().Equals(fieldFilter.Value));
                        break;
                    case "Address":
                        filterExp = And(filterExp,acc => acc.Address.ToString().Equals(fieldFilter.Value));
                        break;
                    case "Phone":
                        filterExp = And(filterExp,acc => acc.Phone.ToString().Equals(fieldFilter.Value));
                        break;
                    case "Email":
                        filterExp = And(filterExp,acc => acc.Email.ToString().Equals(fieldFilter.Value));
                        break;
                    case "Contact":
                        filterExp = And(filterExp,acc => acc.Contact.ToString().Equals(fieldFilter.Value));
                        break;
                    default:
                        filterExp = And(filterExp,acc => acc.PartnerId >= 0);
                        break;
                }
            }
            return filterExp;
        }
    }
}
    