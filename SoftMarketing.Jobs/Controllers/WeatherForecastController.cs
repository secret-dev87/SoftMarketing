using Microsoft.AspNetCore.Mvc;
using Quartz;
using SoftMarketing.Jobs.Tasks;

namespace SoftMarketing.Jobs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IScheduler _scheduler;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IScheduler scheduler)
        {
            _logger = logger;
            _scheduler = scheduler;
        }

        [HttpPost("runJobs")]
        public async Task<string> RunJobs()
        {
            RunInsertMessagesTask(); 
            RunRemoveCustomersTask();
            return "kkk";
        }
        private async void RunInsertMessagesTask()
        {
            ITrigger trigger = TriggerBuilder.Create()
             .WithIdentity($"InsertMessagesTask Trigger")
             .StartAt(new DateTimeOffset(DateTime.Now.AddSeconds(5)))
             .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever())
             .WithPriority(1)
             .Build();

            IDictionary<string, object> map = new Dictionary<string, object>()
            {
                {"Current Date Time", $"{DateTime.Now}" },
                {"Tickets needed", $"5" },
                {"Concert Name", $"Rock" }
            };

            IJobDetail job = JobBuilder.Create<InsertMessagesTask>()
                        .WithIdentity("InsertMessagesTask JobDetail")
                        .SetJobData(new JobDataMap(map))
                        .Build();

             _scheduler.ScheduleJob(job, trigger);
            await _scheduler.Start();
        }
        private async void RunRemoveCustomersTask()
        {
            ITrigger trigger = TriggerBuilder.Create()
             .WithIdentity($"RemoveCustomersTask Trigger")
             .StartAt(new DateTimeOffset(DateTime.Now.AddSeconds(5)))
             .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever())
             .WithPriority(1)
             .Build();

            IDictionary<string, object> map = new Dictionary<string, object>()
            {
                {"Current Date Time", $"{DateTime.Now}" },
                {"Tickets needed", $"5" },
                {"Concert Name", $"Rock" }
            };

            IJobDetail job = JobBuilder.Create<RemoveCustomersTask>()
                        .WithIdentity("RemoveCustomersTask JobDetail")
                        .SetJobData(new JobDataMap(map))
                        .Build();

             _scheduler.ScheduleJob(job, trigger);
            await _scheduler.Start();
        }


    }
}