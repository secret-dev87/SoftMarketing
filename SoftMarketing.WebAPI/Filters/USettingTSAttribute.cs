using Microsoft.AspNetCore.Mvc.Filters;
using SoftMarketing.Model.SalesModels;
using SoftMarketing.Services.Sales;

namespace SoftMarketing.WebAPI.Filters
{
    public class USettingTSAttribute : ActionFilterAttribute
    {
        private IUserService _UserService { set; get; }
        public USettingTSAttribute()
        {
            _UserService = new UserService();
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.HttpContext.Response.StatusCode == 200)
            {
                var user = (User)context.HttpContext.Items["User"];
                user.last_update_setting = DateTime.UtcNow.ToString("yyyy-MM-dd HH:MM:ss.fff");
                if (_UserService.SetSettingTS(user.id, user.last_update_setting) == 1)
                {
                    context.HttpContext.Items["User"] = user;
                }
            }
        }
    }
}
