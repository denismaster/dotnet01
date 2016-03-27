using Courses.Buisness.Services;
using Courses.Models;
using Courses.Models.Repositories;
using Courses.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Courses.Buisness.Comment
{
    public class EventService : IEventService
    {
        /// <summary>
        /// Репозиторий, используемый сервисом
        /// </summary>
        private readonly IEventRepository eventRepository;
        /// <summary>
        /// Фабрика фильтров
        /// </summary>
        private readonly Filtering.IFilterFactory<Models.Event> filterFactory;
        /// <summary>
        /// Внедрение конструктора. Пример использования паттернов Dependecy Injection
        /// </summary>
        /// <param name="repository"></param>
        public EventService(IEventRepository _eventRepository, Filtering.IFilterFactory<Models.Event> filterFactory)
        {
            ///Guard Condition
            if (_eventRepository == null)
                throw new ArgumentNullException("Repository is null!");
            if (filterFactory == null)
                throw new ArgumentNullException("Filtering Factory is null!");
            this.eventRepository = _eventRepository;
            this.filterFactory = filterFactory;
        }
        /// <summary>
        /// Получение всех событий на заданной странице с заданными фильтрами.
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="fieldFilters">Список фильтров</param>
        /// <param name="sortFilter">Порядок сортировки</param>
        /// <returns></returns>
        public EventCollectionViewModel GetEvents(int page, int pageSize,
           List<Filtering.FieldFilter> fieldFilter = null, Filtering.SortFilter sortFilter = null)
        {
            IEnumerable<EventViewModel> events;
            int total;
            if (fieldFilter != null && sortFilter != null)
            {
                var newSortFilter = new SortFilter() { SortOrder = sortFilter.SortOrder };
                var expression = filterFactory.GetFilterExpression(fieldFilter);
                events = eventRepository.Get(page, pageSize, expression, newSortFilter).Select(Convert);
                total = eventRepository.Count(expression);
            }
            else
            {
                events = eventRepository.Get(page, pageSize, x => true).Select(Convert);
                total = eventRepository.Count(x => true);
            }
            var pageInfo = new PageInfo()
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = total
            };
            return new EventCollectionViewModel() { Events = events, PageInfo = pageInfo };
        }



        /// <summary>
        /// Получение информации о событии по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EventViewModel GetByID(int id)
        {
            var _event = eventRepository.Get(id);
            return (_event == null) ? null : Convert(_event);
        }
        /// <summary>
        /// Добавление нового события в репозиторий
        /// </summary>
        /// <param name="eventView"></param>
        public void Add(EventViewModel eventView)
        {
            eventRepository.Add(Convert(eventView));
        }
        /// <summary>
        /// Редактирование информации о событии
        /// </summary>
        /// <param name="category"></param>
        public void Edit(EventViewModel eventView)
        {
            eventRepository.Add(Convert(eventView));
        }
        /// <summary>
        /// Удаление события
        /// </summary>
        /// <param name="eventView"></param>
        public void Delete(EventViewModel eventView)
        {
            eventRepository.Delete(Convert(eventView));
        }
        /// <summary>
        /// Сохранение изменений
        /// </summary>
        public void SaveChanges()
        {
            eventRepository.SaveChanges();
        }
        /// <summary>
        /// Конвертационные функции
        /// </summary>
        private Models.Event Convert(EventViewModel c)
        {
            return new Models.Event()
            {
                Id = c.Id,
                CreatedDate = c.CreatedDate,
                Entity = c.Entity,
                Changes = c.Changes,
                UserId = c.UserId

            };
        }
        private EventViewModel Convert(Models.Event c)
        {
            return new EventViewModel()
            {
                Id = c.Id,
                CreatedDate = c.CreatedDate,
                Entity = c.Entity,
                Changes = c.Changes,
                UserId = c.UserId
            };
        }
    }
}
