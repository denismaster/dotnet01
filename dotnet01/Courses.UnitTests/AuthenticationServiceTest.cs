using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Courses.Buisness.Services;
using Courses.Buisness.Authentication;
using Courses.DAL;
using Courses.Models;

namespace Courses.UnitTests
{
    [TestFixture]
    public class AuthenticationServiceTest
    {
        [Test]
        public void FindTest()
        {
            IAuthenticationService _authenticationService = new AuthenticationService(new AccountRepository(), new SHA256PasswordHasher());            
            bool expected = true, actual;
            if (_authenticationService.Find(1) != null &&
                _authenticationService.Find("testName") != null &&
                _authenticationService.Find("testName", "testPassword") != null)
            {
                actual = true;
            }
            else
            {
                actual = false;
            }
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindExternalTest()
        {
            IAuthenticationService _authenticationService = new AuthenticationService(new AccountRepository(), new SHA256PasswordHasher());
            Assert.IsNotNull(_authenticationService.FindExternal("testAuthKey"));
        }

        [Test]
        public void GetIdentityTest()
        {
            IAuthenticationService _authenticationService = new AuthenticationService(new AccountRepository(), new SHA256PasswordHasher());
            Assert.IsNotNull(_authenticationService.GetIdentity(new User()));
        }

        [Test]
        public void RegisterTest()
        {
            IAuthenticationService _authenticationService = new AuthenticationService(new AccountRepository(), new SHA256PasswordHasher());
            bool expected = true, actual;
            if(_authenticationService.Register("testUserName", "testPassword") &&
                _authenticationService.Register("testUserNameOther", "testAuthKey", "testProvider"))
            {
                actual = true;
            }
            else
            {
                actual = false;
            }
            Assert.AreEqual(expected, actual);
        }

        private User GetUser()
        {
            User _user = new User();
            _user.Email = "testc@tect.com";
            _user.FirstName = "testFirstName";
            _user.LastName = "testLastName";
            _user.Login = "testLogin";
            _user.Role = "Default";
            _user.CreatedDate = new DateTime(1, 1, 1);
            _user.UpdatedDate = new DateTime(1, 1, 1);
            return _user;
        }
    }
}
