using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models;
using Courses.ViewModels;
namespace Courses.Buisness
{
    public interface IPartnerService
    {
        /// <summary>
        /// Возвращает список партнеров. 
        /// </summary>
        PartnerCollectionViewModel GetPartners(int page, int pageSize,
            List<Filtering.FieldFilter> fieldFilter = null, SortFilter sortFilter = null);
        /// <summary>
        /// Получение одного партнера
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PartnerViewModel GetByID(int id);
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
        void SaveChanges();
    }
}
