using System;
using System.Data;
using System.Data.Common; 
using System.Collections;
using SoftMarketing.DAL;
using SoftMarketing.Model;

namespace SoftMarketing.Service
{
	
	public class User_MessageService
	{
		User_MessageDAL user_MessageDAL { get; set; }
		public User_MessageService(){
			user_MessageDAL = new User_MessageDAL();
		}

		public Int32 Add(User_Message user_Message, DbTransaction transaction = null)
		{
			return user_MessageDAL.Add(user_Message, transaction);
		}
		public void AddMultiple(List<User_Message> entityCollection, DbTransaction dbTransaction = null)
		{
			foreach (var item in entityCollection)
			{
				Add(item, dbTransaction);
			}
		}

		public Int32 Update(User_Message user_Message, DbTransaction transaction = null)
		{
			return user_MessageDAL.Update(user_Message, transaction);
		}
		public void UpdateMultiple(List<User_Message> entityCollection, DbTransaction dbTransaction = null)
		{
			foreach (var item in entityCollection)
			{
				Update(item, dbTransaction);
			}
		}
		public void Delete(Int32 uMId, DbTransaction transaction = null)
		{
			user_MessageDAL.Delete(uMId, transaction);
		}

		public IEnumerable<User_Message> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			return user_MessageDAL.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn, connectionString);
		}

		public User_Message Get(Int32 uMId, string connectionString = null)
		{
			return user_MessageDAL.Get(uMId, connectionString);
		}

		public IEnumerable<User_Message> GetAll(string connectionString = null)
		{
			return user_MessageDAL.GetAll(connectionString);
		}

		public Int32 CountAll(string connectionString = null)
		{
			return user_MessageDAL.CountAll(connectionString);
		}

		public IEnumerable<User_Message> GetById(Int32 umId, string connectionString = null)
		{
			return user_MessageDAL.GetByPk(umId, connectionString);
		}

	}
}