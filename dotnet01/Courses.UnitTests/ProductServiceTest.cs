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
    public class ProductServiceTest
    {
        [Test]
        public void GetProductsTest()
        {
            IProductService _productService = new ProductService(new ProductRepository(), new AccountRepository(), 
                                                                 new PartnerRepository(), new ProductFilterFactory());
            ProductCollectionViewModel actual = _productService.GetProducts(1, 5);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void GetIEnumerableProductsCollectionTest()
        {
            IProductService _productService = new ProductService(new ProductRepository(), new AccountRepository(),
                                                                 new PartnerRepository(), new ProductFilterFactory());
            IEnumerable<ProductViewModel> actual = _productService.GetProductsCollectionForWebAPI();
            Assert.IsNotNull(actual);
        }

        [Test]
        public void GetProductWithAccauntsAndPartnersTest()
        {
            IProductService _productService = new ProductService(new ProductRepository(), new AccountRepository(),
                                                                 new PartnerRepository(), new ProductFilterFactory());
            ProductViewModelForAddEditView actual = _productService.GetProductWithAccauntsAndPartners(null);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void GetByIdTest()
        {
            IProductService _productService = new ProductService(new ProductRepository(), new AccountRepository(),
                                                                 new PartnerRepository(), new ProductFilterFactory());
            ProductViewModel _productViewModel = GetProductViewModel();
            _productService.Add(_productViewModel);
            Assert.IsNotNull(_productService.GetById(1));
        }

        [Test]
        public void AddTest()
        {
            IProductService _productService = new ProductService(new ProductRepository(), new AccountRepository(),
                                                                 new PartnerRepository(), new ProductFilterFactory());
            ProductViewModel _productViewModel = GetProductViewModel();
            _productService.Add(_productViewModel);
            Assert.IsNotNull(_productService.GetById(1));
        }

        [Test]
        public void EditTest()
        {
            IProductService _productService = new ProductService(new ProductRepository(), new AccountRepository(),
                                                                 new PartnerRepository(), new ProductFilterFactory());
            ProductViewModel _productViewModel = GetProductViewModel();
            _productService.Add(_productViewModel);
            _productViewModel.Description = "otherDescription";
            _productService.Edit(_productViewModel);
            bool actual, expected = true;
            if(_productService.GetById(1).Description.CompareTo("otherDisctiption") == 0)
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
            IProductService _productService = new ProductService(new ProductRepository(), new AccountRepository(),
                                                                 new PartnerRepository(), new ProductFilterFactory());
            ProductViewModel _productViewModel = GetProductViewModel();
            _productService.Add(_productViewModel);
            _productService.Delete(_productViewModel);
            Assert.IsNull(_productService.GetById(1));
        }

        private ProductViewModel GetProductViewModel()
        {
            ProductViewModel _productViewModel = new ProductViewModel();
            _productViewModel.Active = true;
            _productViewModel.Description = "testDescription";
            _productViewModel.Id = 1;
            _productViewModel.imagePath = "testImagePath";
            _productViewModel.Location = "testLocation";
            _productViewModel.Name = "testName";
            _productViewModel.PartnerId = 1;
            _productViewModel.Teacher = "testTeacher";
            _productViewModel.Type = 1;
            _productViewModel.CreatedDate = new DateTime(1, 1, 1);
            _productViewModel.UpdatedDate = new DateTime(1, 1, 1);
            return _productViewModel;
        }
    }
}
