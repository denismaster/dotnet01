﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models.Repositories;
using System.Data.Entity;
namespace Courses.DAL
{
    /// <summary>
    /// Реализация репозитория для работы с аккаунтами
    /// </summary>
    public class AccountRepository:IAccountRepository
    {
        /// <summary>
        /// Контекст Entity Framework, используем для работы с БД
        /// </summary>
        AccountContext context = new AccountContext();

        public IEnumerable<Models.Account> Get()
        {
            return context.Account.AsEnumerable();
        }
        /// <summary>
        /// <see cref="IAccountRepository.Get()"/>
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IEnumerable<Models.Account> Get(int page, int pageSize, Func<Models.Account, bool> expression)
        {
            return context.Account.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }
        
        public Models.Account Get(int id)
        {
            return context.Account.Find(id);
        }

        public void Add(Models.Account entity)
        {
            context.Account.Add(entity);
        }

        public void Update(Models.Account entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(Models.Account entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        
    }
}
