using Quartz;
using SoftMarketing.Services.Sales;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SoftMarketing.Jobs.Tasks
{
    public class InsertMessagesTask : IJob
    {
        //private IUserService UserService;
        //public InsertMessagesTask()
        //{
        //    if(UserService == null)
        //    UserService = new UserService();
        //}
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                //var dataMap = context.JobDetail.JobDataMap;
                //var timeRequested = dataMap.GetDateTime("Current Date Time");
                //var ticketsNeeded = dataMap.GetInt("Tickets needed");
                //var concertName = dataMap.GetString("Concert Name");

                var user = new UserService().GetSalesUserAndRefreshTokensByUserId(1);
                Debug.WriteLine($"InsertMessagesTask");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Task.FromResult(0);
        }
    }
}
