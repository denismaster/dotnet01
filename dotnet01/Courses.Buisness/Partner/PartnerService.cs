using Courses.Models.Repositories;
using Courses.ViewModels;
using Courses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Courses.Buisness.Partner
{
    public class PartnerService : IPartnerService
    {
        /// <summary>
        /// Репозиторий, используемый сервисом
        /// </summary>
        private readonly IPartnerRepository repository;
        /// <summary>
        /// Фабрика фильтров
        /// </summary>
        private readonly Filtering.IFilterFactory<Models.Partner> filterFactory;
        /// <summary>
        /// Внедрение конструктора. Пример использования паттернов Dependecy Injection
        /// </summary>
        /// <param name="repository"></param>
        public PartnerService(Models.Repositories.IPartnerRepository repository, Filtering.IFilterFactory<Models.Partner> filterFactory)
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
        /// Получение партнеров на заданной странице с заданными фильтрами.
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="fieldFilters">Список фильтров</param>
        /// <param name="sortFilter">Порядок сортировки</param>
        /// <returns></returns>
        public PartnerCollectionViewModel GetPartners(int page, int pageSize, List<Filtering.FieldFilter> fieldFilters = null,
            SortFilter sortFilter = null)
        {
            IEnumerable<PartnerViewModel> partners;
            int total;
            if (fieldFilters != null && sortFilter != null)
            {
                var expression = filterFactory.GetFilterExpression(fieldFilters);
                partners = repository.Get(page, pageSize, expression, sortFilter).Select(Convert);
                total = repository.Count(expression);
            }
            else
            {
                partners = repository.Get(page, pageSize, x => true).Select(Convert);
                total = repository.Count(x => true);
            }
            var pageInfo = new PageInfo()
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = total
            };
            return new PartnerCollectionViewModel() { Partners = partners, PageInfo = pageInfo };
        }

        /// <summary>
        /// Получение информации о партнере по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartnerViewModel GetByID(int id)
        {
            var partner = repository.Get(id);
            return (partner == null) ? null : Convert(partner);
        }
        /// <summary>
        /// Добавление партнера в репозиторий
        /// </summary>
        /// <param name="partner"></param>
        public void Add(PartnerViewModel partner)
        {
            repository.Add(Convert(partner));
        }
        /// <summary>
        /// Обновление партнера
        /// </summary>
        /// <param name="partner"></param>
        public void Edit(PartnerViewModel partner)
        {
            repository.Update(Convert(partner));
        }
        /// <summary>
        /// Удаление партнера
        /// </summary>
        /// <param name="partner"></param>
        public void Delete(PartnerViewModel partner)
        {
            repository.Delete(Convert(partner));
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
        private Models.Partner Convert(PartnerViewModel c)
        {
            return new Models.Partner()
            {
                PartnerId = c.ID,
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                UserId = c.UserId,
                Address = c.Address,
                Phone = c.Phone,
                Email = c.Email,
                Contact = c.Contact         
                
            };
        }
        private PartnerViewModel Convert(Models.Partner c)
        {
            return new PartnerViewModel()
            {
                ID = c.PartnerId,
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                UserId = c.UserId,
                Address = c.Address,
                Phone = c.Phone,
                Email = c.Email,
                Contact = c.Contact
            };
        }
    }
}