using System;
using System.Data;
using System.Data.Common; 
using System.Collections;
using SoftMarketing.DAL;
using SoftMarketing.Model;

namespace SoftMarketing.Service
{
	
	public class ClientService
	{
		
		ClientDAL clientDAL { get; set; }
		public ClientService(){
			clientDAL = new ClientDAL();
		}

		public Int32 Add(Client entity, DbTransaction dbTransaction = null)
		{
			return clientDAL.Add(entity, dbTransaction);
		}
		public int AddMultiple(List<Client> entityCollection, DbTransaction dbTransaction = null)
		{
			foreach (var item in entityCollection)
			{
				Add(item, dbTransaction);
			}
			return entityCollection.Count();
		}

		public Int32 Update(Client entity, DbTransaction dbTransaction = null)
		{
			return clientDAL.Update(entity, dbTransaction);
		}
		public int UpdateMultiple(List<Client> entityCollection, DbTransaction dbTransaction = null)
		{
			foreach (var item in entityCollection)
			{
				Update(item, dbTransaction);
			}
			return entityCollection.Count();
		}

		public void Delete(int clientId, DbTransaction dbTransaction = null)
		{
			clientDAL.Delete(clientId, dbTransaction);
		}

		public IEnumerable<Client> GetByClientID(Int32 Id)
		{
			return clientDAL.GetByClientID(Id);
		}

		public IEnumerable<Client> GetByCustomerId(Int32 customerId)
		{
			return clientDAL.GetByCustomerId(customerId);
		}
	}
}