using System;
using System.Data;
using System.Data.Common; 
using System.Collections;
using SoftMarketing.DAL;
using SoftMarketing.Model;

namespace SoftMarketing.Service{
	
	public class Client_MessegingAppsService
	{
		
		public Client_MessegingAppsService(){
		}

		/// <summary>
		/// Inserts a <see cref="Cc_messegingapps"/> object into the datasource.
		/// </summary>
		/// <param name="entity">The <see cref="Cc_messegingapps"/> object to insert.</param>
		/// <remarks>
		/// 	
		/// </remarks>
		/// <returns></returns>

		public Int32 AddMessagingApp(Client_MessegingApps entity, DbTransaction dbTransaction = null)
		{
			var	messegingAppsDAL=new Client_MessegingAppsDAL();
			return messegingAppsDAL.Add(entity,dbTransaction);
		}
		
		public void AddMultiple(List<Client_MessegingApps> entityCollection, DbTransaction transaction = null)	{
			foreach (Client_MessegingApps entity in entityCollection){
				var intval= AddMessagingApp(entity, transaction);
			}
		}
		public virtual void Update(List<Client_MessegingApps> entityCollection, DbTransaction transaction = null)
		{
			foreach (Client_MessegingApps entity in entityCollection)	{
				Update(entity, transaction);
			}
		}
		public Int32 Update(Client_MessegingApps entity, DbTransaction dbTransaction = null)
		{
			var messegingAppsDAL = new Client_MessegingAppsDAL();
			return messegingAppsDAL.Update(entity, dbTransaction);
		}
		public void Delete(int id, DbTransaction dbTransaction = null)
		{
			var messegingAppDAL = new Client_MessegingAppsDAL();
			messegingAppDAL.Delete(id, null);
		}

		public Client_MessegingApps GetByID(Int32 id)
		{
			var messegingAppDAL = new Client_MessegingAppsDAL();
			return messegingAppDAL.GetById(id);
		}
		public IEnumerable<Client_MessegingApps> GetByClientid(Int32 clientid){
			var messegingAppDAL = new Client_MessegingAppsDAL();
			return messegingAppDAL.GetByclientid(clientid);
		}
		public IEnumerable<Client_MessegingApps> GetByMessegingappid(Int32 messegingappid){
			var messegingAppDAL = new Client_MessegingAppsDAL();
			return messegingAppDAL.GetByMessegingappid(messegingappid);
		}
	}
}