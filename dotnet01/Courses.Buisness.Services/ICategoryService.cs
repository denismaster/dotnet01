using Courses.ViewModels;
using System.Collections.Generic;
namespace Courses.Buisness.Services
{
    public interface ICategoryService
    {
        /// <summary>
        /// Возвращает список категорий
        /// </summary>
        CategoryCollectionViewModel GetCategorys(int page, int pageSize,
           List<Filtering.FieldFilter> fieldFilter = null, Filtering.SortFilter sortFilter = null);
        /// <summary>
        /// получение категории со списком категорий (для выбора родительской) , для передачи его в форму добавления/редактирования
        /// </summary>
        /// <param name="Id">Id категории для редактирования</param>
        /// <returns></returns>
        CategoryViewModelForAddEditView GetCategoryWithCategorys(int? Id);

        /// <summary>
        /// Получение всех категорий без фильтров и сортировок
        /// </summary>
        /// <returns></returns>
        IEnumerable<CategoryViewModel> GetIEnumerableCategorysCollection();
        /// <summary>
        /// Получение категории
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CategoryViewModel GetByID(int id);
        /// <summary>
        /// Добавление категории
        /// </summary>
        /// <param name="partner"></param>
        void Add(CategoryViewModel category);
        /// <summary>
        /// Обновление данных о категории
        /// </summary>
        /// <param name="category"></param>
        void Edit(CategoryViewModel category);
        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <param name="category"></param>
        void Delete(CategoryViewModel category);
        /// <summary>
        /// Сохранение изменений в репозитории
        /// </summary>
        void SaveChanges();
    }
}
