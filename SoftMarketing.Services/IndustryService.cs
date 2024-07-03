using System;
using System.Data;
using System.Data.Common; 
using System.Collections;
using SoftMarketing.DAL;
using SoftMarketing.Model;

namespace SoftMarketing.Service{
	
	public class IndustryService
	{
		
		public IndustryService(){
		}

		/// <summary>
		/// Inserts a <see cref="Industry"/> object into the datasource.
		/// </summary>
		/// <param name="entity">The <see cref="Industry"/> object to insert.</param>
		/// <remarks>
		/// 	
		/// </remarks>
		/// <returns></returns>

		public Int32 AddMessagingApp(Industry entity, DbTransaction dbTransaction = null)
		{
			var	industryDAL=new IndustryDAL();
			return industryDAL.Add(entity,dbTransaction);
		}
		
		public void AddMultiple(List<Industry> entityCollection, DbTransaction transaction = null)	{
			foreach (Industry entity in entityCollection){
				var intval= AddMessagingApp(entity, transaction);
			}
		}
		public virtual void Update(List<Industry> entityCollection, DbTransaction transaction = null)
		{
			foreach (Industry entity in entityCollection)	{
				Update(entity, transaction);
			}
		}
		public Int32 Update(Industry entity, DbTransaction dbTransaction = null)
		{
			var industryDAL = new IndustryDAL();
			return industryDAL.Update(entity, dbTransaction);
		}
		public void Delete(int industryID, DbTransaction dbTransaction = null)
		{
			var industryDAL = new IndustryDAL();
			industryDAL.Delete(industryID, null);
		}

		public Industry GetByID(Int32 industryID)
		{
			var industryDAL = new IndustryDAL();
			return industryDAL.GetById(industryID);
		}
	}
}