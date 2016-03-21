using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models.Repositories
{
   public interface ICategoryRepository: IRepository<Category>
    {

        Category GetOnlyOne();
        void AddPartners(int categoryId, int partnerId);
        void AddProducts(int categoryId, int productId);
        /// <summary>
        /// Получает все сущности по заданному условию и с заданной сортировкой
        /// </summary>
        /// <param name="expression">Лямбда-выражение, описывающее условие поиска</param>
        /// <param name="sortFilter">Поле и направление сортировки</param>
        /// <returns></returns>
        IEnumerable<Category> Get(int page, int pageSize, Func<Category, bool> expression, SortFilter sortFilter);

    }
}
