using System;
using System.Data;
using System.Data.Common; 
using System.Collections;
using SoftMarketing.Model;
using SoftMarketing.DAL;

namespace SoftMarketing.Service
{
	
	public class MessageTypesService
	{
		MessageTypesDAL MessageTypesDAL { get; set; }
		public MessageTypesService()
		{
			MessageTypesDAL = new MessageTypesDAL();
		}

		public Int32 Add(MessageTypes MessageTypes, DbTransaction transaction)
		{
			return MessageTypesDAL.Add(MessageTypes, transaction);
		}

		public string Update(MessageTypes MessageTypes, DbTransaction transaction)
		{
			return MessageTypesDAL.Update(MessageTypes, transaction);
		}

		public void Delete(Int32 trial_messagetypeid_1, DbTransaction transaction)
		{
			MessageTypesDAL.Delete(trial_messagetypeid_1, transaction);


		}

		public IEnumerable<MessageTypes> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			return MessageTypesDAL.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn, connectionString);
		}


		public IEnumerable<MessageTypes> Get(Int32 trial_messagetypeid_1, string connectionString = null)
		{
			return MessageTypesDAL.Get(trial_messagetypeid_1, connectionString);
		}

		public Int32 CountAll(string connectionString = null)
		{
			return MessageTypesDAL.CountAll(connectionString);
		}

		public IEnumerable<MessageTypes> GetAll(string connectionString = null)
		{
			return MessageTypesDAL.GetAll(connectionString);
		}

		public IEnumerable<MessageTypes> GetByPk(Int32 trial_messagetypeid_1, string connectionString = null)
		{
			return MessageTypesDAL.GetByPk(trial_messagetypeid_1,connectionString);
		}

	}
}