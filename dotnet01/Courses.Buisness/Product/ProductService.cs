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
        private readonly IProductRepository repository;
        /// <summary>
        /// Репозиторий, используемый сервисом для получения аккаунтов
        /// </summary>
        private readonly IAccountRepository repositoryAccounts;
        /// <summary>
        /// Репозиторий, используемый сервисом для получения партнеров
        /// </summary>
        private readonly IPartnerRepository repositoryPartners;
        /// <summary>
        /// Фабрика фильтров
        /// </summary>
        private readonly Filtering.IFilterFactory<Models.Product> filterFactory;
        /// <summary>
        /// Внедрение конструктора. Пример использования паттернов Dependecy Injection
        /// </summary>
        /// <param name="repository"></param>
        public ProductService(Models.Repositories.IProductRepository repository, Models.Repositories.IAccountRepository repositoryAccounts, 
            Models.Repositories.IPartnerRepository repositoryPartners, Filtering.IFilterFactory<Models.Product> filterFactory)
        {
            ///Guard Condition
            if (repository == null)
                throw new ArgumentNullException("Product repository is null!");
            if (repositoryPartners == null)
                throw new ArgumentNullException("Partner repository is null!");
            if (filterFactory == null)
                throw new ArgumentNullException("Filtering Factory is null!");
            this.repository = repository;
            this.repositoryAccounts = repositoryAccounts;
            this.repositoryPartners = repositoryPartners;
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
                products = repository.Get(page, pageSize, expression, newSortFilter).Select(ConvertToProductViewModel);
                total = repository.Count(expression);
            }
            else
            {
                products = repository.Get(page, pageSize, x => true).Select(ConvertToProductViewModel);
                total = repository.Count(x => true);
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
            products = repository.Get().Select(ConvertToProductViewModel);
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
                var listUser = repositoryAccounts.Get().ToList<User>();
                listUser.Add(noManager);
                productView.Accounts = new SelectList(listUser, "Id", "Login", 0);
                productView.Partners = new SelectList(repositoryPartners.Get(), "PartnerId", "Name");
            }
            else
            {   
                var product = repository.Get(Id.Value);
                if (product != null)
                {
                    productView = ConvertToProductViewModelForAddEditView(product);
                    if(product.User == null)
                    {
                        User noManager = new User { Id = 0, Login = "------------Отсутствует----------", Status = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };
                        var listUser = repositoryAccounts.Get().ToList<User>();
                        listUser.Add(noManager);
                        productView.Accounts = new SelectList(listUser, "Id", "Login", 0);
                    }
                    else
                        productView.Accounts = new SelectList(repositoryAccounts.Get(), "Id", "Login", product.AssignedUserId);
                    productView.Partners = new SelectList(repositoryPartners.Get(), "PartnerId", "Name", product.PartnerId);
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
            var product = repository.Get(Id);
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
            repository.Add(Convert(product));
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
            repository.Update(Convert(product));
        }
        /// <summary>
        /// Удаление курса
        /// </summary>
        /// <param name="product"></param>
        public void Delete(ProductViewModel product)
        {
            repository.Delete(Convert(product));
        }
        /// <summary>
        /// Сохранение изменений
        /// </summary>
        public void SaveChanges()
        {
            repository.SaveChanges();
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
                Location = c.Location
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
                Location = c.Location
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
                Location = c.Location
            };
        }
    }
}
