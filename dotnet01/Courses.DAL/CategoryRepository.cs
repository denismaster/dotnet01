using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Models.Repositories;
using Courses.Models;
using System.Data.Entity;

namespace Courses.DAL
{
    public  class CategoryRepository: ICategoryRepository
    {
        DatabaseContext context = new DatabaseContext();

        public IEnumerable<Models.Category> Get()
        {
            return context.Categories.AsEnumerable();
        }

        //временно нереализовано
        public IEnumerable<Category> Get(int page, int pageSize, Func<Category, bool> expression, SortFilter sortFilter)
        {
            if (String.IsNullOrWhiteSpace(sortFilter.SortOrder))
            {
                return context.Categories.Where(expression).OrderBy(s => s.CategoryId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            switch (sortFilter.SortOrder)
            {
                case "Name":
                    return context.Categories.Where(expression).OrderBy(s => s.Name).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "NameDesc":
                    return context.Categories.Where(expression).OrderByDescending(s => s.Name).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                //case "Description":
                //    return context.Categories.Where(expression).OrderBy(s => s.Description).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                //case "DescriptionDesc":
                //    return context.Categories.Where(expression).OrderByDescending(s => s.Description).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "CreateDate":
                    return context.Categories.Where(expression).OrderBy(s => s.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "CreateDateDesc":
                    return context.Categories.Where(expression).OrderByDescending(s => s.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "UpdateDate":
                    return context.Categories.Where(expression).OrderBy(s => s.UpdatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "UpdateDateDesc":
                    return context.Categories.Where(expression).OrderByDescending(s => s.UpdatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Active":
                    return context.Categories.Where(expression).OrderBy(s => s.Active).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "ActiveDesc":
                    return context.Categories.Where(expression).OrderByDescending(s => s.Active).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "ParentId":
                    return context.Categories.Where(expression).OrderBy(s => s.ParentId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "ParentIdDesc":
                    return context.Categories.Where(expression).OrderByDescending(s => s.ParentId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                default:
                    return context.Categories.Where(expression).OrderBy(s => s.CategoryId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
        }
        public IEnumerable<Models.Category> Get(int page, int pageSize, Func<Models.Category, bool> expression)
        {
            return context.Categories.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public Models.Category Get(int id)
        {
            return context.Categories.Find(id);
        }

        public void Add(Models.Category entity)
        {
            context.Categories.Add(entity);
        }

        public void Update(Models.Category entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Models.Category entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public int Count(Func<Models.Category, bool> expression)
        {
            return context.Categories.Where(expression).Count();
        }


        public void Dispose()
        {
            context.Dispose();
        }
    }
}
