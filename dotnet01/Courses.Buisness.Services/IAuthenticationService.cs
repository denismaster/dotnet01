using System;
namespace Courses.Buisness.Services
{
    public interface IAuthenticationService
    {
        Courses.Models.User Find(int id);
        Courses.Models.User Find(string username);
        Courses.Models.User Find(string username, string password);
        System.Security.Claims.ClaimsIdentity GetIdentity(Courses.Models.User user);
        bool Register(string username, string password);
    }
}
