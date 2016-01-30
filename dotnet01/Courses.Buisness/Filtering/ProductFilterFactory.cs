using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Buisness.Filtering
{
    public class ProductFilterFactory : IFilterFactory<Models.Product>
    {
        public Func<Models.Product, bool> GetFilterExpression(IEnumerable<FieldFilter> fieldFilters)
        {
            Func<Models.Product, bool> filterExp = acc => acc.Id >= 0;

            foreach (var fieldFilter in fieldFilters)
            {
                switch (fieldFilter.Name)
                {
                    case "Name":
                        filterExp += acc => acc.Name.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper());
                        break;
                    case "Teacher":
                        filterExp += acc => acc.Teacher.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper());
                        break;
                    case "Location":
                        filterExp += acc => acc.Location.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper());
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
                    case "PartnerID":
                        filterExp += acc => acc.PartnerId.ToString().Equals(fieldFilter.Value);
                        break;
                    case "SeatsCount":
                        filterExp += acc => acc.SeatsCount.ToString().Equals(fieldFilter.Value);
                        break;
                    case "AssignedUserID":
                        filterExp += acc => acc.AssignedUserId.ToString().Equals(fieldFilter.Value);
                        break;
                    case "Type":
                        filterExp += acc => acc.Type.ToString().Equals(fieldFilter.Value);
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
