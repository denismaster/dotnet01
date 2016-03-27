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
        private readonly IPartnerRepository partnerRepository;
        /// <summary>
        /// Репозиторий, используемый сервисом для получения аккаунтов (менеджеров)
        /// </summary>
        private readonly IAccountRepository accountRepository;
        /// <summary>
        /// Репозиторий, используемый сервисом для получения категорий
        /// </summary>
        private readonly ICategoryRepository categoryRepository;
        /// <summary>
        /// Фабрика фильтров
        /// </summary>
        private readonly Filtering.IFilterFactory<Models.Partner> filterFactory;
        /// <summary>
        /// Внедрение конструктора. Пример использования паттернов Dependecy Injection
        /// </summary>
        /// <param name="partnerRepository"></param>
        public PatherService(IPartnerRepository partnerRepository, IAccountRepository accountRepository, ICategoryRepository categoryRepository,
            Filtering.IFilterFactory<Models.Partner> filterFactory)
        {
            ///Guard Condition
            if (partnerRepository == null)
                throw new ArgumentNullException("Repository is null!");
            if (filterFactory == null)
                throw new ArgumentNullException("Filtering Factory is null!");
            this.partnerRepository = partnerRepository;
            this.accountRepository = accountRepository;
            this.categoryRepository = categoryRepository;
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
                partners = partnerRepository.Get(page, pageSize, expression, newSortFilter).Select(ConvertFromPartnerToPartnerViewModel);
                total = partnerRepository.Count(expression);
            }
            else
            {
                partners = partnerRepository.Get(page, pageSize, x => true).Select(ConvertFromPartnerToPartnerViewModel);
                total = partnerRepository.Count(x => true);
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
        /// Получение всех партнёров без фильтров и сортировок
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PartnerViewModel> GetIEnumerablePartnersCollection()
        {
            IEnumerable<PartnerViewModel> partners;
            partners = partnerRepository.Get().Select(ConvertFromPartnerToPartnerViewModel);
            return partners;
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
                var listUser = accountRepository.Get().ToList<User>();
                listUser.Add(noManager);
                partnerView.Managers = new SelectList(listUser, "Id", "Login", 0);
            }
            else
            {
                var partner = partnerRepository.Get(Id.Value);
                if (partner != null)
                {
                    partnerView = ConvertFromPartnerToPartnerViewModelForAddEditView(partner);
                    if (partner.User == null)
                    {
                        User noManager = new User { Id = 0, Login = "------------Отсутствует----------", Status = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };
                        var listUser = accountRepository.Get().ToList<User>();
                        listUser.Add(noManager);
                        partnerView.Managers = new SelectList(listUser, "Id", "Login", 0);
                    }
                    else
                        partnerView.Managers = new SelectList(accountRepository.Get(), "Id", "Login", partner.UserId);
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
            var partner = partnerRepository.Get(id);
            return (partner == null) ? null : ConvertFromPartnerToPartnerViewModel(partner);
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
            partnerRepository.Add(ConvertFromPartnerViewModelToPartner(partner));
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
            partnerRepository.Update(ConvertFromPartnerViewModelToPartner(partner));
        }
        /// <summary>
        /// Удаление партнера
        /// </summary>
        /// <param name="partner"></param>
        public void Delete(PartnerViewModel partner)
        {
            partnerRepository.Delete(ConvertFromPartnerViewModelToPartner(partner));
        }

        /// <summary>
        /// получение продукта со списком всех категорий
        /// </summary>
        /// <param name="Id">Id продукта для редактирования</param>
        /// <returns></returns>
        public PartnerWithAllCategorysViewModel GetPartnerWithAllCategorys(int Id)
        {
            var partner = partnerRepository.Get(Id);
            return (partner == null) ? null : ConvertFromPartnerToPartnerWithAllCategorysViewModel(partner);
        }
        /// <summary>
        /// Получает продукт со список категорий текущего продукта
        /// </summary>
        /// <param name="id"></param>
        public PartnerWithCategorysViewModel GetPartnerWithCurrentCategorys(int id)
        {
            var partner = partnerRepository.Get(id);
            return (partner == null) ? null : ConvertFromPartnerToPartnerWithCategorysViewModel(partner);
        }

        /// <summary>
        /// Редактирование списка категорий продукта
        /// </summary>
        /// <param name="product"></param>
        public void EditPartnerCategorys(PartnerWithAllCategorysViewModel partnerView, int[] selectedCategorys)
        {
            Partner partner = partnerRepository.Get(partnerView.Id);
            partner.UpdatedDate = DateTime.Now;
            partner.Categories.Clear();
            SaveChanges();

            if (selectedCategorys != null)
            {
                foreach (int categoryId in selectedCategorys)
                {
                    categoryRepository.AddPartners(categoryId, partner.PartnerId);
                }
            }
        }


        /// <summary>
        /// Сохранение изменений
        /// </summary>
        public void SaveChanges()
        {
            partnerRepository.SaveChanges();
        }
        /// <summary>
        /// Конвертационные функции
        /// </summary>
        private Partner ConvertFromPartnerViewModelToPartner(PartnerViewModel c)
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
        private PartnerViewModel ConvertFromPartnerToPartnerViewModel(Models.Partner c)
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
        private PartnerViewModelForAddEditView ConvertFromPartnerToPartnerViewModelForAddEditView(Models.Partner c)
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
       
        private Category ConvertFromCategoryViewModelToCategory(CategoryViewModel c)
        {
            return new Models.Category()
            {
                CategoryId = c.Id,
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                Active = c.Active,
                Description = c.Description,
                ParentCategoryId = c.ParentCategoryId
            };
        }
        private CategoryViewModel ConvertFromCategoryToCategoryViewModel(Category c)
        {
            return new CategoryViewModel()
            {
                Id = c.CategoryId,
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                Active = c.Active,
                ParentCategoryId = (c.ParentCategory == null) ? null : c.ParentCategoryId,
                Description = c.Description
            };
        }
        private PartnerWithCategorysViewModel ConvertFromPartnerToPartnerWithCategorysViewModel(Partner partner)
        {
            PartnerWithCategorysViewModel partnerWithCategorys = new PartnerWithCategorysViewModel();
            partnerWithCategorys.Categorys = new List<CategoryViewModel>();

            foreach (Category c in partner.Categories)
            {
                partnerWithCategorys.Categorys.Add(ConvertFromCategoryToCategoryViewModel(c));
            }

            partnerWithCategorys.Id = partner.PartnerId;
            partnerWithCategorys.Name = partner.Name;
            partnerWithCategorys.CreatedDate = partner.CreatedDate;
            partnerWithCategorys.UpdatedDate = partner.UpdatedDate;
            partnerWithCategorys.UserId = partner.UserId ?? null;
            partnerWithCategorys.Address = partner.Address;
            partnerWithCategorys.Phone = partner.Phone;
            partnerWithCategorys.Email = partner.Email;
            partnerWithCategorys.Contact = partner.Contact;

            return partnerWithCategorys;
        }
        private PartnerWithAllCategorysViewModel ConvertFromPartnerToPartnerWithAllCategorysViewModel(Partner c)
        {
            PartnerWithCategorysViewModel partnerWithCategorys = ConvertFromPartnerToPartnerWithCategorysViewModel(c);
            PartnerWithAllCategorysViewModel partnerWithAllCategorys = new PartnerWithAllCategorysViewModel();

            partnerWithAllCategorys.Categorys = partnerWithCategorys.Categorys;
            partnerWithAllCategorys.Id = partnerWithCategorys.Id;
            partnerWithAllCategorys.Name = partnerWithCategorys.Name;
            partnerWithAllCategorys.CreatedDate = partnerWithCategorys.CreatedDate;
            partnerWithAllCategorys.UpdatedDate = partnerWithCategorys.UpdatedDate;
            partnerWithAllCategorys.UserId = partnerWithCategorys.UserId ?? null;
            partnerWithAllCategorys.Address = partnerWithCategorys.Address;
            partnerWithAllCategorys.Phone = partnerWithCategorys.Phone;
            partnerWithAllCategorys.Email = partnerWithCategorys.Email;
            partnerWithAllCategorys.Contact = partnerWithCategorys.Contact;

            var categorysList = categoryRepository.Get().Select(ConvertFromCategoryToCategoryViewModel);
            partnerWithAllCategorys.AllCategorys = categorysList.ToList();

            return partnerWithAllCategorys;
        }
    }
}