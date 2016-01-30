using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Buisness.Filtering
{
    public class PartnerFilterFactory : IFilterFactory<Models.Partner>
    {
        public Func<Models.Partner, bool> GetFilterExpression(IEnumerable<FieldFilter> fieldFilters)
        {
            Func<Models.Partner, bool> filterExp = acc => acc.PartnerId >= 0;

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
                    case "UserId":
                        filterExp += acc => acc.UserId.ToString().Equals(fieldFilter.Value);
                        break;
                    case "Address":
                        filterExp += acc => acc.Address.ToString().Equals(fieldFilter.Value);
                        break;
                    case "Phone":
                        filterExp += acc => acc.Phone.ToString().Equals(fieldFilter.Value);
                        break;
                    case "Email":
                        filterExp += acc => acc.Email.ToString().Equals(fieldFilter.Value);
                        break;
                    case "Contact":
                        filterExp += acc => acc.Contact.ToString().Equals(fieldFilter.Value);
                        break;
                    default:
                        filterExp += acc => acc.PartnerId >= 0;
                        break;
                }
            }
            return filterExp;
        }
    }
}
    