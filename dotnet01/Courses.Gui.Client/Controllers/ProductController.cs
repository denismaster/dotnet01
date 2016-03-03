using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Courses.DAL;
using Courses.ViewModels;
using Courses.Buisness.Services;
using Courses.Buisness.Filtering;

namespace Courses.Gui.Client.Controllers
{
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
        public ProductCollectionViewModel GetProducts()
        {
            int? page = 1;
            int pageSize = 3;
            int currentPage = page ?? 1;
            var products = productService.GetProducts(currentPage, pageSize);
            return products;
        }

        // GET: api/Product/5
        public ProductViewModel GetProduct(int id)
        {
            var productView = productService.GetById(id);
            return productView;
        }

    }
}


