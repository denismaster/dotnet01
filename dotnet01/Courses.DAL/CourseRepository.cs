using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models.Repositories;
using System.Data.Entity;
namespace Courses.DAL
{
    /// <summary>
    /// Реализация репозитория для работы с курсами
    /// </summary>
    public class CourseRepository : ICourseRepository
    {
        /// <summary>
        /// Контекст Entity Framework, используем для работы с БД
        /// </summary>
        CourseContext context = new CourseContext();

        public IEnumerable<Models.Course> Get()
        {
            return context.Course.AsEnumerable();
        }

        public IEnumerable<Models.Course> Get(int page, int pageSize, Func<Models.Course, bool> expression)
        {
            //временное решение
            return context.Course.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<Models.Course> Get(int page, int pageSize, Func<Models.Course, bool> expression, SortFilter sortFilter)
        {
            if (String.IsNullOrWhiteSpace(sortFilter.SortOrder))
            {
                return context.Course.Where(expression).OrderBy(s => s.Id).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            switch (sortFilter.SortOrder)
            {
                case "Title":
                    return context.Course.Where(expression).OrderBy(s => s.Title).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "TitleDesc":
                    return context.Course.Where(expression).OrderByDescending(s => s.Title).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Cost":
                    return context.Course.Where(expression).OrderBy(s => s.Cost).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "CostDesc":
                    return context.Course.Where(expression).OrderByDescending(s => s.Cost).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Length":
                    return context.Course.Where(expression).OrderBy(s => s.Length).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "LengthDesc":
                    return context.Course.Where(expression).OrderByDescending(s => s.Length).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Dates":
                    return context.Course.Where(expression).OrderBy(s => s.Dates).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "DatesDesc":
                    return context.Course.Where(expression).OrderByDescending(s => s.Dates).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Location":
                    return context.Course.Where(expression).OrderBy(s => s.Location).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "LocationDesc":
                    return context.Course.Where(expression).OrderByDescending(s => s.Location).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Teacher":
                    return context.Course.Where(expression).OrderBy(s => s.Teacher).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "TeacherDesc":
                    return context.Course.Where(expression).OrderByDescending(s => s.Teacher).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Organizer":
                    return context.Course.Where(expression).OrderBy(s => s.Organizer).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "OrganizerDesc":
                    return context.Course.Where(expression).OrderByDescending(s => s.Organizer).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Active":
                    return context.Course.Where(expression).OrderBy(s => s.Active).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "ActiveDesc":
                    return context.Course.Where(expression).OrderByDescending(s => s.Active).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                default:
                    return context.Course.Where(expression).OrderBy(s => s.Id).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
        }

        public Models.Course Get(int id)
        {
            return context.Course.Find(id);
        }

        public void Add(Models.Course entity)
        {
            context.Course.Add(entity);
        }

        public void Update(Models.Course entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(Models.Course entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public int Count(Func<Models.Course, bool> expression)
        {
            return context.Course.Where(expression).Count();
        }
    }
}
