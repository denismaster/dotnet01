using Courses.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.DAL
{
    public class PartnerRepository : IPartnerRepository
    {
        /// <summary>
        /// Контекст Entity Framework, используем для работы с БД
        /// </summary>
        PartnerContext context = new PartnerContext();

        public IEnumerable<Models.Partner> Get()
        {
            return context.Partner.AsEnumerable();
        }

        public IEnumerable<Models.Partner> Get(int page, int pageSize, Func<Models.Partner, bool> expression)
        {
            //временное решение
            return context.Partner.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<Models.Partner> Get(int page, int pageSize, Func<Models.Partner, bool> expression, SortFilter sortFilter)
        {
            if (String.IsNullOrWhiteSpace(sortFilter.SortOrder))
            {
                return context.Partner.Where(expression).OrderBy(s => s.PartnerId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            switch (sortFilter.SortOrder)
            {
                case "Name":
                    return context.Partner.Where(expression).OrderBy(s => s.Name).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "NameDesc":
                    return context.Partner.Where(expression).OrderByDescending(s => s.Name).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
               
                case "CreatedDate":
                    return context.Partner.Where(expression).OrderBy(s => s.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "CreatedDateDesc":
                    return context.Partner.Where(expression).OrderByDescending(s => s.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "UpdatedDate":
                    return context.Partner.Where(expression).OrderBy(s => s.UpdatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "UpdatedDateDesc":
                    return context.Partner.Where(expression).OrderByDescending(s => s.UpdatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "UserID":
                    return context.Partner.Where(expression).OrderBy(s => s.UserId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "UserIDDesc":
                    return context.Partner.Where(expression).OrderByDescending(s => s.UserId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Address":
                    return context.Partner.Where(expression).OrderBy(s => s.Address).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "AddressDesc":
                    return context.Partner.Where(expression).OrderByDescending(s => s.Address).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Phone":
                    return context.Partner.Where(expression).OrderBy(s => s.Phone).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "PhoneDesc":
                    return context.Partner.Where(expression).OrderByDescending(s => s.Phone).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Email":
                    return context.Partner.Where(expression).OrderBy(s => s.Email).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "EmailDesc":
                    return context.Partner.Where(expression).OrderByDescending(s => s.Email).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Contact":
                    return context.Partner.Where(expression).OrderBy(s => s.Contact).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "ContactDesc":
                    return context.Partner.Where(expression).OrderByDescending(s => s.Contact).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                
                default:
                    return context.Partner.Where(expression).OrderBy(s => s.PartnerId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
        }

        public Models.Partner Get(int id)
        {
            return context.Partner.Find(id);
        }

        public void Add(Models.Partner entity)
        {
            context.Partner.Add(entity);
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
            return context.Partner.Where(expression).Count();
        }
    }
}

