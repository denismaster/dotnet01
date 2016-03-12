
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models;
using Courses.ViewModels;
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
