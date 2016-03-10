using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Courses.Gui.Client.Models;
//using Courses.Gui.Client.Models.Identity;

namespace Courses.Gui.Client
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
    public class UserModel:IdentityUser
    {

    }
    // Configure the application user manager which is used in this application.
    public class ApplicationUserManager : UserManager<UserModel>
    {
        public ApplicationUserManager(IUserStore<UserModel> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(
            IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            throw new NotImplementedException();
            //var manager = new ApplicationUserManager(
            //    new CustomUserStore(context.Get<ApplicationDbContext>()));
            //var manager = new ApplicationUserManager(new UserStore(context.Get<DAL.AccountRepository>()));
            // Configure validation logic for usernames 
            //manager.UserValidator = new UserValidator<UserModel, int>(manager)
            //{
            //    AllowOnlyAlphanumericUserNames = false,
            //    RequireUniqueEmail = true
            //};
            // Configure validation logic for passwords 
            //manager.PasswordValidator = new PasswordValidator
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = false,
            //    RequireDigit = false,
            //    RequireLowercase = false,
            //    RequireUppercase = false,
            //};
            // Register two factor authentication providers. This application uses Phone 
            // and Emails as a step of receiving a code for verifying the user 
            // You can write your own provider and plug in here. 
            //manager.RegisterTwoFactorProvider("PhoneCode",
            //    new PhoneNumberTokenProvider<UserModel>
            //    {
            //        MessageFormat = "Your security code is: {0}"
            //    });
            //manager.RegisterTwoFactorProvider("EmailCode",
            //    new EmailTokenProvider<UserModel>
            //    {
            //        Subject = "Security Code",
            //        BodyFormat = "Your security code is: {0}"
            //    });
            //manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();
            //var dataProtectionProvider = options.DataProtectionProvider;
            //if (dataProtectionProvider != null)
            //{
            //    manager.UserTokenProvider =
            //        new DataProtectorTokenProvider<UserModel>(
            //            dataProtectionProvider.Create("ASP.NET Identity"));
            //}
            //return manager;
        }
    } 

    // Configure the application sign-in manager which is used in this application.  
    public class ApplicationSignInManager : SignInManager<UserModel,string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager) { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(UserModel user)
        {
            throw new NotImplementedException();
           // return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
