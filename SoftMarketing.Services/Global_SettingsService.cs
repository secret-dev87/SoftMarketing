using System;
using System.Data;
using System.Data.Common; 
using System.Collections;
using SoftMarketing.Model;
using SoftMarketing.DAL;

namespace SoftMarketing.Service
{
	
	public class Global_SettingsService
	{
		Global_SettingsDAL Global_SettingsDAL { get; set; }
		public Global_SettingsService()
		{
			Global_SettingsDAL = new Global_SettingsDAL();
		}

		public Int32 Add(Global_Settings setting, DbTransaction transaction)
		{
			return Global_SettingsDAL.Add(setting, transaction);
		}

		public string Update(Global_Settings setting, DbTransaction transaction)
		{
			return Global_SettingsDAL.Update(setting, transaction);	
		}

		public void Delete(Int32 name, DbTransaction transaction)
		{
			Global_SettingsDAL.Delete(name, transaction);
		}

		public IEnumerable<Global_Settings> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			return Global_SettingsDAL.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn, connectionString);


		}

		public Global_Settings Get(Int32 name, string connectionString = null)
		{
			return Global_SettingsDAL.Get(name, connectionString);
		}

		public Int32 CountAll(string connectionString = null)
		{
			return Global_SettingsDAL.CountAll(connectionString);
		}

		public IEnumerable<Global_Settings> GetAll(string connectionString = null)
		{
			return Global_SettingsDAL.GetAll(connectionString);
		}

		public IEnumerable<Global_Settings> GetByPk(Int32 name, string connectionString = null)
		{
			return Global_SettingsDAL.GetByPk(name, connectionString);	
		}

	}
}