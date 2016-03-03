using System;
using Ninject;
using Courses.Models;
using Courses.Models.Repositories;
using Courses.Buisness;
using Courses.DAL;
using Courses.Buisness.Filtering;
using Courses.Buisness.Services;

namespace Courses.Gui.Manager
{
    /// <summary>
    /// Фабрика, которая будет управлять нашим ManagerController
    /// </summary>
    public class ManagerControllerFactory : System.Web.Mvc.DefaultControllerFactory
    {
        /// <summary>
        /// Ядро Ninject
        /// </summary>
        IKernel kernel;

        public ManagerControllerFactory()
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
            kernel.Bind<IFilterFactory<Product>>().To<ProductFilterFactory>();
            kernel.Bind<IProductRepository>().To<ProductRepository>();

            kernel.Bind<IPartnerRepository>().To<PartnerRepository>();
            kernel.Bind<ICategoryService>().To<PatherService>();

            kernel.Bind<IAccountRepository>().To<AccountRepository>();

            kernel.Bind<IFilterFactory<Partner>>().To<PartnerFilterFactory>();
        }
    }
}