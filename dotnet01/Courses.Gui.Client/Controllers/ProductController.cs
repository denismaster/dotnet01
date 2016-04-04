using Courses.Buisness.Services;
using Courses.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace Courses.Gui.Client.Controllers
{
    //[Authorize(Roles = "Admin, Manager, Default")]
    public class ProductController : ApiController
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            if (productService == null)
                throw new ArgumentNullException();
            this.productService = productService;
        }

        // GET: api/Product
        public JsonResult<IEnumerable<ProductViewModelForWebApi>> GetProducts()
        {
            IEnumerable<ProductViewModelForWebApi> products = productService.GetProductsCollectionForWebAPI();
            return Json(products);
        }

        // GET: api/Product/5
        public JsonResult<ProductViewModelForWebApi> GetProduct(int id)
        {
            var productView = productService.GetByIdForWebApi(id);
            return Json(productView);
        }

    }
}


