using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models;
using Courses.ViewModels;
namespace Courses.Buisness.Services
{
    public interface IPartnerService
    {
        /// <summary>
        /// Возвращает список партнеров. 
        /// </summary>
        PartnerCollectionViewModel GetPartners(int page, int pageSize,
            List<Filtering.FieldFilter> fieldFilter = null, Filtering.SortFilter sortFilter = null);

        /// <summary>
        /// Получение всех партнёров без фильтров и сортировок
        /// </summary>
        /// <returns></returns>
        IEnumerable<PartnerViewModel> GetIEnumerablePartnersCollection();

        /// <summary>
        /// Получение одного партнера
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PartnerViewModel GetByID(int id);

        /// <summary>
        /// получение партнера со списком аккаунтов, для передачи его в форму добавления/редактирования
        /// </summary>
        /// <param name="Id">Id партнера для редактирования</param>
        /// <returns></returns>
        PartnerViewModelForAddEditView GetPartnerWithMenegers(int? Id);
        /// <summary>
        /// Добавление партнера 
        /// </summary>
        /// <param name="partner"></param>
        void Add(PartnerViewModel partner);
        /// <summary>
        /// Обновление данных партнера
        /// </summary>
        /// <param name="partner"></param>
        void Edit(PartnerViewModel partner);
        /// <summary>
        /// Удаление партнера
        /// </summary>
        /// <param name="partner"></param>
        void Delete(PartnerViewModel partner);
        /// <summary>
        /// Сохранение изменений в репозитории
        /// </summary>

        /// <summary>
        /// получение продукта со списком всех категорий
        /// </summary>
        /// <param name="Id">Id продукта для редактирования</param>
        /// <returns></returns>
        PartnerWithAllCategorysViewModel GetPartnerWithAllCategorys(int Id);
        /// <summary>
        /// Получает продукт со список категорий текущего продукта
        /// </summary>
        /// <param name="id"></param>
        PartnerWithCategorysViewModel GetPartnerWithCurrentCategorys(int id);

        /// <summary>
        /// Редактирование списка категорий продукта
        /// </summary>
        /// <param name="product"></param>
        void EditPartnerCategorys(PartnerWithAllCategorysViewModel partnerView, int[] selectedCategorys);

        void SaveChanges();
    }
}
