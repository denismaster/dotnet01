using Courses.ViewModels;
using System.Collections.Generic;

namespace Courses.Buisness.Services
{
    public interface IEventService
    {
        /// <summary>
        /// Возвращает список всех событий по заданному фильтру
        /// </summary>
        EventCollectionViewModel GetEvents(int page, int pageSize,
           List<Filtering.FieldFilter> fieldFilter = null, Filtering.SortFilter sortFilter = null);

        /// <summary>
        /// Получение события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        EventViewModel GetByID(int id);
        /// <summary>
        /// Добавление события
        /// </summary>
        /// <param name="_event"></param>
        void Add(EventViewModel _event);
        /// <summary>
        /// Обновление данных о событии
        /// </summary>
        /// <param name="_event"></param>
        void Edit(EventViewModel _event);
        /// <summary>
        /// Удаление события
        /// </summary>
        /// <param name="_event"></param>
        void Delete(EventViewModel _event);
        /// <summary>
        /// Сохранение изменений в репозитории
        /// </summary>
        void SaveChanges();
    }
}
