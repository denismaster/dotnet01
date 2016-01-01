using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Buisness.Filtering
{
    /*
¨¨¨¨¨¨¨¨¨★ 
¨¨¨¨¨¨¨¨¨**
¨¨¨¨¨¨¨¨¨*o*
¨¨¨¨¨¨¨¨*♥*o*
¨¨¨¨¨¨¨***o***
¨¨¨¨¨¨**o**♥*o*
¨¨¨¨¨**♥**o**o**
¨¨¨¨**o**♥***♥*o*
¨¨¨*****♥*o**o****
¨¨**♥**o*****o**♥**
¨******o*****♥**o***
****o***♥**o***o***♥ *
¨¨¨¨¨____!_!____
¨¨¨¨¨\_________/¨¨ 
 С НОВЫМ ГОДОМ, ДРУЗЬЯ!)
    */
    public class AccountFilterFactory : IFilterFactory<Models.Account>
    {
        public Func<Models.Account, bool> GetFilterExpression(IEnumerable<FieldFilter> fieldFilters, SortFilter sortFilter)
        {
            Func<Models.Account, bool> filterExp = acc => acc.Id >= 0;
            foreach (var fieldFilter in fieldFilters)
            {
                switch (fieldFilter.Name)
                {
                    case "LogIn":
                        filterExp += acc => acc.Login.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper());
                        break;
                    case "Role":
                        filterExp += acc => acc.Role.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper());
                        break;
                    case "Mail":
                        filterExp += acc => acc.Mail.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper());
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
