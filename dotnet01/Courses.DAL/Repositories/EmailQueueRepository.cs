using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models;
using Courses.Models.Repositories;
using System.Data.Entity;

namespace Courses.DAL
{
    class EmailQueueRepository:RepositoryBase<EmailQueue>,IEmailQueueRepository
    {

    }
}
