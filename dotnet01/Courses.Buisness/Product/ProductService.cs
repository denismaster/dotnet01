using Courses.Buisness.Services;
using Courses.Models;
using Courses.Models.Repositories;
using Courses.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Courses.Buisness
{
    public class ProductService : IProductService
    {
        /// <summary>
        /// Репозиторий, используемый сервисом для получения продуктов
        /// </summary>
        private readonly IProductRepository productRepository;
        /// <summary>
        /// Репозиторий, используемый сервисом для получения аккаунтов
        /// </summary>
        private readonly IAccountRepository accountRepository;
        /// <summary>
        /// Репозиторий, используемый сервисом для получения партнеров
        /// </summary>
        private readonly IPartnerRepository partnerRepository;
        /// <summary>
        /// Репозиторий, используемый сервисом для получения категорий
        /// </summary>
        private readonly ICategoryRepository categoryRepository;
        /// <summary>
        /// Фабрика фильтров
        /// </summary>
        private readonly Filtering.IFilterFactory<Models.Product> filterFactory;
        /// <summary>
        /// Внедрение конструктора. Пример использования паттернов Dependecy Injection
        /// </summary>
        /// <param name="repository"></param>
        public ProductService(IProductRepository repository, IAccountRepository repositoryAccounts,
            IPartnerRepository repositoryPartners, ICategoryRepository categoryRepository, Filtering.IFilterFactory<Models.Product> filterFactory)
        {
            ///Guard Condition
            if (repository == null)
                throw new ArgumentNullException("Product repository is null!");
            if (repositoryPartners == null)
                throw new ArgumentNullException("Partner repository is null!");
            if (filterFactory == null)
                throw new ArgumentNullException("Filtering Factory is null!");
            this.productRepository = repository;
            this.accountRepository = repositoryAccounts;
            this.partnerRepository = repositoryPartners;
            this.categoryRepository = categoryRepository;
            this.filterFactory = filterFactory;
        }
        /// <summary>
        /// Получение курсов на заданной странице с заданными фильтрами.
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="fieldFilters">Список фильтров</param>
        /// <param name="sortFilter">Порядок сортировки</param>
        /// <returns></returns>
        public ProductCollectionViewModel GetProducts(int page, int pageSize, List<Filtering.FieldFilter> fieldFilters = null,
            Filtering.SortFilter sortFilter = null)
        {
            IEnumerable<ProductViewModel> products;
            int total;
            if (fieldFilters != null && sortFilter != null)
            {
                var newSortFilter = new SortFilter() { SortOrder = sortFilter.SortOrder };
                var expression = filterFactory.GetFilterExpression(fieldFilters);
                products = productRepository.Get(page, pageSize, expression, newSortFilter).Select(ConvertFromProductToProductViewModel);
                total = productRepository.Count(expression);
            }
            else
            {
                products = productRepository.Get(page, pageSize, x => true).Select(ConvertFromProductToProductViewModel);
                total = productRepository.Count(x => true);
            }
            var pageInfo = new PageInfo()
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = total
            };
            return new ProductCollectionViewModel() { Products = products, PageInfo = pageInfo };
        }
        /// <summary>
        /// Получение всех курсов без фильтров и сортировок
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductViewModelForWebApi> GetProductsCollectionForWebAPI()
        {
            return productRepository.Get().Select(ConvertFromProductToProductViewModel_WebApi);
        }

        /// <summary>
        /// получение курса со списком аккаунтов и партнеров, для передачи его в форму добавления/редактирования
        /// </summary>
        /// <param name="Id">Id продукта для редактирования</param>
        /// <returns></returns>
        public ProductForAddEditViewModel GetProductWithAccauntsAndPartners(int? Id)
        {
            ProductForAddEditViewModel productView = new ProductForAddEditViewModel();
            if (Id == null)
            {
                //для возможности не выбирать менеджера                                                              
                User noManager = new User { Id = 0, Login = "------------Отсутствует----------", Status = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };
                var listUser = accountRepository.Get().Where(m => m.Role.Equals("Manager")).ToList<User>();
                listUser.Add(noManager);
                productView.Accounts = new SelectList(listUser, "Id", "Login", 0);
                productView.Partners = new SelectList(partnerRepository.Get(), "PartnerId", "Name");
            }
            else
            {
                var product = productRepository.Get(Id.Value);
                if (product != null)
                {
                    productView = ConvertFromProductToProductViewModelForAddEditView(product);
                    User noManager = new User { Id = 0, Login = "------------Отсутствует----------", Status = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };
                    var listUser = accountRepository.Get().Where(m => m.Role.Equals("Manager")).ToList<User>();
                    listUser.Add(noManager);
                    productView.Accounts = new SelectList(listUser, "Id", "Login", 0);
                    productView.Partners = new SelectList(partnerRepository.Get(), "PartnerId", "Name", product.PartnerId);
                }
            }
            return productView;
        }

        /// <summary>
        /// Получение информации о курсе по его идентификатору
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ProductViewModel GetById(int Id)
        {
            var product = productRepository.Get(Id);
            return (product == null) ? null : ConvertFromProductToProductViewModel(product);
        }

        /// <summary>
        /// Получение информации о курсе по его идентификатору 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ProductViewModelForWebApi GetByIdForWebApi(int Id)
        {
            var product = productRepository.Get(Id);
            return (product == null) ? null : ConvertFromProductToProductViewModel_WebApi(product);
        }


        /// <summary>
        /// Добавление курса в репозиторий
        /// </summary>
        /// <param name="product"></param>
        public void Add(ProductViewModel product)
        {
            product.CreatedDate = product.UpdatedDate = DateTime.Now;
            if (product.AssignedUserId == 0)
                product.AssignedUserId = null;
            productRepository.Add(ConvertFromProductViewModelToProduct(product));
        }
        /// <summary>
        /// Обновление курса
        /// </summary>
        /// <param name="product"></param>
        public void Edit(ProductViewModel product)
        {
            product.UpdatedDate = DateTime.Now;
            if (product.AssignedUserId == 0)
                product.AssignedUserId = null;
            productRepository.Update(ConvertFromProductViewModelToProduct(product));
        }
        /// <summary>
        /// Удаление курса
        /// </summary>
        /// <param name="product"></param>
        public void Delete(ProductViewModel product)
        {
            productRepository.Delete(ConvertFromProductViewModelToProduct(product));
        }
        /// <summary>
        /// получение продукта со списком всех категорий
        /// </summary>
        /// <param name="Id">Id продукта для редактирования</param>
        /// <returns></returns>
        public ProductWithAllCategorysViewModel GetProductWithAllCategorys(int Id)
        {
            var product = productRepository.Get(Id);
            return (product == null) ? null : ConvertFromProductToProductWithAllCategorysViewModel(product);
        }
        /// <summary>
        /// Получает продукт со список категорий текущего продукта
        /// </summary>
        /// <param name="id"></param>
        public ProductWithCategorysViewModel GetProductWithCurrentCategorys(int id)
        {
            var product = productRepository.Get(id);
            return (product == null) ? null : ConvertFromProductToProductWithCategorysViewModel(product);
        }

        /// <summary>
        /// Редактирование списка категорий продукта
        /// </summary>
        /// <param name="product"></param>
        public void EditProductCategorys(ProductWithAllCategorysViewModel productView, int[] selectedCategorys)
        {
            Product product = productRepository.Get(productView.Id);
            product.UpdatedDate = DateTime.Now;
            product.Categories.Clear();
            SaveChanges();

            if (selectedCategorys != null)
            {
                foreach (int categoryId in selectedCategorys)
                {
                    categoryRepository.AddProducts(categoryId, product.Id);
                }
            }
        }



        /// <summary>
        /// Сохранение изменений
        /// </summary>
        public void SaveChanges()
        {
            productRepository.SaveChanges();
        }
        /// <summary>
        /// Конвертационные функции
        /// </summary>
        private Product ConvertFromProductViewModelToProduct(ProductViewModel c)
        {
            Product product = new Product()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                Active = c.Active,
                Type = c.Type,
                PartnerId = c.PartnerId,
                Teacher = c.Teacher,
                SeatsCount = c.SeatsCount,
                AssignedUserId = c.AssignedUserId,
                Location = c.Location,
                Image = (c.Image == null || c.Image.Length == 0) ? null : c.Image
            };
            return product;
        }
        private ProductViewModel ConvertFromProductToProductViewModel(Product c)
        {
            return new ProductViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                Active = c.Active,
                Type = c.Type,
                PartnerId = c.PartnerId,
                Teacher = c.Teacher,
                SeatsCount = c.SeatsCount ?? null,
                AssignedUserId = c.AssignedUserId ?? null,
                Location = c.Location,
                Image = (c.Image == null || c.Image.Length == 0) ? null : c.Image,
                ManagerName = (c.User != null) ? c.User.Login : "Отсутствует",
                PartnerName = (c.Partner != null) ? c.Partner.Name : "Отсутствует",
            };
        }

        private ProductViewModelForWebApi ConvertFromProductToProductViewModel_WebApi(Product product)
        {
            ProductViewModelForWebApi productView = new ProductViewModelForWebApi();

            productView.CategoriesNames = new List<String>();

            foreach (Category c in product.Categories)
            {
                productView.CategoriesNames.Add(c.Name);
                productView.CategoriesNamesString += c.Name + " ; ";
            }

            productView.Id = product.Id;
            productView.Name = product.Name;
            productView.Description = product.Description;
            productView.CreatedDate = product.CreatedDate;
            productView.UpdatedDate = product.UpdatedDate;
            productView.Active = product.Active;
            productView.Type = product.Type;
            productView.PartnerId = product.PartnerId;
            productView.Teacher = product.Teacher;
            productView.SeatsCount = product.SeatsCount ?? null;
            productView.AssignedUserId = product.AssignedUserId ?? null;
            productView.Location = product.Location;
            productView.Image = (product.Image == null || product.Image.Length == 0) ? null : product.Image;

            productView.PartnerName = (product.Partner != null) ? product.Partner.Name : "Отсутствует";
            productView.ManagerName = (product.User != null) ? product.User.Login : "Отсутствует";


            switch (productView.Type)
            {
                case 1: productView.TypeName = "Курс"; break;
                case 2: productView.TypeName = "Серия лекций"; break;
                case 3: productView.TypeName = "Мастер-класс"; break;
                case 4: productView.TypeName = "Подготовка к экзаменам"; break;
                case 5: productView.TypeName = "Практические занятия"; break;
            }

            return productView;
        }

        private ProductForAddEditViewModel ConvertFromProductToProductViewModelForAddEditView(Product product)
        {
            return new ProductForAddEditViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CreatedDate = product.CreatedDate,
                UpdatedDate = product.UpdatedDate,
                Active = product.Active,
                Type = product.Type,
                PartnerId = product.PartnerId,
                Teacher = product.Teacher,
                SeatsCount = product.SeatsCount ?? null,
                AssignedUserId = product.AssignedUserId ?? null,
                Location = product.Location,
                Image = (product.Image == null || product.Image.Length == 0) ? null : product.Image,
                ManagerName = (product.User != null) ? product.User.Login : "Отсутствует",
                PartnerName = (product.Partner != null) ? product.Partner.Name : "Отсутствует"
            };
        }
        private ProductWithCategorysViewModel ConvertFromProductToProductWithCategorysViewModel(Product product)
        {
            ProductWithCategorysViewModel productView = new ProductWithCategorysViewModel();
            productView.Categorys = new List<CategoryViewModel>();

            foreach (Category c in product.Categories)
            {
                productView.Categorys.Add(ConvertFromCategoryToCategoryViewModel(c));
            }

            productView.Id = product.Id;
            productView.Name = product.Name;
            productView.Description = product.Description;
            productView.CreatedDate = product.CreatedDate;
            productView.UpdatedDate = product.UpdatedDate;
            productView.Active = product.Active;
            productView.Type = product.Type;
            productView.PartnerId = product.PartnerId;
            productView.Teacher = product.Teacher;
            productView.SeatsCount = product.SeatsCount ?? null;
            productView.AssignedUserId = product.AssignedUserId ?? null;
            productView.Location = product.Location;
            productView.Image = (product.Image == null || product.Image.Length == 0) ? null : product.Image;
            productView.ManagerName = (product.User != null) ? product.User.Login : "Отсутствует";
            productView.PartnerName = (product.Partner != null) ? product.Partner.Name : "Отсутствует";
            productView.ManagerName = (product.User != null) ? product.User.Login : "Отсутствует";

            return productView;
        }
        private Models.Category ConvertFromCategoryViewModelToCategory(CategoryViewModel c)
        {
            return new Models.Category()
            {
                CategoryId = c.Id,
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                Active = c.Active,
                Description = c.Description,
                ParentCategoryId = c.ParentCategoryId
            };
        }
        private CategoryViewModel ConvertFromCategoryToCategoryViewModel(Models.Category c)
        {
            return new CategoryViewModel()
            {
                Id = c.CategoryId,
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                Active = c.Active,
                ParentCategoryId = (c.ParentCategory == null) ? null : c.ParentCategoryId,
                Description = c.Description
            };
        }
        private ProductWithAllCategorysViewModel ConvertFromProductToProductWithAllCategorysViewModel(Product c)
        {
            ProductWithCategorysViewModel productWithCategorys = ConvertFromProductToProductWithCategorysViewModel(c);
            ProductWithAllCategorysViewModel productWithAllCategorys = new ProductWithAllCategorysViewModel();

            productWithAllCategorys.Categorys = productWithCategorys.Categorys;
            productWithAllCategorys.Id = productWithCategorys.Id;
            productWithAllCategorys.Name = productWithCategorys.Name;
            productWithAllCategorys.Description = productWithCategorys.Description;
            productWithAllCategorys.CreatedDate = productWithCategorys.CreatedDate;
            productWithAllCategorys.UpdatedDate = productWithCategorys.UpdatedDate;
            productWithAllCategorys.Active = productWithCategorys.Active;
            productWithAllCategorys.Type = productWithCategorys.Type;
            productWithAllCategorys.PartnerId = productWithCategorys.PartnerId;
            productWithAllCategorys.Teacher = productWithCategorys.Teacher;
            productWithAllCategorys.SeatsCount = productWithCategorys.SeatsCount ?? null;
            productWithAllCategorys.AssignedUserId = productWithCategorys.AssignedUserId ?? null;
            productWithAllCategorys.Location = productWithCategorys.Location;
            productWithAllCategorys.Image = (productWithCategorys.Image == null || productWithCategorys.Image.Length == 0) ? null : productWithCategorys.Image;

            productWithAllCategorys.ManagerName = productWithCategorys.ManagerName;
            productWithAllCategorys.PartnerName = productWithCategorys.PartnerName;

            var categorysList = categoryRepository.Get().Select(ConvertFromCategoryToCategoryViewModel);
            productWithAllCategorys.AllCategorys = categorysList.ToList();

            return productWithAllCategorys;
        }
    }
}
