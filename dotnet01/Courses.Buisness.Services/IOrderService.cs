using Courses.ViewModels;
using System.Collections.Generic;

namespace Courses.Buisness.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// Возвращает список всех заказов по заданному фильтру
        /// </summary>
        OrderCollectionViewModel GetOrders(int page, int pageSize,
           List<Filtering.FieldFilter> fieldFilter = null, Filtering.SortFilter sortFilter = null);

        /// <summary>
        /// Получение заказа
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OrderViewModel GetByID(int id);
        /// <summary>
        /// Добавление заказа
        /// </summary>
        /// <param name="order"></param>
        void Add(OrderViewModel order);
        /// <summary>
        /// Обновление данных о заказе
        /// </summary>
        /// <param name="order"></param>
        void Edit(OrderViewModel order);
        /// <summary>
        /// Удаление заказа
        /// </summary>
        /// <param name="order"></param>
        void Delete(OrderViewModel order);
        /// <summary>
        /// Сохранение изменений в репозитории
        /// </summary>
        void SaveChanges();
    }
}
