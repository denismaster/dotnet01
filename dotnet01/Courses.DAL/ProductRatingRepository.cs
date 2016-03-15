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
    class ProductRatingRepository:IProductRatingRepository
    {
        DatabaseContext context = new DatabaseContext();

        public IEnumerable<ProductRating> Get()
        {
            return context.ProductRatings.AsEnumerable();
        }
        public IEnumerable<ProductRating> Get(int page, int pageSize, Func<ProductRating, bool> expression, SortFilter sortFilter)
        {
            return null;
        }
        public IEnumerable<ProductRating> Get(int page, int pageSize, Func<ProductRating, bool> expression)
        {
            return context.ProductRatings.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public ProductRating Get(int id)
        {
            return context.ProductRatings.Find(id);
        }

        public void Add(ProductRating entity)
        {
            context.ProductRatings.Add(entity);
        }

        public void Update(ProductRating entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(ProductRating entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public int Count(Func<ProductRating, bool> expression)
        {
            return context.ProductRatings.Where(expression).Count();
        }

        public void Dispose()
        {
            context.Dispose();
        }
        public ProductRating GetOnlyOne()
        {
            return context.ProductRatings.FirstOrDefault();
        }
    }
}
