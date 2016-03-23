using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Models.Repositories;
using Courses.Models;
using System.Data.Entity;

namespace Courses.DAL
{
    public class CategoryRepository :RepositoryBase<Category>, ICategoryRepository
    {
        public IEnumerable<Category> Get(int page, int pageSize, Func<Category, bool> expression, SortFilter sortFilter)
        {
            if (String.IsNullOrWhiteSpace(sortFilter.SortOrder))
            {
                return entityContext.Where(expression).OrderBy(s => s.CategoryId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
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

                case "CreateDate":
                    return entityContext.Where(expression).OrderBy(s => s.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "CreateDateDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "UpdateDate":
                    return entityContext.Where(expression).OrderBy(s => s.UpdatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "UpdateDateDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.UpdatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Active":
                    return entityContext.Where(expression).OrderBy(s => s.Active).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "ActiveDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.Active).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "ParentId":
                    return entityContext.Where(expression).OrderBy(s => s.ParentCategory.CategoryId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "ParentIdDesc":
                    return entityContext.Where(expression).OrderByDescending(s => s.ParentCategory.CategoryId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                default:
                    return entityContext.Where(expression).OrderBy(s => s.CategoryId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
        }
   
        public void AddPartners(int categoryId, int partnerId)
        { 
            using (DatabaseContext context = new DatabaseContext())
            { 

                Category category = new Category { CategoryId = categoryId };
                context.Categories.Add(category);
                context.Categories.Attach(category);

                Partner partner = new Partner { PartnerId = partnerId };
                context.Partners.Add(partner);
                context.Partners.Attach(partner);

                if (category.Partners == null)
                    category.Partners = new List<Partner>();

                category.Partners.Add(partner);
                context.SaveChanges();
            }
        }
        public void AddProducts(int categoryId,int productId)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                Category category = new Category { CategoryId = categoryId };
                context.Categories.Add(category);
                context.Categories.Attach(category);

                Product product = new Product { Id = productId };
                context.Products.Add(product);
                context.Products.Attach(product);

              

                category.Products.Add(product);
                context.SaveChanges();
            }
        }
    }
}
