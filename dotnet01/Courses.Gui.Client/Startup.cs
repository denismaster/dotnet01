using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.MicrosoftAccount;
using Microsoft.Owin.Security.Twitter;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security.OAuth;
using Courses.Gui.Client.Providers;
using System.Security.Claims;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(Courses.Gui.Client.Startup))]

namespace Courses.Gui.Client
{
    public class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        static Startup()
        {
            PublicClientId = "web";

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                AuthorizeEndpointPath = new PathString("/Account/Authorize"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                CookieName = "CourseCookie",
                LoginPath = new PathString("/Account/Login"),
            });
            app.UseOAuthBearerTokens(OAuthOptions);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseExternalSignInCookie("ExternalCookie");

            // App.Secrets is application specific and holds values in CodePasteKeys.json
            // Values are NOT included in repro – auto-created on first load
            app.UseVkontakteAuthentication(
               appId: "5357653", 
               appSecret: "CqMDX2wGJDCBzeLkyFjS", 
               scope: "email"
            );

            app.UseGoogleAuthentication(
                clientId: "1234",
                clientSecret: "1234"
            );

            app.UseFacebookAuthentication(
                appId: "1234",
                appSecret: "1234"
            );

            app.UseMicrosoftAccountAuthentication(
                clientId: "1234",
                clientSecret: "1234"
            );

            app.UseTwitterAuthentication(
                consumerKey: "1234",
                consumerSecret: "1234"
            );

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}
