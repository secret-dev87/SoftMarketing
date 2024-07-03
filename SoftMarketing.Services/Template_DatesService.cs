using System;
using System.Data;
using System.Data.Common; 
using System.Collections;
using SoftMarketing.DAL;
using SoftMarketing.Model;

namespace SoftMarketing.Service
{
	
	public class Template_DatesService
	{
		Template_DatesDAL Template_DatesDAL { get; set; }
		public Template_DatesService()
		{
			Template_DatesDAL = new Template_DatesDAL();
		}

		public Int32 Add(Template_Dates Template_Dates, DbTransaction transaction)
		{
			return Template_DatesDAL.Add(Template_Dates, transaction);
		}

		public Int32 Update(Template_Dates Template_Dates, DbTransaction transaction)
		{
			return Template_DatesDAL.Update(Template_Dates, transaction);
		}

		public void Delete(Int32 id, DbTransaction transaction)
		{
			Template_DatesDAL.Delete(id, transaction);
		}

		public IEnumerable<Template_Dates> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			return Template_DatesDAL.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn, connectionString);
		}
		

		public Template_Dates Get(Int32 id, string connectionString = null)
		{
			return Template_DatesDAL.Get(id, connectionString);
		}

		public Int32 CountAll(string connectionString = null)
		{
			return Template_DatesDAL.CountAll(connectionString);
		}

		public IEnumerable<Template_Dates> GetAll(string connectionString = null)
		{
			return Template_DatesDAL.GetAll(connectionString);
		}
		public IEnumerable<Template_Dates> GetByPk(Int32 id, string connectionString = null)
		{
			return Template_DatesDAL.GetByPk(id, connectionString);
		}

	}
}