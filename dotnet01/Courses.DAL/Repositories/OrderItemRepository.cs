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
    class OrderItemRepository:RepositoryBase<OrderItem>,IOrderItemRepository
    {

    }
}
