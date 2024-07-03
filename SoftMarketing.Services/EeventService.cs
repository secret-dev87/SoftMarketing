using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using SoftMarketing.Model;
using SoftMarketing.DAL;
using SoftMarketing.DAL.UnitOfWork;

namespace SoftMarketing.Service
{

    public class EeventService
    {
        EventDAL EventDAL { get; set; }
        public EeventService()
        {
            EventDAL = new EventDAL();
        }


        //public Int32 Add(Events Event, DbTransaction transaction)
        //{
        //    return EventDAL.Add(Event, transaction);
        //}

        //public void addAndRemove(Events Event, DbTransaction transaction)
        //{
        //    this.Add(Event, transaction);
        //    this.Update(Event, transaction);
        //}
        //public Int32 Update(Events Event, DbTransaction transaction)
        //{
        //    return EventDAL.Update(Event, transaction);
        //}

        public void Delete(Int32 eventid, DbTransaction transaction)
        {
            EventDAL.Delete(eventid, transaction);
        }

        public IEnumerable<Events> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
        {
            return EventDAL.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn, connectionString);
        }


        public Events Get(Int32 eventid, string connectionString = null)
        {
            return EventDAL.Get(eventid, connectionString); 
        }

        public Int32 CountAll()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                EventDAL EventDAL = (EventDAL)unitOfWork.Repository<EventDAL>();
                return EventDAL.CountAll();
            }
        }

        public void AddAndUpdatCustomer()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                EventDAL EventDAL = (EventDAL)unitOfWork.Repository<EventDAL>();
                EventDAL.Add();
                EventDAL.Add();
                unitOfWork.SaveChanges();
            }
        }
        
        //public void AddAndUpdatCustomer(Events Event)
        //{
        //    using (var unitOfWork = new UnitOfWork(true))
        //    {
        //        EventDAL EventDAL = (EventDAL)unitOfWork.Repository<EventDAL>();
        //        EventDAL.Add(Event);
        //        EventDAL.Update(Event);
        //        unitOfWork.SaveChanges();
        //    }
        //}
        public IEnumerable<Events> GetAll(string connectionString = null)
        {
            return EventDAL.GetAll(connectionString);
        }

        public IEnumerable<Events> GetByPk(Int32 eventid, string connectionString = null)
        {
            return EventDAL.GetByPk(eventid, connectionString);
        }

    }
}