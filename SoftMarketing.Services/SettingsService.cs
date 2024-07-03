using System;
using System.Data;
using System.Data.Common; 
using System.Collections;
using SoftMarketing.DAL;
using SoftMarketing.Model;

namespace SoftMarketing.Service
{
	
	public class SettingsService
	{
		SettingsDAL SettingsDAL { get; set; }
		public SettingsService()
		{
			SettingsDAL = new SettingsDAL();
		}

		public Int32 Add(Settings Settings, DbTransaction transaction)
		{
			return SettingsDAL.Add(Settings, transaction);
		}

		public Int32 Update(Settings Settings, DbTransaction transaction)
		{
			return SettingsDAL.Update(Settings, transaction);
		}

		public void Delete(Int32 id, DbTransaction transaction)
		{
			SettingsDAL.Delete(id, transaction);
		}


		public IEnumerable<Settings> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			return SettingsDAL.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn, connectionString);
		}

		public Settings Get(Int32 id, string connectionString = null)
		{
			return SettingsDAL.Get(id, connectionString);
		}

		public Int32 CountAll(string connectionString = null)
		{
			return SettingsDAL.CountAll(connectionString);
		}

		public IEnumerable<Settings> GetAll(string connectionString = null)
		{
			return SettingsDAL.GetAll(connectionString);
		}

		public IEnumerable<Settings> GetByPk(Int32 id, string connectionString = null)
		{
			return SettingsDAL.GetByPk(id, connectionString);
		}
	}
}