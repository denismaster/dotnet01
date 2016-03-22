﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.ViewModels;
using Courses.Models;
namespace Courses.Buisness.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Возвращает список курсов. 
        /// TODO:Желательно возвращать готовые ViewModels, но это пока неважно.
        /// </summary>
        ProductCollectionViewModel GetProducts(int page, int pageSize,
            List<Filtering.FieldFilter> fieldFilter = null, Filtering.SortFilter sortFilter = null);

        /// <summary>
        /// Получение всех курсов без фильтров и сортировок
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductViewModel> GetIEnumerableProductsCollection();

        /// <summary>
        /// получение курса со списком аккаунтов и партнеров, для передачи его в форму добавления/редактирования
        /// </summary>
        /// <param name="Id">Id продукта для редактирования</param>
        /// <returns></returns>
        ProductViewModelForAddEditView GetProductWithAccauntsAndPartners(int? Id);
        /// <summary>
        /// Получение одного курса
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ProductViewModel GetById(int Id);
       
        /// <summary>
        /// Добавление курса. 
        /// </summary>
        /// <param name="product"></param>
        void Add(ProductViewModel product);
        /// <summary>
        /// Обновление данных курса
        /// </summary>
        /// <param name="product"></param>
        void Edit(ProductViewModel product);
        /// <summary>
        /// Удаление курса
        /// </summary>
        /// <param name="product"></param>
        ///  /// <summary>
        /// получение продукта со списком всех категорий
        /// </summary>
        /// <param name="Id">Id продукта для редактирования</param>
        /// <returns></returns>
        ProductCategoryViewModel GetProductCategory(int? Id);
        /// <summary>
        /// Редактирование списка категорий продукта
        /// </summary>
        /// <param name="product"></param>
        void EditProductCategorys(ProductCategoryViewModel productView, int[] selectedCourses);
        void Delete(ProductViewModel product);
        /// <summary>
        /// Сохранение изменений в репозитории
        /// </summary>
        void SaveChanges();
    }
}
