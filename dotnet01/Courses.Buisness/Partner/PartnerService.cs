using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models;
using Courses.Models.Repositories;
using Courses.ViewModels;
using Courses.Buisness.Services;
using System.Web.Mvc;

namespace Courses.Buisness
{
    public class PatherService : IPartnerService
    {
        /// <summary>
        /// Репозиторий, используемый сервисом
        /// </summary>
        private readonly IPartnerRepository repository;
        /// <summary>
        /// Репозиторий, используемый сервисом для получения аккаунтов (менеджеров)
        /// </summary>
        private readonly IAccountRepository repositoryAccounts;
        /// <summary>
        /// Фабрика фильтров
        /// </summary>
        private readonly Filtering.IFilterFactory<Models.Partner> filterFactory;
        /// <summary>
        /// Внедрение конструктора. Пример использования паттернов Dependecy Injection
        /// </summary>
        /// <param name="repository"></param>
        public PatherService(Models.Repositories.IPartnerRepository repository, Models.Repositories.IAccountRepository repositoryAccounts,
            Filtering.IFilterFactory<Models.Partner> filterFactory)
        {
            ///Guard Condition
            if (repository == null)
                throw new ArgumentNullException("Repository is null!");
            if (filterFactory == null)
                throw new ArgumentNullException("Filtering Factory is null!");
            this.repository = repository;
            this.repositoryAccounts = repositoryAccounts;
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
        public PartnerCollectionViewModel GetPartners(int page, int pageSize,
            List<Filtering.FieldFilter> fieldFilters = null,
             Filtering.SortFilter sortFilter = null)
        {
            IEnumerable<PartnerViewModel> partners;
            int total;
            if (fieldFilters != null && sortFilter != null)
            {
                var newSortFilter = new SortFilter() { SortOrder = sortFilter.SortOrder };
                var expression = filterFactory.GetFilterExpression(fieldFilters);
                partners = repository.Get(page, pageSize, expression, newSortFilter).Select(Convert);
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
        /// получение партнера со списком аккаунтов, для передачи его в форму добавления/редактирования
        /// </summary>
        /// <param name="Id">Id партнера для редактирования</param>
        /// <returns></returns>
        public PartnerViewModelForAddEditView GetPartnerWithMenegers(int? Id)
        {
            PartnerViewModelForAddEditView partnerView = new PartnerViewModelForAddEditView();
            if (Id == null)
            {
                //для возможности не выбирать менеджера                                                              
                User noManager = new User { Id = 0, Login = "------------Отсутствует----------", Status = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };
                var listUser = repositoryAccounts.Get().ToList<User>();
                listUser.Add(noManager);
                partnerView.Managers = new SelectList(listUser, "Id", "Login", 0);
            }
            else
            {
                var partner = repository.Get(Id.Value);
                if (partner != null)
                {
                    partnerView = ConvertToPartnerViewModelForAddEditView(partner);
                    if (partner.User == null)
                    {
                        User noManager = new User { Id = 0, Login = "------------Отсутствует----------", Status = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };
                        var listUser = repositoryAccounts.Get().ToList<User>();
                        listUser.Add(noManager);
                        partnerView.Managers = new SelectList(listUser, "Id", "Login", 0);
                    }
                    else
                        partnerView.Managers = new SelectList(repositoryAccounts.Get(), "Id", "Login", partner.UserId);
                }
            }
            return partnerView;
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
            partner.CreatedDate = partner.UpdatedDate = DateTime.Now;
            if (partner.UserId == 0)
                partner.UserId = null;
            repository.Add(Convert(partner));
        }
        /// <summary>
        /// Обновление партнера
        /// </summary>
        /// <param name="partner"></param>
        public void Edit(PartnerViewModel partner)
        {
            partner.UpdatedDate = DateTime.Now;
            if (partner.UserId == 0)
                partner.UserId = null;
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
                PartnerId = c.Id,
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                UserId = c.UserId ?? null,
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
                Id = c.PartnerId,
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                UserId = c.UserId ?? null,
                Address = c.Address,
                Phone = c.Phone,
                Email = c.Email,
                Contact = c.Contact
            };
        }
        private PartnerViewModelForAddEditView ConvertToPartnerViewModelForAddEditView(Models.Partner c)
        {
            return new PartnerViewModelForAddEditView()
            {
                Id = c.PartnerId,
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                UserId = c.UserId ?? null,
                Address = c.Address,
                Phone = c.Phone,
                Email = c.Email,
                Contact = c.Contact
            };
        }
    }
}