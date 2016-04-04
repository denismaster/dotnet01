using Courses.ViewModels;
using System.Collections.Generic;
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
        /// Получение всех курсов без фильтров и сортировок
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductViewModelForWebApi> GetProductsCollectionForWebAPI();

        /// <summary>
        /// получение курса со списком аккаунтов и партнеров, для передачи его в форму добавления/редактирования
        /// </summary>
        /// <param name="Id">Id продукта для редактирования</param>
        /// <returns></returns>
        ProductForAddEditViewModel GetProductWithAccauntsAndPartners(int? Id);
        /// <summary>
        /// Получение одного курса
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ProductViewModel GetById(int Id);

        /// <summary>
        /// Получение информации о курсе по его идентификатору 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ProductViewModelForWebApi GetByIdForWebApi(int Id);

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


        /// получение продукта со списком всех категорий
        /// </summary>
        /// <param name="Id">Id продукта для редактирования</param>
        /// <returns></returns>
        ProductWithAllCategorysViewModel GetProductWithAllCategorys(int Id);
        /// <summary>
        /// Получает продукт со список категорий текущего продукта
        /// </summary>
        /// <param name="id"></param>
        ProductWithCategorysViewModel GetProductWithCurrentCategorys(int id);

        /// <summary>
        /// Редактирование списка категорий продукта
        /// </summary>
        /// <param name="product"></param>
        void EditProductCategorys(ProductWithAllCategorysViewModel productView, int[] selectedCourses);
        void Delete(ProductViewModel product);
        /// <summary>
        /// Сохранение изменений в репозитории
        /// </summary>
        void SaveChanges();
    }
}
