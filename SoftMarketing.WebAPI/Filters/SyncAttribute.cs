//using Microsoft.AspNetCore.Mvc.Filters;
//using SoftMarketing.Model.SalesModels;
//using SoftMarketing.WebAPI.Helpers;
//using Microsoft.AspNetCore.Mvc;
//using System.Text.Json;
//using SoftMarketing.Services.Sales;

//namespace SoftMarketing.WebAPI.Filters
//{
//    public class SyncAttribute : ActionFilterAttribute
//    {
//        public override void OnActionExecuting(ActionExecutingContext context)
//        {
//            //SyncData(context);
//        }

//        public override void OnActionExecuted(ActionExecutedContext context)
//        {
//            //context.HttpContext.Request.EnableBuffering();

//            //using (var swapStream = new MemoryStream())
//            //{
//            //    var originalResponseBody = context.HttpContext.Response.Body;

//            //    context.HttpContext.Response.Body = swapStream;


//            //    swapStream.Seek(0, SeekOrigin.Begin);
//            //    string responseBody = new StreamReader(swapStream).ReadToEnd();
//            //    swapStream.Seek(0, SeekOrigin.Begin);

//            //    swapStream.CopyToAsync(originalResponseBody);
//            //    context.HttpContext.Response.Body = originalResponseBody;
//            //}




//            //context.HttpContext.Response.WriteAsync("Mohammad :" + "Mousa");
//            //SyncData(context);
//        }
       
//        //public void UpdateLastAction(ActionExecutedContext context)
//        //{

//        //    context.HttpContext.Request.Headers.Remove("last_update_customer");
//        //    context.HttpContext.Response.Headers.Add("last_update_customer", DateTime.Now.ToString());
//        //}

//        public void SyncData(ActionExecutedContext context)
//        {
//            var last_update_customer = context.HttpContext.Request.Headers["last_update_customer"].FirstOrDefault();
//            var last_update_message = context.HttpContext.Request.Headers["last_update_message"].FirstOrDefault();
//            var last_update_setting = context.HttpContext.Request.Headers["last_update_setting"].FirstOrDefault();
//            var last_update_template = context.HttpContext.Request.Headers["last_update_template"].FirstOrDefault();
//            if (last_update_customer != null || last_update_message != null || last_update_setting != null || last_update_template != null)
//            {
//                var user = (User)context.HttpContext.Items["User"];
//                List<string> tables = new List<string>();
//                if (last_update_customer != null)
//                {
//                    if (Convert.ToDateTime(last_update_customer) != user.last_update_customer)
//                    {
//                        tables.Add("customer");
//                    }
//                }
//                if (last_update_message != null)
//                {
//                    if (Convert.ToDateTime(last_update_message) != user.last_update_message)
//                    {
//                        tables.Add("message"); 
//                    }
//                }
//                if (last_update_setting != null)
//                {
//                    if (Convert.ToDateTime(last_update_setting) != user.last_update_setting)
//                    {
//                        tables.Add("setting"); 
//                    }
//                }
//                if (last_update_template != null)
//                {
//                    if (Convert.ToDateTime(last_update_template) != user.last_update_template)
//                    {
//                        tables.Add("template"); 
//                    }
//                }
//                var syncedData = new UserService().Sync(user, tables);
//                throw new NotSynced(syncedData);
//            }
//        }
//    }
//}
