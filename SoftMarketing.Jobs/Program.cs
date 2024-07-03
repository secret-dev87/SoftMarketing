using Quartz;
using SoftMarketing.Services.Sales;
using System.Collections.Specialized;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddSingleton(provider => GetScheduler().Result);
//builder.Services.AddScoped<IUserService, UserService>();

async Task<IScheduler> GetScheduler()
{
    //    var properties = new NameValueCollection
    //            {
    //              {"quartz.scheduler.instanceName", "DemoScheduler"},
    //{"quartz.scheduler.instanceId", "instance1"},
    //{"quartz.threadPool.type", "Quartz.Simpl.SimpleThreadPool, Quartz"},
    //{"quartz.threadPool.threadCount", "10"},
    //{"quartz.threadPool.threadPriority", "Normal"},
    //{"quartz.jobStore.misfireThreshold", "60000"},
    //{"quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz"},
    //{"quartz.jobStore.useProperties", "true"},
    //{"quartz.jobStore.dataSource", "default"},
    //{"quartz.jobStore.tablePrefix", "QRTZ_"},
    //{"quartz.jobStore.lockHandler.type", "Quartz.Impl.AdoJobStore.UpdateLockRowSemaphore, Quartz"},
    //{"quartz.dataSource.default.provider", "MySql"},
    //{"quartz.serializer.type", "binary"},
    //{"quartz.dataSource.default.connectionString", "Server=135.181.47.94;Port=3306;Database=soft_database;Uid=root;Pwd=Password123#;"}

    // };

    var properties = new NameValueCollection();

    // and override values via builder
    IScheduler scheduler = await SchedulerBuilder.Create(properties)
        //// default max concurrency is 10
        //.UseDefaultThreadPool(x => x.MaxConcurrency = 5)
        //// this is the default 
        //// .WithMisfireThreshold(TimeSpan.FromSeconds(60))
        //.UsePersistentStore(x =>
        //{
        //// force job data map values to be considered as strings
        //// prevents nasty surprises if object is accidentally serialized and then 
        //// serialization format breaks, defaults to false
        //    x.UseProperties = true;
        //    x.UseClustering();
        //// there are other SQL providers supported too 
        //    //x.UseMySql("Server=135.181.47.94;Port=3306;Database=soft_database;Uid=root;Pwd=Password123#;");
        //// this requires Quartz.Serialization.Json NuGet package
        //    x.UseJsonSerializer();
        //})


        .BuildScheduler();

    //await scheduler.Start();

    //var schedulerFactory = new StdSchedulerFactory(properties);
    //var scheduler = await schedulerFactory.GetScheduler();
    //await scheduler.Start();                                                                                  
    return scheduler;
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
