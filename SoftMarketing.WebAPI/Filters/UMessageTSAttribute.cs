using Microsoft.AspNetCore.Mvc.Filters;
using SoftMarketing.Model.SalesModels;
using SoftMarketing.Services.Sales;

namespace SoftMarketing.WebAPI.Filters
{
    public class UMessageTSAttribute : ActionFilterAttribute
    {
        private IUserService _UserService { set; get; }
        public UMessageTSAttribute()
        {
            _UserService = new UserService();
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.HttpContext.Response.StatusCode == 200)
            {
                var user = (User)context.HttpContext.Items["User"];
                //user.last_update_template = DateTime.UtcNow.ToString("yyyy-MM-dd HH:MM:ss.fff");
                DateTime startTime = DateTime.ParseExact("07/08/2023", "MM/dd/yyyy", null);
                DateTime endTime = DateTime.Now;   // Replace with your end time
                TimeSpan elapsedTime = endTime - startTime;
                _UserService.SetMessageTS(user.id, elapsedTime.ToString());
            }
        }
    }
}
