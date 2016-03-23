using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models;
using Courses.Models.Repositories;
using Courses.ViewModels;
using Courses.Buisness.Services;
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
                products = productRepository.Get(page, pageSize, expression, newSortFilter).Select(ConvertToProductViewModel);
                total = productRepository.Count(expression);
            }
            else
            {
                products = productRepository.Get(page, pageSize, x => true).Select(ConvertToProductViewModel);
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
        public IEnumerable<ProductViewModel> GetIEnumerableProductsCollection()
        {
            IEnumerable<ProductViewModel> products;
            products = productRepository.Get().Select(ConvertToProductViewModel);
            return products;
        }

        /// <summary>
        /// получение курса со списком аккаунтов и партнеров, для передачи его в форму добавления/редактирования
        /// </summary>
        /// <param name="Id">Id продукта для редактирования</param>
        /// <returns></returns>
        public ProductViewModelForAddEditView GetProductWithAccauntsAndPartners(int? Id)
        {
            ProductViewModelForAddEditView productView = new ProductViewModelForAddEditView();
            if (Id == null)
            {
                //для возможности не выбирать менеджера                                                              
                User noManager = new User { Id = 0, Login = "------------Отсутствует----------", Status = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };
                var listUser = accountRepository.Get().ToList<User>();
                listUser.Add(noManager);
                productView.Accounts = new SelectList(listUser, "Id", "Login", 0);
                productView.Partners = new SelectList(partnerRepository.Get(), "PartnerId", "Name");
            }
            else
            {   
                var product = productRepository.Get(Id.Value);
                if (product != null)
                {
                    productView = ConvertToProductViewModelForAddEditView(product);
                    User noManager = new User { Id = 0, Login = "------------Отсутствует----------", Status = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };
                    var listUser = accountRepository.Get().ToList<User>();
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
            return (product == null) ? null : ConvertToProductViewModel(product);
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
            productRepository.Add(Convert(product));
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
            productRepository.Update(Convert(product));
        }
        /// <summary>
        /// Удаление курса
        /// </summary>
        /// <param name="product"></param>
        public void Delete(ProductViewModel product)
        {
            productRepository.Delete(Convert(product));
        }
        /// <summary>
        /// получение продукта со списком всех категорий
        /// </summary>
        /// <param name="Id">Id продукта для редактирования</param>
        /// <returns></returns>
        public ProductCategoryViewModel GetProductCategory(int? Id)
        {
            ProductCategoryViewModel productView = new ProductCategoryViewModel();
            if (Id == null)
            {
                productView.Id = 0;
            }
            else
            {
                Product product = productRepository.Get(Id.Value);
                Category category = categoryRepository.Get(2);
                productView = ConvertFromProductToProductCategoryViewModel(product);
                var categorysList = categoryRepository.Get().Select(ConvertToCategoryViewModelFromCategory);
                productView.AllCategorys = categorysList.ToList();
            }
            return productView;
        }

        /// <summary>
        /// Редактирование списка категорий продукта
        /// </summary>
        /// <param name="product"></param>
        public void EditProductCategorys(ProductCategoryViewModel productView, int[] selectedCategorys)
        {
            Product product = productRepository.Get(productView.Id);
            product.Categories.Clear();

            if(selectedCategorys != null)
            {
                foreach(var c in productView.AllCategorys.Where(co => selectedCategorys.Contains(co.Id)))
                {
                    product.Categories.Add(ConvertFromCategoryViewModelToCategory(c));
                }
            }
            product.UpdatedDate = DateTime.Now;
            productRepository.Update(product);
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
        private Product Convert(ProductViewModel c)
        {
            return new Product()
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
                imagePath = c.imagePath
            };
        }
        private ProductViewModel ConvertToProductViewModel(Product c)
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
                imagePath = c.imagePath
            };
        }

        private ProductViewModelForAddEditView ConvertToProductViewModelForAddEditView(Product c)
        {
            return new ProductViewModelForAddEditView()
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
                imagePath = c.imagePath
            };
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
        private CategoryViewModel ConvertToCategoryViewModelFromCategory(Models.Category c)
        {
            return new CategoryViewModel()
            {
                Id = c.CategoryId,
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                Active = c.Active,
                ParentCategoryId = (c.ParentCategory == null) ? null : (int?)(c.ParentCategory.CategoryId),
                Description = c.Description
            };
        }
        private ProductCategoryViewModel ConvertFromProductToProductCategoryViewModel(Product c)
        {
            ProductCategoryViewModel productView = new ProductCategoryViewModel();
            productView.Id = c.Id;
            productView.Name = c.Name;
            List<CategoryViewModel> categoryList = new List<CategoryViewModel>();
            if (c.Categories != null)
            {
                foreach (Category category in c.Categories)
                {
                    categoryList.Add(ConvertToCategoryViewModelFromCategory(category));
                }
            }
            categoryList.Add(new CategoryViewModel { Id = 0, Active = false, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            productView.SelectedCategorys = categoryList;
            return productView;
        }
    }
}
