using System;
using Ninject;
using Courses.Models;
using Courses.Models.Repositories;
using Courses.Buisness;
using Courses.DAL;
using Courses.Buisness.Filtering;
using Courses.Buisness.Services;
using Courses.Buisness.Authentication;

namespace Courses.Gui.Admin
{
    /// <summary>
    /// Фабрика, которая будет управлять нашим AdminController
    /// </summary>
    public class AdminControllerFactory : System.Web.Mvc.DefaultControllerFactory
    {
        /// <summary>
        /// Ядро Ninject
        /// </summary>
        IKernel kernel;

        public AdminControllerFactory()
        {
            kernel = new StandardKernel();
            AddBindings();
        }
        /// <summary>
        /// Получаем контроллер, делая resolve зависимостей с Ninject
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        protected override System.Web.Mvc.IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                  ? null
                  : (System.Web.Mvc.IController)kernel.Get(controllerType);
        }
        /// <summary>
        /// Настраиваем контейнер
        /// </summary>
        //TODO Использовать авторегистрацию в других фабриках, это на будущее
        private void AddBindings()
        {
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IFilterFactory<User>>().To<AccountFilterFactory>();
            kernel.Bind<IAccountRepository>().To<AccountRepository>();
            kernel.Bind<IAuthenticationService>().To<AuthenticationService>();
            kernel.Bind<IPasswordHasher>().To<SHA256PasswordHasher>();
        }
    }
}