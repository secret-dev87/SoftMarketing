using SoftMarketing.DAL.DataAccess.MarketingDAL;
using SoftMarketing.DAL.UnitOfWork;
using SoftMarketing.Model.MarketingModels;
using SoftMarketing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftMarketing.Services.Sales;

namespace SoftMarketing.Service.Marketing
{
    public class CustomerService
    {
        private IUserService UserService { get; set; }
        public CustomerService(IUserService userService = null)
        {
            if (userService != null)
                UserService = userService;
            else
                UserService = new UserService();
        }
        public Customer Add(Customer customer, int userId)
        {
            string id = string.Empty;
            using (var unitOfWork = new UnitOfWork(true))
            {
                CustomerDAL CustomerDAL = (CustomerDAL)unitOfWork.Repository<CustomerDAL>();
                var currentDate = DateTime.UtcNow;
                customer.added = currentDate;
                customer.last_reminder = currentDate;
                customer = CustomerDAL.Add(customer);
                UserService.UpdateLastUpdateCustomer(userId, unitOfWork);
                unitOfWork.SaveChanges();
            }
            return customer;
        }

        public int Update(Customer customer, int userId)
        {
            int numOfEffectedRows = -1;
            using (var unitOfWork = new UnitOfWork(true))
            {
                CustomerDAL CustomerDAL = (CustomerDAL)unitOfWork.Repository<CustomerDAL>();
                numOfEffectedRows = CustomerDAL.Update(customer);
                UserService.UpdateLastUpdateCustomer(userId, unitOfWork);
                unitOfWork.SaveChanges();
            }
            return numOfEffectedRows;
        }
        public bool Delete(int id, int userId)
        {
            bool isDeleted= false;
            using (var unitOfWork = new UnitOfWork(true))
            {
                CustomerDAL CustomerDAL = (CustomerDAL)unitOfWork.Repository<CustomerDAL>();
                isDeleted = CustomerDAL.Delete(id, userId);
                UserService.UpdateLastUpdateCustomer(userId, unitOfWork);
                unitOfWork.SaveChanges();
            }
            return isDeleted;
        }
        public Customer Get(int id, int userId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                CustomerDAL CustomerDAL = (CustomerDAL)unitOfWork.Repository<CustomerDAL>();
                return CustomerDAL.Get(id, userId);
            }

        }
        public List<Customer> GetAll(int userId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                CustomerDAL CustomerDAL = (CustomerDAL)unitOfWork.Repository<CustomerDAL>();
                return CustomerDAL.GetAll(userId);
            }

        }
        public int GetLast(int userId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                CustomerDAL CustomerDAL = (CustomerDAL)unitOfWork.Repository<CustomerDAL>();
                return CustomerDAL.GetLastRecord(userId);
            }

        }
    }
}
