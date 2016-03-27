using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Courses.Buisness;
using Courses.DAL;
using Courses.Buisness.Filtering;
using Courses.ViewModels;
using Courses.Buisness.Services;

namespace Courses.UnitTests
{
    [TestFixture]
    public class CategoryServiceTest
    {
        [Test]
        public void GetCategorysTest()
        {
            ICategoryService _categoryService = new CategoryService(new CategoryRepository(), new CategoryFilterFactory());
            CategoryCollectionViewModel actual = _categoryService.GetCategorys(1, 5);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void GetCategoryWithCategorysTest()
        {
            ICategoryService _categoryService = new CategoryService(new CategoryRepository(), new CategoryFilterFactory());
            CategoryViewModelForAddEditView actual = _categoryService.GetCategoryWithCategorys(null);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void GetByIDTest()
        {
            ICategoryService _categoryService = new CategoryService(new CategoryRepository(), new CategoryFilterFactory());
            CategoryViewModel _categoryViewModel = GetCategoryViewModel();
            _categoryService.Add(_categoryViewModel);
            Assert.IsNotNull(_categoryService.GetByID(1));
        }

        [Test]
        public void AddTest()
        {
            ICategoryService _categoryService = new CategoryService(new CategoryRepository(), new CategoryFilterFactory());
            CategoryViewModel _categoryViewModel = GetCategoryViewModel();
            _categoryService.Add(_categoryViewModel);
            Assert.IsNotNull(_categoryService.GetByID(1));
        }
        [Test]
        public void EditTest()
        {
            ICategoryService _categoryService = new CategoryService(new CategoryRepository(), new CategoryFilterFactory());
            CategoryViewModel _categoryViewModel = GetCategoryViewModel();
            _categoryService.Add(_categoryViewModel);
            _categoryViewModel.Description = "otherDescription";
            _categoryService.Edit(_categoryViewModel);
            bool actual, expected = true;
            if(_categoryService.GetByID(1).Description.CompareTo("otherDescription") == 0)
            {
                actual = true;
            }
            else
            {
                actual = false;
            }
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeleteTest()
        {
            ICategoryService _categoryService = new CategoryService(new CategoryRepository(), new CategoryFilterFactory());
            CategoryViewModel _categoryViewModel = GetCategoryViewModel();
            _categoryService.Add(_categoryViewModel);
            _categoryService.Delete(_categoryViewModel);
            Assert.IsNull(_categoryService.GetByID(1));
        }

        private CategoryViewModel GetCategoryViewModel()
        {
            CategoryViewModel _categoryViewModel = new CategoryViewModel();
            _categoryViewModel.Description = "testDecrtioption";
            _categoryViewModel.Name = "testName";
            _categoryViewModel.Active = true;
            _categoryViewModel.CreatedDate = new DateTime(1, 1, 1);
            _categoryViewModel.UpdatedDate = new DateTime(1, 1, 1);
            _categoryViewModel.Id = 1;
            return _categoryViewModel;
        }
    }
}
