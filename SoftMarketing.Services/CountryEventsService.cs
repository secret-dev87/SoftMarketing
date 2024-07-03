using System;
using System.Data;
using System.Data.Common; 
using System.Collections;
using SoftMarketing.DAL;
using SoftMarketing.Model;

namespace SoftMarketing.Service
{
	
	public class CountryEventsService
	{
		CountryEventsDAL CountryEventsDAL { set; get; }

		public CountryEventsService(){
			CountryEventsDAL = new CountryEventsDAL();
		}
		
		public Int32 Add(CountryEvents entity, DbTransaction dbTransaction = null)
		{
				return CountryEventsDAL.Add(entity, dbTransaction);
		}
		public void AddMultiple(List<CountryEvents> entityCollection, DbTransaction dbTransaction = null)
		{
			foreach (var item in entityCollection){
					Add(item, dbTransaction);
			}
		}

		public Int32 Update(CountryEvents entity, DbTransaction dbTransaction = null)
		{
			return CountryEventsDAL.Update(entity, dbTransaction);
		}
		public void UpdateMultiple(List<CountryEvents> entityCollection, DbTransaction dbTransaction = null)
		{
			foreach (var item in entityCollection)
			{
				Update(item, dbTransaction);
			}
		}
		
		public void Delete(int countryEventId, DbTransaction dbTransaction = null)
		{
			CountryEventsDAL.Delete(countryEventId, dbTransaction);		
		}

		public CountryEvents GetByID(Int32 id)
		{
			return CountryEventsDAL.GetByID(id);
		}
	}
}