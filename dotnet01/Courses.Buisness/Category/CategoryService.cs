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
    public class CategoryService : ICategoryService
    {
        /// <summary>
        /// Репозиторий, используемый сервисом
        /// </summary>
        private readonly ICategoryRepository categoryRepository;
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
            this.categoryRepository = repository;
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
                categorys = categoryRepository.Get(page, pageSize, expression, newSortFilter).Select(Convert);
                total = categoryRepository.Count(expression);
            }
            else
            {
                categorys = categoryRepository.Get(page, pageSize, x => true).Select(Convert);
                total = categoryRepository.Count(x => true);
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
        /// Получение всех категорий без фильтров и сортировок
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryViewModel> GetIEnumerableCategorysCollection()
        {
            IEnumerable<CategoryViewModel> categorys;
            categorys = categoryRepository.Get().Select(Convert);
            return categorys;
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
                var listCategorys = categoryRepository.Get().ToList<Category>();
                listCategorys.Add(noCategory);
                categoryView.Categorys = new SelectList(listCategorys, "CategoryId", "Name", 0);
            }
            else
            {
                var category = categoryRepository.Get(Id.Value);
                if (category != null)
                {
                    categoryView = ConvertToCategoryViewModelForAddEditView(category);
                    //для возможности не выбирать категорию                                                            
                    Category noCategory = new Category { CategoryId = 0, Name = "------------Отсутствует----------", Active = true, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };
                    var listCategorys = categoryRepository.Get().ToList<Category>();
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
            var category = categoryRepository.Get(id);
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
                category.ParentCategory = null;
            else
            {
                category.ParentCategory = categoryRepository.Get(categoryView.ParentCategoryId.Value);
            }
            categoryRepository.Add(category);
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
                category.ParentCategory = null;
            else
            {
                category.ParentCategory = categoryRepository.Get(categoryView.ParentCategoryId.Value);
            }
            categoryRepository.Update(category);
        }
        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <param name="categoryView"></param>
        public void Delete(CategoryViewModel categoryView)
        {
            Category categoryForDelete = categoryRepository.Get(categoryView.Id);
            IEnumerable<Category> childCategory = categoryRepository.Get(1, categoryRepository.Count(m => true), m => m.ParentCategoryId == categoryView.Id);
            foreach (Category c in childCategory)
            {
                c.ParentCategory = categoryForDelete.ParentCategory;
                c.ParentCategoryId = categoryForDelete.ParentCategoryId;
                categoryRepository.Update(c);
            }

            categoryRepository.Delete(categoryForDelete);
        }
        /// <summary>
        /// Сохранение изменений
        /// </summary>
        public void SaveChanges()
        {
            categoryRepository.SaveChanges();
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
                Description = c.Description,
                ParentCategoryId = (c.ParentCategoryId == 0) ? null : c.ParentCategoryId
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
                ParentCategoryId = (c.ParentCategoryId == 0) ? null : c.ParentCategoryId,
                Description = c.Description
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
                ParentCategoryId = (c.ParentCategoryId == 0) ? null : c.ParentCategoryId,
                Description = c.Description
            };
        }
    }
}