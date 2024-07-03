//using System;
//using System.Data;
//using System.Data.Common; 
//using System.Collections;
//using SoftMarketing.Model;
//using SoftMarketing.Service;

//namespace SoftMarketing.WebAPI{
	
//	public class ClientmessagehistoryController {
		
//		public ClientmessagehistoryController(){
//		}
		
		
//		/// <summary>
//		/// Inserts a <see cref="Clientmessagehistory"/> object into the datasource.
//		/// </summary>
//		/// <param name="entity">The <see cref="Clientmessagehistory"/> object to insert.</param>
//		/// <remarks>
//		/// 	
//		/// </remarks>
//		/// <returns></returns>
		
//		public virtual Int32 Add(ClientMessageHistory entity)
//		{
//			ClientMessageHistoryService clientMessageHistoryService = new ClientMessageHistoryService();
//			return clientMessageHistoryService.Add(entity);
//		}
//		public virtual void Add(List<ClientMessageHistory> entityCollection)	{
//			foreach (ClientMessageHistory entity in entityCollection){
//					Add(entity);
//			}
//		}
//		public virtual void Update(List<ClientMessageHistory> entityCollection){
//			foreach (ClientMessageHistory entity in entityCollection)	{
//				Update(entity);
//			}
//		}
//		public virtual Int32 Update(ClientMessageHistory entity)
//		{
//			ClientMessageHistoryService clientMessageHistoryService=new ClientMessageHistoryService();
//			return clientMessageHistoryService.Update(entity);
//		}
//		public virtual void Delete( Int32 messagehistoryid) {
//			ClientMessageHistoryService clientMessageHistoryService = new ClientMessageHistoryService();
//			clientMessageHistoryService.Delete(messagehistoryid);
//		}
//		public virtual ClientMessageHistory Get(Int32 messagehistoryid)
//		{
//			ClientMessageHistoryService clientMessageHistoryService = new ClientMessageHistoryService();
//			return clientMessageHistoryService.GetByID(messagehistoryid);
//		}
//		public virtual IEnumerable<ClientMessageHistory> GetByClientid(Int32 clientid)
//		{
//			ClientMessageHistoryService clientMessageHistoryService = new ClientMessageHistoryService();
//			return clientMessageHistoryService.GetByClientId(clientid);
//		}
//		public virtual IEnumerable<ClientMessageHistory> GetByMessageId(Int32 messageid)
//		{
//			ClientMessageHistoryService clientMessageHistoryService = new ClientMessageHistoryService();
//			return clientMessageHistoryService.GetByMessageId(messageid);
			
//		}
//	}
//}