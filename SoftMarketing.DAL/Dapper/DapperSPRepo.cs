﻿using Dapper;
using SoftMarketing.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.DAL.Dapper
{
    public class DapperSProcRepo : DataAccessBase
    {

        //private IConfiguration _configuration { get; set; }

        public DapperSProcRepo(/*IConfiguration configuration*/)
        {
            //_configuration = configuration;
            //ConnectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public string ConnectionString
        {
            get; set;
        }


        public void Execute(string name)
        {
            Execute(name, null);
        }


        public void Execute(string name, object param)
        {
            //using (var cnn = new SqlConnection(ConnectionString))
            //{
            Connection.Execute(name, param, commandType: CommandType.StoredProcedure);
            //}
        }




        public T Single<T>(string name, int id)
        {
            return Single<T>(name, new { id });
        }
        public T Single<T>(string name, object param)
        {
            var result = Connection.Query<T>(name, param, commandType: CommandType.StoredProcedure);

            if (result != null)
                return result.FirstOrDefault();

            return default(T);
        }




        public List<T> List<T>(string name, int id)
        {
            return List<T>(name, new { id });
        }

        public List<T> List<T>(string name, object param)
        {

            var result = Connection.Query<T>(name, param, commandType: CommandType.StoredProcedure);

            if (result != null)
                return result.ToList();

            return new List<T>();
        }




        public Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string name, object param)
        {
            var result = Connection.QueryMultiple(name, param, commandType: CommandType.StoredProcedure);

            var item1 = result.Read<T1>().ToList();
            var item2 = result.Read<T2>().ToList();



            if (item1 != null && item2 != null)
                return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(item1, item2);

            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(new List<T1>(), new List<T2>());
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> List<T1, T2, T3>(string name, object param)
        {
            var result = Connection.QueryMultiple(name, param, commandType: CommandType.StoredProcedure);

            var item1 = result.Read<T1>().ToList();
            var item2 = result.Read<T2>().ToList();
            var item3 = result.Read<T3>().ToList();

            if (item1 != null && item2 != null && item3 != null)
                return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(item1, item2, item3);

            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(new List<T1>(), new List<T2>(), new List<T3>());
        }



        public List<T> List<T>(string name)
        {
            var result = Connection.Query<T>(name, commandType: CommandType.StoredProcedure);

            if (result != null)
                return result.ToList();
            return new List<T>();
        }


        public void QueryExecute(string name, object param)
        {
            Connection.Execute(name, param, commandType: CommandType.Text);
        }

        public void QueryExecute(string name)
        {
            Connection.Execute(name, null, commandType: CommandType.Text);
        }





    }
}
