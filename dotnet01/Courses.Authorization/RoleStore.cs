using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
namespace Courses.Authorization
{
    public class RoleStore:IRoleStore<IdentityRole>
    {
        
        public Task CreateAsync(IdentityRole role)
        {
            return Task.FromResult<bool>(true);
        }

        public Task DeleteAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRole> FindByIdAsync(string roleId)
        {
            return Task.FromResult<IdentityRole>(IdentityRole.Default);
        }

        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            return Task.FromResult<IdentityRole>(new IdentityRole(roleName));
        }

        public Task UpdateAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}
