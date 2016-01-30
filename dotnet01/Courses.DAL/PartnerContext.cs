using Courses.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.DAL
{
    public class PartnerContext : DbContext
    {
        public PartnerContext() :
            base("AccountsDatabase")
        { }

        public DbSet<Partner> Partner { get; set; }
    }
}