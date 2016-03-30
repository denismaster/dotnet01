using Courses.Buisness.Services;
using Courses.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace Courses.Gui.Client.Controllers
{
    //[Authorize(Roles = "Admin, Manager, Default")]
    public class CategoryController : ApiController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            if (_categoryService == null)
                throw new ArgumentNullException();
            this.categoryService = _categoryService;
        }
        // GET: api/Category
        public JsonResult<IEnumerable<CategoryViewModel>> GetCategorys()
        {
            var categorys = categoryService.GetIEnumerableCategorysCollection();
            return Json(categorys);
        }

        // GET: api/Category/5
        public JsonResult<CategoryViewModel> GetCategory(int id)
        {
            var category = categoryService.GetByID(id);
            return Json(category);
        }
    }
}