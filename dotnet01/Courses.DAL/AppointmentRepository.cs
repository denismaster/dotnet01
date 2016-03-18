using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models.Repositories;
using Courses.Models;
using System.Data.Entity;

namespace Courses.DAL
{
    class AppointmentRepository:IAppointmentRepository
    {
        DatabaseContext context = new DatabaseContext();

        public IEnumerable<Models.Appointment> Get()
        {
            return context.Appointments.AsEnumerable();
        }

        //временно нереализовано
        public IEnumerable<Appointment> Get(int page, int pageSize, Func<Appointment, bool> expression, SortFilter sortFilter)
        {
            return null;
        }
        public IEnumerable<Models.Appointment> Get(int page, int pageSize, Func<Models.Appointment, bool> expression)
        {
            return context.Appointments.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public Models.Appointment Get(int id)
        {
            return context.Appointments.Find(id);
        }

        public void Add(Models.Appointment entity)
        {
            context.Appointments.Add(entity);
        }

        public void Update(Models.Appointment entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(Models.Appointment entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public int Count(Func<Models.Appointment, bool> expression)
        {
            return context.Appointments.Where(expression).Count();
        }

        public Appointment GetOnlyOne()
        {
            return context.Appointments.FirstOrDefault();
        }
        public void Dispose()
        {
            context.Dispose();
        }
        public void ClearTable()
        {
            context.Appointments.RemoveRange(Get());
        }
    }
}
