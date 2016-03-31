using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Linq;
using System.Security.Claims;
using Courses.Gui.Client.Models;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Courses.Gui.Client.Controllers
{
    //[Authorize(Roles = "Admin, Manager, Default")]
    public class AccountController : Controller
    {
        private readonly Buisness.Services.IAuthenticationService _authService;

        public AccountController(Buisness.Services.IAuthenticationService service)
        {
            if(service==null)
            {
                throw new ArgumentNullException("Authentication Service");
            }
            _authService = service;
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //User user = await db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                var user = _authService.Find(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    var claims = _authService.GetIdentity(user);

                    AuthenticationManager.SignOut();

                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claims);
                    return RedirectToAction("Spa", "Home");
                }
            }
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Authorize()
        {
            var claims = new System.Security.Claims.ClaimsPrincipal(User).Claims;
            var identity = new System.Security.Claims.ClaimsIdentity(claims, "Bearer");
            AuthenticationManager.SignIn(identity);
            return new EmptyResult();
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _authService.Register(model.Email, model.Password);
                //var result = await _userManager.CreateAsync(user, model.Password);
                if (result)
                {
                    var user = _authService.Find(model.Email);
                    var claims = _authService.GetIdentity(user);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claims);
                    return RedirectToAction("Spa", "Home");
                }
                else
                {
                    //AddErrors();
                    ModelState.AddModelError("", "Пользователь уже зарегистрирован");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            throw new NotImplementedException();
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }
        public ActionResult AuthorizeVk()
        {
            return ExternalLogin("Vkontakte", Url.Action("Spa","Home"));
        }

        public ActionResult AuthorizeFacebook()
        {
            return ExternalLogin("Facebook", Url.Action("Index", "Home"));
        }

        public ActionResult AuthorizeTwitter()
        {
            return ExternalLogin("Twitter", Url.Action("Index", "Home"));
        }

        public ActionResult AuthorizeGoogle()
        {
            return ExternalLogin("Google", Url.Action("Index", "Home"));
        }

        public ActionResult AuthorizeMicrosoft()
        {
            return ExternalLogin("Microsoft", Url.Action("Index", "Home"));
        }
        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(Models.ChangePasswordViewModel model)
        {
            throw new NotImplementedException();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginData = await AuthenticationManager.AuthenticateAsync("ExternalCookie");          
            var loginClaims = loginData.Identity.Claims;
            var userName = loginClaims.First(c => c.Type == ClaimTypes.Name).Value;
            var providerName = loginClaims.First().Issuer;
            var authKey = loginClaims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            var user = _authService.FindExternal(authKey);

            if (user == null)
            {
                return RedirectToAction("Login", new
                {
                    message = "Unable to log in with "+providerName+". "
                });
            }

            // store on AppUser
            AuthenticationManager.SignOut();
            var claims = _authService.GetIdentity(user);
            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claims);

            return Redirect(returnUrl);
        }

        //
        // POST: /Account/ExternalLinkLogin
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLinkLogin(string provider)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLinkLoginCallback"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/ExternalLinkLoginCallBack
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> ExternalLinkLoginCallback()
        {
            var loginData = await AuthenticationManager.AuthenticateAsync("ExternalCookie");
            var loginClaims= loginData.Identity.Claims;
            var userName = loginClaims.First(c => c.Type == ClaimTypes.Name).Value;
            var providerName = loginClaims.First().Issuer;
            var authKey = loginClaims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            Courses.Models.User user=null;
            if(!String.IsNullOrEmpty(userName))
                user = _authService.Find(userName);
            if (user == null)
            {
                _authService.Register(userName, authKey, providerName);
            }
            else
            {
                _authService.LinkExternalLogin(user, authKey, providerName);
            }
            AuthenticationManager.SignOut();
            var claims = _authService.GetIdentity(user);
            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claims);
            return RedirectToAction("Register");
        }

        //
        // GET: /Account/ExternalUnlinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalUnlinkLogin()
        {
            throw new NotImplementedException();
            //var userId = AppUserState.UserId;
            //var user = busUser.Load(userId);
            //if (user == null)
            //{
            //    ErrorDisplay.ShowError("Couldn't find associated User: " + busUser.ErrorMessage);
            //    return RedirectToAction("Register", new { id = userId });
            //}
            //user.OpenId = string.Empty;
            //user.OpenIdClaim = string.Empty;

            //if (busUser.Save())
            //    return RedirectToAction("Register", new { id = userId });

            //return RedirectToAction("Register", new { message = "Unable to unlink OpenId. " + busUser.ErrorMessage });
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            throw new NotImplementedException();
        }
       
        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            throw new NotImplementedException();
        }

        //
        // POST: /Account/LogOff
        //[HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Spa", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Helpers
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        private string getint(string value)
        {
            var result = default(int);
            int.TryParse(value, out result);
            return result.ToString();
        }
        #endregion                       
    }
}