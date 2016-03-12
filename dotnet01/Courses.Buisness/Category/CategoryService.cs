using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models;
using Courses.Models.Repositories;
using Courses.ViewModels;
using Courses.Buisness.Services;

namespace Courses.Buisness
{
    public class CategoryService : ICategoryService
    {
        /// <summary>
        /// Репозиторий, используемый сервисом
        /// </summary>
        private readonly ICategoryRepository repository;
        /// <summary>
        /// Фабрика фильтров
        /// </summary>
        private readonly Filtering.IFilterFactory<Models.Category> filterFactory;
        /// <summary>
        /// Внедрение конструктора. Пример использования паттернов Dependecy Injection
        /// </summary>
        /// <param name="repository"></param>
        public CategoryService(Models.Repositories.ICategoryRepository repository, Filtering.IFilterFactory<Models.Category> filterFactory)
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
        /// Получение категорий на заданной странице с заданными фильтрами.
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="fieldFilters">Список фильтров</param>
        /// <param name="sortFilter">Порядок сортировки</param>
        /// <returns></returns>
        public CategoryCollectionViewModel GetCategorys(int page, int pageSize,
            List<Filtering.FieldFilter> fieldFilters = null,
             Filtering.SortFilter sortFilter = null)
        {
            IEnumerable<CategoryViewModel> categorys;
            int total;
            if (fieldFilters != null && sortFilter != null)
            {
                var newSortFilter = new SortFilter() { SortOrder = sortFilter.SortOrder };
                var expression = filterFactory.GetFilterExpression(fieldFilters);
                categorys = repository.Get(page, pageSize, expression, newSortFilter).Select(Convert);
                total = repository.Count(expression);
            }
            else
            {
                categorys = repository.Get(page, pageSize, x => true).Select(Convert);
                total = repository.Count(x => true);
            }
            var pageInfo = new PageInfo()
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = total
            };
            return new CategoryCollectionViewModel() { Categorys = categorys, PageInfo = pageInfo };
        }


        /// <summary>
        /// Получение информации о категории по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CategoryViewModel GetByID(int id)
        {
            var category = repository.Get(id);
            return (category == null) ? null : Convert(category);
        }
        /// <summary>
        /// Добавление категории в репозиторий
        /// </summary>
        /// <param name="category"></param>
        public void Add(CategoryViewModel category)
        {
            repository.Add(Convert(category));
        }
        /// <summary>
        /// Обновление категории
        /// </summary>
        /// <param name="category"></param>
        public void Edit(CategoryViewModel category)
        {
            repository.Update(Convert(category));
        }
        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <param name="category"></param>
        public void Delete(CategoryViewModel category)
        {
            repository.Delete(Convert(category));
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
        private Models.Category Convert(CategoryViewModel c)
        {
            return new Models.Category()
            {
                CategoryId = c.Id,
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                Active = c.Active,
                ParentId = c.ParentId
            };
        }
        private CategoryViewModel Convert(Models.Category c)
        {
            return new CategoryViewModel()
            {
                Id = c.CategoryId,
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                Active = c.Active,
                ParentId = c.ParentId
            };
        }
    }
}