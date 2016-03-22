using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models.Repositories;
using System.Data.Entity;
using Courses.Models;

namespace Courses.DAL
{
    public class EmailTemplateRepository : RepositoryBase<EmailTemplate>, IEmailTemplateRepoitory
    {

    }
}
