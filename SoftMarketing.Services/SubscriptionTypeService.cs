using System;
using System.Data;
using System.Data.Common; 
using System.Collections;
using SoftMarketing.Model;
using SoftMarketing.DAL;

namespace SoftMarketing.Service
{
	
	public class SubscriptionTypeService
	{
		SubscriptionTypeDAL SubscriptionTypeDAL { get; set; }
		public SubscriptionTypeService()
		{
			SubscriptionTypeDAL = new SubscriptionTypeDAL();
		}

		public Int32 Add(SubscriptionType SubscriptionType, DbTransaction transaction)
		{
			return SubscriptionTypeDAL.Add(SubscriptionType, transaction);
		}

		public Int32 Update(SubscriptionType SubscriptionType, DbTransaction transaction)
		{
			return SubscriptionTypeDAL.Update(SubscriptionType, transaction);
		}

		public void Delete(Int32 subscriptiontypeid, DbTransaction transaction)
		{
			SubscriptionTypeDAL.Delete(subscriptiontypeid, transaction);
		}

		public IEnumerable<SubscriptionType> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			return SubscriptionTypeDAL.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn, connectionString);
		}
		
		public SubscriptionType Get(Int32 subscriptiontypeid, string connectionString = null)
		{
			return SubscriptionTypeDAL.Get(subscriptiontypeid, connectionString);
		}


		public Int32 CountAll(string connectionString = null)
		{
			return SubscriptionTypeDAL.CountAll(connectionString);
		}

		public IEnumerable<SubscriptionType> GetAll(string connectionString = null)
		{
			return SubscriptionTypeDAL.GetAll(connectionString);
		}

		public IEnumerable<SubscriptionType> GetByPk(Int32 eventid, string connectionString = null)
		{
			return SubscriptionTypeDAL.GetByPk(eventid, connectionString);
		}
	}
}