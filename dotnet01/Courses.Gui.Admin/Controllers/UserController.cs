using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Courses.Buisness.Authentication;

namespace Courses.Gui.Admin.Controllers
{
    public class AuthorizationModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
    public class PartialUserInfo
    {
        public string Name { get; set; }
    }
    public class UserController : Controller
    {
        public readonly AuthenticationService authenticationService;

        public UserController(AuthenticationService authenticationService)
        {
            if (authenticationService == null)
                throw new ArgumentNullException();
            this.authenticationService = authenticationService;
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            ViewData["IsAuthorized"] = Request.IsAuthenticated;
            base.OnResultExecuting(filterContext);
        }
        //
        // GET: /Account/
        [Authorize]  // Этот атрибут позволяет просматривать страничку /Account только авторизованным пользователям
        // если при обращении к ней вы не авторизованы - вас редиректнет на /Account/LogOn
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly] // этот экшн сгенерит форму логина или покажет приветствие пользователя для уже авторизованных лиц
        // использовать как @Html.RenderAction<AccountController>(x=>x.LogOnPartial());
        public ActionResult LogOnPartial(AuthorizationModel model = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return PartialView("PartialUserInfo", new PartialUserInfo { Name = User.Identity.Name });
            }

            return PartialView(model ?? new AuthorizationModel());
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut(); // разлогиниваем текущего пользователя
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(AuthorizationModel model)
        {
            //if (authenticationService.IsValid(model.Login, model.Password)) // валидируем пользователя
            {
                
                FormsAuthentication.SetAuthCookie(model.Login, true); // выставляем куки для авторизованных лиц
                if (!String.IsNullOrEmpty(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Неверное имя пользователя или пароль");

            return View(model);
        }

        public ActionResult LogOn()
        {
            return View(new AuthorizationModel { ReturnUrl = "" }); //показываем форму авторизации
        }
    }
}