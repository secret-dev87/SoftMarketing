using Quartz;
using System.Diagnostics;

namespace SoftMarketing.Jobs.Tasks
{
    public class RemoveCustomersTask : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                Debug.WriteLine($"RemoveCustomersTask");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Task.FromResult(0);
        }
    }
}
