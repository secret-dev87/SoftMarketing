using System;
using System.Data;
using System.Data.Common; 
using System.Collections;
using SoftMarketing.DAL;
using SoftMarketing.Model;

namespace SoftMarketing.Service
{
	
	public class MessegingAppService
	{
		MessegingAppDAL MessegingAppDAL { get; set; }
		public MessegingAppService()
		{
			MessegingAppDAL = new MessegingAppDAL();
		}

		public Int32 Add(MessegingApp MessegingApp, DbTransaction transaction)
		{
			return MessegingAppDAL.Add(MessegingApp, transaction);
		}

		public Int32 Update(MessegingApp MessegingApp, DbTransaction transaction)
		{
			return MessegingAppDAL.Update(MessegingApp, transaction);
		}

		public void Delete(Int32 trial_id_1, DbTransaction transaction)
		{
			MessegingAppDAL.Delete(trial_id_1, transaction);
		}

		public IEnumerable<MessegingApp> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			return MessegingAppDAL.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn, connectionString);
		}
	
		public IEnumerable<MessegingApp> Get(Int32 trial_id_1, string connectionString = null)
		{
			return MessegingAppDAL.Get(trial_id_1 , connectionString);
		}

		public Int32 CountAll(string connectionString = null)
		{
			return MessegingAppDAL.CountAll(connectionString);
		}

		public IEnumerable<MessegingApp> GetAll(string connectionString = null)
		{
			return MessegingAppDAL.GetAll(connectionString);
		}

		public IEnumerable<MessegingApp> GetByPk(Int32 trial_id_1, string connectionString = null)
		{
			return MessegingAppDAL.GetByPk(trial_id_1, connectionString);
		}
	}
}