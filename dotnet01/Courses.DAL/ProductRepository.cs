﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models.Repositories;
using Courses.Models;
using System.Data.Entity;

namespace Courses.DAL
{
    /// <summary>
    /// Реализация репозитория для работы с курсами
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// Контекст Entity Framework, используем для работы с БД
        /// </summary>
        ProductContext context = new ProductContext();

        public IEnumerable<Models.Product> Get()
        {
            return context.Product.AsEnumerable();
        }

        public IEnumerable<Models.Product> Get(int page, int pageSize, Func<Models.Product, bool> expression)
        {
            //временное решение
            return context.Product.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<Models.Product> Get(int page, int pageSize, Func<Models.Product, bool> expression, SortFilter sortFilter)
        {
            if (String.IsNullOrWhiteSpace(sortFilter.SortOrder))
            {
                return context.Product.Where(expression).OrderBy(s => s.Id).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            switch (sortFilter.SortOrder)
            {
                case "Name":
                    return context.Product.Where(expression).OrderBy(s => s.Name).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "NameDesc":
                    return context.Product.Where(expression).OrderByDescending(s => s.Name).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Description":
                    return context.Product.Where(expression).OrderBy(s => s.Description).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "DescriptionDesc":
                    return context.Product.Where(expression).OrderByDescending(s => s.Description).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "CreatedDate":
                    return context.Product.Where(expression).OrderBy(s => s.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "CreatedDateDesc":
                    return context.Product.Where(expression).OrderByDescending(s => s.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "UpdatedDate":
                    return context.Product.Where(expression).OrderBy(s => s.UpdatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "UpdatedDateDesc":
                    return context.Product.Where(expression).OrderByDescending(s => s.UpdatedDate).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Active":
                    return context.Product.Where(expression).OrderBy(s => s.Active).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "ActiveDesc":
                    return context.Product.Where(expression).OrderByDescending(s => s.Active).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Type":
                    return context.Product.Where(expression).OrderBy(s => s.Type).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "TypeDesc":
                    return context.Product.Where(expression).OrderByDescending(s => s.Type).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "PartnerID":
                    return context.Product.Where(expression).OrderBy(s => s.PartnerId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "PartnerIDDesc":
                    return context.Product.Where(expression).OrderByDescending(s => s.PartnerId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Teacher":
                    return context.Product.Where(expression).OrderBy(s => s.Teacher).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "TeacherDesc":
                    return context.Product.Where(expression).OrderByDescending(s => s.Teacher).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "SeatsCount":
                    return context.Product.Where(expression).OrderBy(s => s.SeatsCount).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "SeatsCountDesc":
                    return context.Product.Where(expression).OrderByDescending(s => s.SeatsCount).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "AssignedUserID":
                    return context.Product.Where(expression).OrderBy(s => s.AssignedUserId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "AssignedUserIDrDesc":
                    return context.Product.Where(expression).OrderByDescending(s => s.AssignedUserId).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                case "Location":
                    return context.Product.Where(expression).OrderBy(s => s.Location).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
                case "LocationDesc":
                    return context.Product.Where(expression).OrderByDescending(s => s.Location).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

                default:
                    return context.Product.Where(expression).OrderBy(s => s.Id).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
        }

        public Models.Product Get(int id)
        {
            return context.Product.Find(id);
        }

        public void Add(Models.Product entity)
        {
            context.Product.Add(entity);
        }

        public void Update(Models.Product entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Models.Product entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public int Count(Func<Models.Product, bool> expression)
        {
            return context.Product.Where(expression).Count();
        }
    }
}