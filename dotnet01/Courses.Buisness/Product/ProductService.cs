using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models;
using Courses.Models.Repositories;
using Courses.ViewModels;

namespace Courses.Buisness
{
    public interface IProductService
    {
        /// <summary>
        /// Возвращает список курсов. 
        /// TODO:Желательно возвращать готовые ViewModels, но это пока неважно.
        /// </summary>
        ProductCollectionViewModel GetProducts(int page, int pageSize,
            List<Filtering.FieldFilter> fieldFilter = null, SortFilter sortFilter = null);
        /// <summary>
        /// Получение одного курса
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProductViewModel GetByID(int id);
        /// <summary>
        /// Добавление курса. 
        /// </summary>
        /// <param name="product"></param>
        void Add(ProductViewModel product);
        /// <summary>
        /// Обновление данных курса
        /// </summary>
        /// <param name="product"></param>
        void Edit(ProductViewModel product);
        /// <summary>
        /// Удаление курса
        /// </summary>
        /// <param name="product"></param>
        void Delete(ProductViewModel product);
        /// <summary>
        /// Сохранение изменений в репозитории
        /// </summary>
        void SaveChanges();
    }
    public class ProductService : IProductService
    {
        /// <summary>
        /// Репозиторий, используемый сервисом
        /// </summary>
        private readonly IProductRepository repository;
        /// <summary>
        /// Фабрика фильтров
        /// </summary>
        private readonly Filtering.IFilterFactory<Models.Product> filterFactory;
        /// <summary>
        /// Внедрение конструктора. Пример использования паттернов Dependecy Injection
        /// </summary>
        /// <param name="repository"></param>
        public ProductService(Models.Repositories.IProductRepository repository, Filtering.IFilterFactory<Models.Product> filterFactory)
        {
            ///Guard Condition
            if (repository == null)
                throw new ArgumentNullException("Repository is null!");
            if (filterFactory == null)
                throw new ArgumentNullException("Filtering Factory is null!");
            this.repository = repository;
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
            SortFilter sortFilter = null)
        {
            IEnumerable<ProductViewModel> products;
            int total;
            if (fieldFilters != null && sortFilter != null)
            {
                var expression = filterFactory.GetFilterExpression(fieldFilters);
                products = repository.Get(page, pageSize, expression, sortFilter).Select(Convert);
                total = repository.Count(expression);
            }
            else
            {
                products = repository.Get(page, pageSize, x => true).Select(Convert);
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
        /// Получение информации о курсе по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductViewModel GetByID(int id)
        {
            var product = repository.Get(id);
            return (product == null) ? null : Convert(product);
        }
        /// <summary>
        /// Добавление курса в репозиторий
        /// </summary>
        /// <param name="product"></param>
        public void Add(ProductViewModel product)
        {
            repository.Add(Convert(product));
        }
        /// <summary>
        /// Обновление курса
        /// </summary>
        /// <param name="product"></param>
        public void Edit(ProductViewModel product)
        {
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
                ID = c.ID,
                Name = c.Name,
                Description = c.Description,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                Active = c.Active,
                Type = c.Type,
                PartnerID = c.PartnerID,
                Teacher = c.Teacher,
                SeatsCount = c.SeatsCount,
                AssignedUserID = c.AssignedUserID,
                Location = c.Location
            };
        }
        private ProductViewModel Convert(Product c)
        {
            return new ProductViewModel()
            {
                ID = c.ID,
                Name = c.Name,
                Description = c.Description,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                Active = c.Active,
                Type = c.Type,
                PartnerID = c.PartnerID,
                Teacher = c.Teacher,
                SeatsCount = c.SeatsCount,
                AssignedUserID = c.AssignedUserID,
                Location = c.Location
            };
        }
    }
}
