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
    public class PartnerRepository : IPartnerRepository
    {
        /// <summary>
        /// Контекст Entity Framework, используем для работы с БД
        /// </summary>
        DatabaseContext context = new DatabaseContext();

        public IEnumerable<Models.Partner> Get()
        {
            return context.Partners.AsEnumerable();
        }

        public IEnumerable<Models.Partner> Get(int page, int pageSize, Func<Models.Partner, bool> expression)
        {
            //временное решение
            return context.Partners.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<Models.Partner> Get(int page, int pageSize, Func<Models.Partner, bool> expression, SortFilter sortFilter)
        {
            if (String.IsNullOrWhiteSpace(sortFilter.SortOrder))
            {
                return context.Partners.Where(expression).OrderBy(s => s.PartnerId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            switch (sortFilter.SortOrder)
            {
                case "Name":
                    return context.Partners.Where(expression).OrderBy(s => s.Name).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "NameDesc":
                    return context.Partners.Where(expression).OrderByDescending(s => s.Name).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
               
                case "CreatedDate":
                    return context.Partners.Where(expression).OrderBy(s => s.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "CreatedDateDesc":
                    return context.Partners.Where(expression).OrderByDescending(s => s.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "UpdatedDate":
                    return context.Partners.Where(expression).OrderBy(s => s.UpdatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "UpdatedDateDesc":
                    return context.Partners.Where(expression).OrderByDescending(s => s.UpdatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "UserID":
                    return context.Partners.Where(expression).OrderBy(s => s.UserId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "UserIDDesc":
                    return context.Partners.Where(expression).OrderByDescending(s => s.UserId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Address":
                    return context.Partners.Where(expression).OrderBy(s => s.Address).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "AddressDesc":
                    return context.Partners.Where(expression).OrderByDescending(s => s.Address).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Phone":
                    return context.Partners.Where(expression).OrderBy(s => s.Phone).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "PhoneDesc":
                    return context.Partners.Where(expression).OrderByDescending(s => s.Phone).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Email":
                    return context.Partners.Where(expression).OrderBy(s => s.Email).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "EmailDesc":
                    return context.Partners.Where(expression).OrderByDescending(s => s.Email).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Contact":
                    return context.Partners.Where(expression).OrderBy(s => s.Contact).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "ContactDesc":
                    return context.Partners.Where(expression).OrderByDescending(s => s.Contact).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                
                default:
                    return context.Partners.Where(expression).OrderBy(s => s.PartnerId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
        }

        public Models.Partner Get(int id)
        {
            return context.Partners.Find(id);
        }

        public void Add(Models.Partner entity)
        {
            context.Partners.Add(entity);
        }

        public void Update(Models.Partner entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Models.Partner entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public int Count(Func<Models.Partner, bool> expression)
        {
            return context.Partners.Where(expression).Count();
        }
    }
}

