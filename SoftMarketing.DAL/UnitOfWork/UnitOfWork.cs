using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.DAL.UnitOfWork
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private DbTransaction _transaction;
        private DbConnection _connection;
        private Dictionary<string, dynamic> _repositories;

        public UnitOfWork(bool withTransaction = false)
        {
            _connection = ConnectionFactory.CreateConnection();
            _connection.Open();
            if (withTransaction)
            {
                _transaction = _connection.BeginTransaction();
            }
            _repositories = new Dictionary<string, dynamic>();
        }

        public void SaveChanges()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction have already been commited. Check your transaction handling.");

            _transaction.Commit();
            _transaction = null;
        }
        public IDataAccessBase Repository<T>() where T : class, new()//, IDataAccessBase,
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(T).Name;

            if (_repositories.ContainsKey(type))
            {
                return ((IDataAccessBase)_repositories[type]); ;
            }

            var repositoryType = typeof(T);
            _repositories.Add(type, Activator.CreateInstance(repositoryType));
            ((IDataAccessBase)_repositories[type]).Connection = _connection;
            ((IDataAccessBase)_repositories[type]).DbTransaction = _transaction;
            return _repositories[type];
        }

        public IDataAccessBase GenericRepository(Type T)
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
            }

            var type = T.Name;

            if (_repositories.ContainsKey(type))
            {
                return ((IDataAccessBase)_repositories[type]); ;
            }

            var repositoryType = T;
            _repositories.Add(type, Activator.CreateInstance(repositoryType));
            ((IDataAccessBase)_repositories[type]).Connection = _connection;
            return _repositories[type];
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();

                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
            }
        }
    }
}
