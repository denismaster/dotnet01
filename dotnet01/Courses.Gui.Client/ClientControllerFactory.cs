using Courses.Buisness;
using Courses.Buisness.Filtering;
using Courses.Buisness.Services;
using Courses.DAL;
using Courses.Models;
using Courses.Models.Repositories;
using Ninject;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
namespace Courses.Gui.Client
{
    /// <summary>
    /// Фабрика, которая будет управлять нашим ManagerController
    /// </summary>
    public class ClientControllerFactory : System.Web.Mvc.DefaultControllerFactory, IHttpControllerActivator
    {
        /// <summary>
        /// Ядро Ninject
        /// </summary>
        IKernel kernel;

        public ClientControllerFactory()
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
            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<IPartnerService>().To<PatherService>();
            kernel.Bind<ICategoryService>().To<CategoryService>();

            kernel.Bind<IProductRepository>().To<ProductRepository>();
            kernel.Bind<IPartnerRepository>().To<PartnerRepository>();
            kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            kernel.Bind<IAccountRepository>().To<AccountRepository>();

            kernel.Bind<IFilterFactory<Product>>().To<ProductFilterFactory>();
            kernel.Bind<IFilterFactory<Partner>>().To<PartnerFilterFactory>();
            kernel.Bind<IFilterFactory<Category>>().To<CategoryFilterFactory>();

            kernel.Bind<IPasswordHasher>().To<Buisness.Authentication.SHA256PasswordHasher>();
            kernel.Bind<IAuthenticationService>().To<Buisness.Authentication.AuthenticationService>();
        }

        public System.Web.Http.Controllers.IHttpController Create(HttpRequestMessage request, System.Web.Http.Controllers.HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            return (IHttpController)kernel.Get(controllerType);
        }
    }
}