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
    /// <summary>
    /// Реализация репозитория для работы с курсами
    /// </summary>
    public class ProductRepository  :RepositoryBase<Product>, IProductRepository
    {
   
        public IEnumerable<Models.Product> Get(int page, int pageSize, Func<Models.Product, bool> expression, SortFilter sortFilter)
        {
            if (string.IsNullOrWhiteSpace(sortFilter.SortOrder))
            {
                return entityContext.Where(expression).OrderBy(s => s.Id).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            switch (sortFilter.SortOrder)
            {
                case "Name":
                    return entityContext.Where(expression).OrderBy(s => s.Name).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "NameDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.Name).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Description":
                    return entityContext.Where(expression).OrderBy(s => s.Description).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "DescriptionDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.Description).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "CreatedDate":
                    return entityContext.Where(expression).OrderBy(s => s.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "CreatedDateDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "UpdatedDate":
                    return entityContext.Where(expression).OrderBy(s => s.UpdatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "UpdatedDateDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.UpdatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Active":
                    return entityContext.Where(expression).OrderBy(s => s.Active).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "ActiveDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.Active).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "Type":
                    return entityContext.Where(expression).OrderBy(s => s.Type).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "TypeDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.Type).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "PartnerId":
                    return entityContext.Where(expression).OrderBy(s => s.PartnerId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "PartnerIdDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.PartnerId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "Teacher":
                    return entityContext.Where(expression).OrderBy(s => s.Teacher).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "TeacherDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.Teacher).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "SeatsCount":
                    return entityContext.Where(expression).OrderBy(s => s.SeatsCount).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "SeatsCountDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.SeatsCount).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "AssignedUserID":
                    return entityContext.Where(expression).OrderBy(s => s.AssignedUserId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "AssignedUserIDDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.AssignedUserId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Location":
                    return entityContext.Where(expression).OrderBy(s => s.Location).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "LocationDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.Location).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                default:
                    return entityContext.Where(expression).OrderBy(s => s.Id).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
        }
    
    }
}
