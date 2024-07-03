using System;
using System.Data;
using System.Data.Common;
using SoftMarketing.Service;
using SoftMarketing.Model;

namespace SoftMarketing.WebAPI{
	
	public class Client_MessegingAppsController
	{
		
		public Client_MessegingAppsController(){
		}

		/// <summary>
		/// Inserts a <see cref="Cc_messegingapps"/> object into the datasource.
		/// </summary>
		/// <param name="entity">The <see cref="Cc_messegingapps"/> object to insert.</param>
		/// <remarks>
		/// 	
		/// </remarks>
		/// <returns></returns>
		
		public virtual Int32 Add(Client_MessegingApps entity)
        {
			Client_MessegingAppsService cC_MessegingAppsService = new();
			return cC_MessegingAppsService.AddMessagingApp(entity);
		}
		public void Add(List<Client_MessegingApps> entityCollection)	{
			Client_MessegingAppsService cC_MessegingAppsService = new();
			cC_MessegingAppsService.AddMultiple(entityCollection);
		}
		
		
		public virtual void Update(List<Client_MessegingApps> entityCollection) {
			foreach (Client_MessegingApps entity in entityCollection)	{
				Update(entity);
			}
		}
		
		public virtual Int32 Update(Client_MessegingApps entity)
		{
			Client_MessegingAppsService cC_MessegingAppsService = new();
			return cC_MessegingAppsService.Update(entity);
		}

		public virtual void Delete(int id)
		{
			Client_MessegingAppsService cC_MessegingAppsService =new();
			cC_MessegingAppsService.Delete(id);
		}
		public Client_MessegingApps Get(Int32 id)
		{
			Client_MessegingAppsService cC_MessegingAppsService=new();
			return cC_MessegingAppsService.GetByID(id);
		}
		public virtual IEnumerable<Client_MessegingApps> GetByClientId(Int32 ClientId)
		{
			Client_MessegingAppsService cC_MessegingAppsService = new();
			return cC_MessegingAppsService.GetByClientid(ClientId);
		}
		public virtual IEnumerable<Client_MessegingApps> GetByMessegingappid(Int32 messegingappid)
		{
			Client_MessegingAppsService cC_MessegingAppsService = new();
			return cC_MessegingAppsService.GetByMessegingappid(messegingappid);
		}
		
	}
}