using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models.Repositories
{
    public interface IProductRepository: IRepository<Product>
    {

        Product GetOnlyOne();

                /// <summary>
        /// Получает все сущности по заданному условию и с заданной сортировкой
        /// </summary>
        /// <param name="expression">Лямбда-выражение, описывающее условие поиска</param>
        /// <param name="sortFilter">Поле и направление сортировки</param>
        /// <returns></returns>
        IEnumerable<Product> Get(int page, int pageSize, Func<Product, bool> expression, SortFilter sortFilter);
    }
}
