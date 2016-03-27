using Courses.Buisness.Services;
using Courses.Models;
using Courses.Models.Repositories;
using Courses.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Courses.Buisness.Comment
{
    public class OrderService : IOrderService
    {
        /// <summary>
        /// Репозиторий, используемый сервисом
        /// </summary>
        private readonly IOrderRepository orderRepository;
        /// <summary>
        /// Фабрика фильтров
        /// </summary>
        private readonly Filtering.IFilterFactory<Models.Order> filterFactory;
        /// <summary>
        /// Внедрение конструктора. Пример использования паттернов Dependecy Injection
        /// </summary>
        /// <param name="repository"></param>
        public OrderService(IOrderRepository _orderRepository, Filtering.IFilterFactory<Models.Order> filterFactory)
        {
            ///Guard Condition
            if (_orderRepository == null)
                throw new ArgumentNullException("Repository is null!");
            if (filterFactory == null)
                throw new ArgumentNullException("Filtering Factory is null!");
            this.orderRepository = _orderRepository;
            this.filterFactory = filterFactory;
        }
        /// <summary>
        /// Получение всех заказов на заданной странице с заданными фильтрами.
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="fieldFilters">Список фильтров</param>
        /// <param name="sortFilter">Порядок сортировки</param>
        /// <returns></returns>
        public OrderCollectionViewModel GetOrders(int page, int pageSize,
           List<Filtering.FieldFilter> fieldFilter = null, Filtering.SortFilter sortFilter = null)
        {
            IEnumerable<OrderViewModel> orders;
            int total;
            if (fieldFilter != null && sortFilter != null)
            {
                var newSortFilter = new SortFilter() { SortOrder = sortFilter.SortOrder };
                var expression = filterFactory.GetFilterExpression(fieldFilter);
                orders = orderRepository.Get(page, pageSize, expression, newSortFilter).Select(Convert);
                total = orderRepository.Count(expression);
            }
            else
            {
                orders = orderRepository.Get(page, pageSize, x => true).Select(Convert);
                total = orderRepository.Count(x => true);
            }
            var pageInfo = new PageInfo()
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = total
            };
            return new OrderCollectionViewModel() { Orders = orders, PageInfo = pageInfo };
        }



        /// <summary>
        /// Получение информации о заказе по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderViewModel GetByID(int id)
        {
            var comment = orderRepository.Get(id);
            return (comment == null) ? null : Convert(comment);
        }
        /// <summary>
        /// Добавление нового заказа
        /// </summary>
        /// <param name="orderView"></param>
        public void Add(OrderViewModel orderView)
        {
            orderView.CreatedDate = orderView.UpdateDate = DateTime.Now;
            orderRepository.Add(Convert(orderView));
        }
        /// <summary>
        /// Редактирование заказа
        /// </summary>
        /// <param name="category"></param>
        public void Edit(OrderViewModel orderView)
        {
            orderView.UpdateDate = DateTime.Now;
            orderRepository.Add(Convert(orderView));
        }
        /// <summary>
        /// Удаление заказа
        /// </summary>
        /// <param name="orderView"></param>
        public void Delete(OrderViewModel orderView)
        {
            orderRepository.Delete(Convert(orderView));
        }
        /// <summary>
        /// Сохранение изменений
        /// </summary>
        public void SaveChanges()
        {
            orderRepository.SaveChanges();
        }
        /// <summary>
        /// Конвертационные функции
        /// </summary>
        private Models.Order Convert(OrderViewModel c)
        {
            return new Models.Order()
            {
                Id = c.Id,
                CreatedDate = c.CreatedDate,
                UpdateDate = c.UpdateDate,
                CustomerId = c.CustomerId
            };
        }
        private OrderViewModel Convert(Models.Order c)
        {
            return new OrderViewModel()
            {
                Id = c.Id,
                CreatedDate = c.CreatedDate,
                UpdateDate = c.UpdateDate,
                CustomerId = c.CustomerId
            };
        }
    }
}
