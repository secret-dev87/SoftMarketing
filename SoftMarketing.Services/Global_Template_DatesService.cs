using System;
using System.Data;
using System.Data.Common; 
using System.Collections;
using SoftMarketing.Model;
using SoftMarketing.DAL;

namespace SoftMarketing.Service
{
	
	public class Global_Template_DatesService
	{
		Global_Template_DatesDAL Global_Template_DatesDAL { set; get; }
		public Global_Template_DatesService()
		{
			Global_Template_DatesDAL = new Global_Template_DatesDAL();
		}
		public Int32 Add(Template_Dates Template_Dates, DbTransaction transaction)
		{
			return Global_Template_DatesDAL.Add(Template_Dates, transaction);
		}
		public Int32 Update(Template_Dates Template_Dates, DbTransaction transaction)
		{
			
			return Global_Template_DatesDAL.Update(Template_Dates,transaction);
		}


		public void Delete(Int32 ID, DbTransaction transaction)
		{
			Global_Template_DatesDAL.Delete(ID, transaction);
		}

		public IEnumerable<Template_Dates> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			return Global_Template_DatesDAL.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn, connectionString);
		}

		
		public IEnumerable<Template_Dates> Get(Int32 ID, string connectionString = null)
		{
			return Global_Template_DatesDAL.Get(ID, connectionString);
		}

		public Int32 CountAll(string connectionString = null)
		{
			return Global_Template_DatesDAL.CountAll(connectionString);
		}

		public IEnumerable<Template_Dates> GetAll(string connectionString = null)
		{
			return Global_Template_DatesDAL.GetAll(connectionString);
		}

		public IEnumerable<Template_Dates> GetByPk(Int32 ID, string connectionString = null)
		{
			return Global_Template_DatesDAL.GetByPk(ID, connectionString);
		}
	}
}