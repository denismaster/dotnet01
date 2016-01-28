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
    public class AccountFilterFactory : IFilterFactory<Models.User>
    {
        public Func<Models.User, bool> GetFilterExpression(IEnumerable<FieldFilter> fieldFilters)
        {
            Func<Models.User, bool> filterExp = acc => acc.Id >= 0;
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
                        filterExp += acc => acc.Email.Trim().ToUpper().Contains(fieldFilter.Value.Trim().ToUpper());
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
