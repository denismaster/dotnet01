using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Models.Repositories;
using Courses.Models;
using System.Data.Entity;

namespace Courses.DAL
{
    public class CategoryRepository : ICategoryRepository
    {
        DatabaseContext context = new DatabaseContext();

        public IEnumerable<Models.Category> Get()
        {
            return context.Categories.AsEnumerable();
        }

        //временно нереализовано
        public IEnumerable<Category> Get(int page, int pageSize, Func<Category, bool> expression, SortFilter sortFilter)
        {
            return null;
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
            context.Entry(entity).State = EntityState.Deleted;
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

        public Category GetOnlyOne()
        {
            return context.Categories.FirstOrDefault();
        }
        public void Dispose()
        {
            context.Dispose();
        }
        public void ClearTable()
        {
            context.Categories.RemoveRange(Get());
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

                if (category.Products == null)
                    category.Products= new List<Product>();

                category.Products.Add(product);
                context.SaveChanges();
            }
        }
    }
}
