using System;
using System.Data;
using System.Data.Common; 
using System.Collections;
using SoftMarketing.Model;
using SoftMarketing.DAL;

namespace SoftMarketing.Service
{
	
	public class Translations_WinFormsService
	{
		Translations_WinFormsDAL Translations_WinFormsDAL { get; set; }
		public Translations_WinFormsService()
		{
			Translations_WinFormsDAL = new Translations_WinFormsDAL();
		}

		public Int32 Add(Translation Translation, DbTransaction transaction)
		{
			return Translations_WinFormsDAL.Add(Translation, transaction);
		}

		public string Update(Translation Translation, DbTransaction transaction)
		{
			return Translations_WinFormsDAL.Update(Translation, transaction);
		}

		public void Delete(string english, DbTransaction transaction)
		{
			Translations_WinFormsDAL.Delete(english, transaction);
		}

		public IEnumerable<Translation> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			return Translations_WinFormsDAL.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn, connectionString);
		}

		public Translation Get(String english, string connectionString = null)
		{
			return Translations_WinFormsDAL.Get(english, connectionString);
		}

		public Int32 CountAll(string connectionString = null)
		{
			return Translations_WinFormsDAL.CountAll(connectionString);
		}

		public IEnumerable<Translation> GetAll(string connectionString = null)
		{
			return Translations_WinFormsDAL.GetAll(connectionString);
		}

		public IEnumerable<Translation> GetByPk(string english, string connectionString = null)
		{
			return Translations_WinFormsDAL.GetByPk(english, connectionString);
		}
	}
}