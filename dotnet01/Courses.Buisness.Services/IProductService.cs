using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.ViewModels;
using Courses.Models;
namespace Courses.Buisness.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Возвращает список курсов. 
        /// TODO:Желательно возвращать готовые ViewModels, но это пока неважно.
        /// </summary>
        ProductCollectionViewModel GetProducts(int page, int pageSize,
            List<Filtering.FieldFilter> fieldFilter = null, Filtering.SortFilter sortFilter = null);
        /// <summary>
        /// Получение одного курса
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ProductViewModel GetById(int Id);
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
}
