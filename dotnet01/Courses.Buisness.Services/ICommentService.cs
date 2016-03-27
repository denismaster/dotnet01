using Courses.ViewModels;
using System.Collections.Generic;

namespace Courses.Buisness.Services
{
    public interface ICommentService
    {
        /// <summary>
        /// Возвращает список всех комментариев по заданному фильтру
        /// </summary>
        CommentCollectionViewModel GetComments(int page, int pageSize,
           List<Filtering.FieldFilter> fieldFilter = null, Filtering.SortFilter sortFilter = null);

        /// <summary>
        /// Получение комментария
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CommentViewModel GetByID(int id);
        /// <summary>
        /// Добавление комментария
        /// </summary>
        /// <param name="comment"></param>
        void Add(CommentViewModel comment);
        /// <summary>
        /// Обновление данных о комментарие
        /// </summary>
        /// <param name="comment"></param>
        void Edit(CommentViewModel comment);
        /// <summary>
        /// Удаление комментария
        /// </summary>
        /// <param name="comment"></param>
        void Delete(CommentViewModel comment);
        /// <summary>
        /// Сохранение изменений в репозитории
        /// </summary>
        void SaveChanges();
    }
}
