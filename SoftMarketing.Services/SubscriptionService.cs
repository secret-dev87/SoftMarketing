using System;
using System.Data;
using System.Data.Common; 
using System.Collections;
using SoftMarketing.DAL;
using SoftMarketing.Model;

namespace SoftMarketing.Service
{
	
	public class SubscriptionService
	{
		SubscriptionDAL SubscriptionDAL { get; set; }
		public SubscriptionService()
		{
			SubscriptionDAL = new SubscriptionDAL();
		}

		public Int32 Add(Subscription Subscription, DbTransaction transaction)
		{
			return SubscriptionDAL.Add(Subscription, transaction);	
		}

		public Int32 Update(Subscription Subscription, DbTransaction transaction)
		{
			return SubscriptionDAL.Update(Subscription, transaction);
		}

		public void Delete(Int32 subscriptionid, DbTransaction transaction)
		{
			SubscriptionDAL.Delete(subscriptionid, transaction);
		}

		public IEnumerable<Subscription> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			return SubscriptionDAL.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn, connectionString);
		}


		public Subscription Get(Int32 subscriptionid, string connectionString = null)
		{
			return SubscriptionDAL.Get(subscriptionid, connectionString);
		}

		public Int32 CountAll(string connectionString = null)
		{
			return SubscriptionDAL.CountAll(connectionString);
		}

		public IEnumerable<Subscription> GetAll(string connectionString = null)
		{
			return SubscriptionDAL.GetAll(connectionString);
		}

		public IEnumerable<Subscription> GetByPk(Int32 subscriptionid, string connectionString = null)
		{
			return SubscriptionDAL.GetByPk(subscriptionid, connectionString);
		}

	}
}