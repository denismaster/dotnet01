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
        /// получение категории со списком категорий (для выбора родительской) , для передачи его в форму добавления/редактирования
        /// </summary>
        /// <param name="Id">Id категории для редактирования</param>
        /// <returns></returns>
        public CategoryViewModelForAddEditView GetCategoryWithCategorys(int? Id)
        {
            CategoryViewModelForAddEditView categoryView = new CategoryViewModelForAddEditView();
            if (Id == null)
            {
                //для возможности не выбирать категорию                                                            
                Category noCategory = new Category { CategoryId = 0, Name = "------------Отсутствует----------", Active = true, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };
                var listCategorys = repository.Get().ToList<Category>();
                listCategorys.Add(noCategory);
                categoryView.Categorys = new SelectList(listCategorys, "CategoryId", "Name", 0);
            }
            else
            {
                var category = repository.Get(Id.Value);
                if (category != null)
                {
                    categoryView = ConvertToCategoryViewModelForAddEditView(category);
                    //для возможности не выбирать категорию                                                            
                    Category noCategory = new Category { CategoryId = 0, Name = "------------Отсутствует----------", Active = true, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };
                    var listCategorys = repository.Get().ToList<Category>();
                    listCategorys.Add(noCategory);
                    listCategorys.Remove(category);
                    categoryView.Categorys = new SelectList(listCategorys, "CategoryId", "Name", 0);

                }
            }
            return categoryView;
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
        /// <param name="categoryView"></param>
        public void Add(CategoryViewModel categoryView)
        {
            categoryView.CreatedDate = categoryView.UpdatedDate = DateTime.Now;
            Category category = Convert(categoryView);
            if (categoryView.ParentCategoryId == 0)
                category._Category = null;
            else
            {
                category._Category = repository.Get(categoryView.ParentCategoryId.Value);
            }
            repository.Add(category);
        }
        /// <summary>
        /// Обновление категории
        /// </summary>
        /// <param name="category"></param>
        public void Edit(CategoryViewModel categoryView)
        {
            categoryView.UpdatedDate = DateTime.Now;
            Category category = Convert(categoryView);
            if (categoryView.ParentCategoryId == 0)
                category._Category = null;
            else
            {
                category._Category = repository.Get(categoryView.ParentCategoryId.Value);
            }
            repository.Update(category);
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
                Active = c.Active
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
                ParentCategoryId = c._Category.CategoryId
            };
        }
        private CategoryViewModelForAddEditView ConvertToCategoryViewModelForAddEditView(Models.Category c)
        {
            return new CategoryViewModelForAddEditView()
            {
                Id = c.CategoryId,
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                Active = c.Active,
                ParentCategoryId = c._Category.CategoryId
            };
        }
    }
}