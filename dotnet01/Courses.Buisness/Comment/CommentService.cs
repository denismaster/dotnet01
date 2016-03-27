using Courses.Buisness.Services;
using Courses.Models;
using Courses.Models.Repositories;
using Courses.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Courses.Buisness.Comment
{
    public class CommentService : ICommentService
    {
        /// <summary>
        /// Репозиторий, используемый сервисом
        /// </summary>
        private readonly ICommentsRepository commentsRepository;
        /// <summary>
        /// Фабрика фильтров
        /// </summary>
        private readonly Filtering.IFilterFactory<Models.Comment> filterFactory;
        /// <summary>
        /// Внедрение конструктора. Пример использования паттернов Dependecy Injection
        /// </summary>
        /// <param name="repository"></param>
        public CommentService(ICommentsRepository _commentsRepository, Filtering.IFilterFactory<Models.Comment> filterFactory)
        {
            ///Guard Condition
            if (_commentsRepository == null)
                throw new ArgumentNullException("Repository is null!");
            if (filterFactory == null)
                throw new ArgumentNullException("Filtering Factory is null!");
            this.commentsRepository = _commentsRepository;
            this.filterFactory = filterFactory;
        }
        /// <summary>
        /// Получение комментариев на заданной странице с заданными фильтрами.
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="fieldFilters">Список фильтров</param>
        /// <param name="sortFilter">Порядок сортировки</param>
        /// <returns></returns>
        public CommentCollectionViewModel GetComments(int page, int pageSize,
           List<Filtering.FieldFilter> fieldFilter = null, Filtering.SortFilter sortFilter = null)
        {
            IEnumerable<CommentViewModel> comments;
            int total;
            if (fieldFilter != null && sortFilter != null)
            {
                var newSortFilter = new SortFilter() { SortOrder = sortFilter.SortOrder };
                var expression = filterFactory.GetFilterExpression(fieldFilter);
                comments = commentsRepository.Get(page, pageSize, expression, newSortFilter).Select(Convert);
                total = commentsRepository.Count(expression);
            }
            else
            {
                comments = commentsRepository.Get(page, pageSize, x => true).Select(Convert);
                total = commentsRepository.Count(x => true);
            }
            var pageInfo = new PageInfo()
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = total
            };
            return new CommentCollectionViewModel() { Comments = comments, PageInfo = pageInfo };
        }



        /// <summary>
        /// Получение информации о комментарии по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CommentViewModel GetByID(int id)
        {
            var comment = commentsRepository.Get(id);
            return (comment == null) ? null : Convert(comment);
        }
        /// <summary>
        /// Добавление комментария в репозиторий
        /// </summary>
        /// <param name="commentView"></param>
        public void Add(CommentViewModel commentView)
        {
            commentView.CreatedDate = commentView.UpdatedDate = DateTime.Now;
            commentsRepository.Add(Convert(commentView));
        }
        /// <summary>
        /// Редактирование комментария
        /// </summary>
        /// <param name="category"></param>
        public void Edit(CommentViewModel commentView)
        {
            commentView.UpdatedDate = DateTime.Now;
            commentsRepository.Add(Convert(commentView));
        }
        /// <summary>
        /// Удаление комментария
        /// </summary>
        /// <param name="comment"></param>
        public void Delete(CommentViewModel comment)
        {
            commentsRepository.Delete(Convert(comment));
        }
        /// <summary>
        /// Сохранение изменений
        /// </summary>
        public void SaveChanges()
        {
            commentsRepository.SaveChanges();
        }
        /// <summary>
        /// Конвертационные функции
        /// </summary>
        private Models.Comment Convert(CommentViewModel c)
        {
            return new Models.Comment()
            {
                Id = c.Id,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                Text = c.Text,
                CustomerId = c.CustomerId,
                ProductId = c.ProductId
            };
        }
        private CommentViewModel Convert(Models.Comment c)
        {
            return new CommentViewModel()
            {
                Id = c.Id,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                Text = c.Text,
                CustomerId = c.CustomerId,
                ProductId = c.ProductId
            };
        }
    }
}
