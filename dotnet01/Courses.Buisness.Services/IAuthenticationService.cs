using System;
namespace Courses.Buisness.Services
{
    public interface IAuthenticationService
    {
        Courses.Models.User Find(int id);
        Courses.Models.User Find(string username);
        Courses.Models.User Find(string username, string password);
        Courses.Models.User FindExternal(string authKey);
        void LinkExternalLogin(Courses.Models.User user, string authkey, string providername);
        void UnlinkExternalLogin(Courses.Models.User user);
        System.Security.Claims.ClaimsIdentity GetIdentity(Courses.Models.User user);
        bool Register(string username, string password);
        bool Register(string username, string authkey, string provider);
        
    }
}
