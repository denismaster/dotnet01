using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models.Repositories;
using Courses.Models;
using System.Data.Entity;

namespace Courses.DAL
{
    public class PartnerRepository :RepositoryBase<Partner>, IPartnerRepository
    {
   
     
        public IEnumerable<Models.Partner> Get(int page, int pageSize, Func<Models.Partner, bool> expression, SortFilter sortFilter)
        {
            if (string.IsNullOrWhiteSpace(sortFilter.SortOrder))
            {
                return entityContext.Where(expression).OrderBy(s => s.PartnerId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            switch (sortFilter.SortOrder)
            {
                case "Name":
                    return entityContext.Where(expression).OrderBy(s => s.Name).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "NameDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.Name).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
               
                case "CreatedDate":
                    return entityContext.Where(expression).OrderBy(s => s.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "CreatedDateDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "UpdatedDate":
                    return entityContext.Where(expression).OrderBy(s => s.UpdatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "UpdatedDateDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.UpdatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "UserID":
                    return entityContext.Where(expression).OrderBy(s => s.UserId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "UserIDDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.UserId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Address":
                    return entityContext.Where(expression).OrderBy(s => s.Address).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "AddressDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.Address).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Phone":
                    return entityContext.Where(expression).OrderBy(s => s.Phone).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "PhoneDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.Phone).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Email":
                    return entityContext.Where(expression).OrderBy(s => s.Email).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "EmailDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.Email).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Contact":
                    return entityContext.Where(expression).OrderBy(s => s.Contact).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "ContactDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.Contact).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                
                default:
                    return entityContext.Where(expression).OrderBy(s => s.PartnerId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
        }

      
    }
}

