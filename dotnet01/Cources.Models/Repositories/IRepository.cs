﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models.Repositories
{
    /// <summary>
    /// Интерфейс базового репозитория с базовыми методами
    /// </summary>
    /// <typeparam name="T">Класс доменной модели</typeparam>
    public interface IRepository<T> where T:DomainObject
    {
        //Осторожно, следующий метод является ЖУТКИМ костылем.
        /// <summary>
        /// Получает количество записей в БД по заданному условию.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        int Count(Func<T, bool> expression);
        /// <summary>
        /// Получает все сущности с БД
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Get();
        /// <summary>
        /// Получает все сущности по заданному условию
        /// </summary>
        /// <param name="expression">Лямбда-выражение, описывающее условие поиска</param>
        /// <returns></returns>
        IEnumerable<T> Get(int page, int pageSize, Func<T, bool> expression);
        /// <summary>
        /// Получает все сущности по заданному условию и с заданной сортировкой
        /// </summary>
        /// <param name="expression">Лямбда-выражение, описывающее условие поиска</param>
        /// <param name="sortFilter">Поле и направление сортировки</param>
        /// <returns></returns>
        IEnumerable<T> Get(int page, int pageSize, Func<T, bool> expression, SortFilter sortFilter);
        /// <summary>
        /// Получение единственной сущности по значению ключевого поля
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);
        /// <summary>
        /// Добавление сущности в БД
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);
        /// <summary>
        /// Обновление значений полей сущности в БД
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        /// Удаление сущности из БД
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// Сохраняет изменения в БД
        /// </summary>
        void SaveChanges();
    }
}
