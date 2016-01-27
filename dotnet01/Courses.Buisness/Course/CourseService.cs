using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models;
using Courses.Models.Repositories;
using Courses.ViewModels;

namespace Courses.Buisness
{
    public interface ICourseService
    {
        /// <summary>
        /// Возвращает список курсов. 
        /// TODO:Желательно возвращать готовые ViewModels, но это пока неважно.
        /// </summary>
        CourseCollectionViewModel GetCourses(int page, int pageSize,
            List<Filtering.FieldFilter> fieldFilter = null, SortFilter sortFilter = null);
        /// <summary>
        /// Получение одного курса
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CourseViewModel GetByID(int id);
        /// <summary>
        /// Добавление курса. 
        /// </summary>
        /// <param name="course"></param>
        void Add(CourseViewModel course);
        /// <summary>
        /// Обновление данных курса
        /// </summary>
        /// <param name="course"></param>
        void Edit(CourseViewModel course);
        /// <summary>
        /// Удаление курса
        /// </summary>
        /// <param name="course"></param>
        void Delete(CourseViewModel course);
        /// <summary>
        /// Сохранение изменений в репозитории
        /// </summary>
        void SaveChanges();
    }
    public class CourseService : ICourseService
    {
        /// <summary>
        /// Репозиторий, используемый сервисом
        /// </summary>
        private readonly ICourseRepository repository;
        /// <summary>
        /// Фабрика фильтров
        /// </summary>
        private readonly Filtering.IFilterFactory<Models.Course> filterFactory;
        /// <summary>
        /// Внедрение конструктора. Пример использования паттернов Dependecy Injection
        /// </summary>
        /// <param name="repository"></param>
        public CourseService(Models.Repositories.ICourseRepository repository, Filtering.IFilterFactory<Models.Course> filterFactory)
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
        /// Получение курсов на заданной странице с заданными фильтрами.
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="fieldFilters">Список фильтров</param>
        /// <param name="sortFilter">Порядок сортировки</param>
        /// <returns></returns>
        public CourseCollectionViewModel GetCourses(int page, int pageSize, List<Filtering.FieldFilter> fieldFilters = null,
            SortFilter sortFilter = null)
        {
            IEnumerable<CourseViewModel> courses;
            int total;
            if (fieldFilters != null && sortFilter != null)
            {
                var expression = filterFactory.GetFilterExpression(fieldFilters);
                courses = repository.Get(page, pageSize, expression, sortFilter).Select(Convert);
                total = repository.Count(expression);
            }
            else
            {
                courses = repository.Get(page, pageSize, x => true).Select(Convert);
                total = repository.Count(x => true);
            }
            var pageInfo = new PageInfo()
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = total
            };
            return new CourseCollectionViewModel() { Courses = courses, PageInfo = pageInfo };
        }

        /// <summary>
        /// Получение информации о курсе по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CourseViewModel GetByID(int id)
        {
            var course = repository.Get(id);
            return (course == null) ? null : Convert(course);
        }
        /// <summary>
        /// Добавление курса в репозиторий
        /// </summary>
        /// <param name="course"></param>
        public void Add(CourseViewModel course)
        {
            repository.Add(Convert(course));
        }
        /// <summary>
        /// Обновление курса
        /// </summary>
        /// <param name="course"></param>
        public void Edit(CourseViewModel course)
        {
            repository.Update(Convert(course));
        }
        /// <summary>
        /// Удаление курса
        /// </summary>
        /// <param name="course"></param>
        public void Delete(CourseViewModel course)
        {
            repository.Delete(Convert(course));
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
        private Course Convert(CourseViewModel c)
        {
            return new Course()
            {
                Id = c.Id,
                Title = c.Title,
                Cost = c.Cost,
                Length = c.Length,
                Dates = c.Dates,
                Location = c.Location,
                Teacher = c.Teacher,
                Organizer = c.Organizer,
                Description = c.Description,
                Image = c.Image,
                Active = c.Active
            };
        }
        private CourseViewModel Convert(Course c)
        {
            return new CourseViewModel()
            {
                Id = c.Id,
                Title = c.Title,
                Cost = c.Cost,
                Length = c.Length,
                Dates = c.Dates,
                Location = c.Location,
                Teacher = c.Teacher,
                Organizer = c.Organizer,
                Description = c.Description,
                Image = c.Image,
                Active = c.Active
            };
        }
    }
}
