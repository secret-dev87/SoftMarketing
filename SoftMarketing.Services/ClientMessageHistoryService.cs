using System;
using System.Data;
using System.Data.Common; 
using System.Collections;
using SoftMarketing.DAL;
using SoftMarketing.Model;

namespace SoftMarketing.Service
{
	
	public class ClientMessageHistoryService
	{
		
		public ClientMessageHistoryService(){
		}



		/// <summary>
		/// Inserts a <see cref="Clientmessagehistory"/> object into the datasource.
		/// </summary>
		/// <param name="entity">The <see cref="Clientmessagehistory"/> object to insert.</param>
		/// <remarks>
		/// 	
		/// </remarks>
		/// <returns></returns>

		public virtual Int32 Add(ClientMessageHistory entity)
		{
			ClientMessageHistoryDAL clientMessageHistoryDAL = new ClientMessageHistoryDAL();
			return clientMessageHistoryDAL.Add(entity);
		}
		public virtual void Add(List<ClientMessageHistory> entityCollection)	{
			foreach (ClientMessageHistory entity in entityCollection){
					Add(entity);
			}
		}

		public virtual void Update(List<ClientMessageHistory> entityCollection) {
			foreach (ClientMessageHistory entity in entityCollection)	{
				Update(entity);
			}
		}
		public virtual Int32 Update(ClientMessageHistory entity)
		{
			ClientMessageHistoryService clientMessageHistoryService = new ClientMessageHistoryService();
			return clientMessageHistoryService.Update(entity);
		}		
		public virtual void Delete( Int32 messagehistoryid) {
			ClientMessageHistoryService clientMessageHistoryService = new ClientMessageHistoryService();
			clientMessageHistoryService.Delete(messagehistoryid);
		}

		public virtual ClientMessageHistory GetByID(Int32 messagehistoryid)
		{
			ClientMessageHistoryDAL clientMessageHistoryDAL= new ClientMessageHistoryDAL();
			return clientMessageHistoryDAL.GetByID(messagehistoryid);
		}

		public virtual IEnumerable<ClientMessageHistory> GetByClientId(Int32 clientid)
		{
			ClientMessageHistoryDAL clientMessageHistoryDAL	=	new ClientMessageHistoryDAL();
			return clientMessageHistoryDAL.GetByclientid(clientid);
		}
		public virtual IEnumerable<ClientMessageHistory> GetByMessageId(Int32 messageid)
		{
			ClientMessageHistoryDAL clientMessageHistoryDAL = new ClientMessageHistoryDAL();
			return clientMessageHistoryDAL.GetByMessageid(messageid);
		}
	}
}