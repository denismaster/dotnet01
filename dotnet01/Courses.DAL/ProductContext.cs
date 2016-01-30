using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Courses.Models;

namespace Courses.DAL
{
    public class ProductContext : DbContext
    {
        public ProductContext() :
            base("ProductsDatabase")
        { }

        public DbSet<Product> Product { get; set; }
    }
}
