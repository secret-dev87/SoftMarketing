using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using SoftMarketing.Model;
using Microsoft.AspNetCore.Mvc;
using SoftMarketing.WebAPI.Core;
using System.Net;
using SoftMarketing.Services.Marketing;
using SoftMarketing.Model.SalesModels;
using SoftMarketing.WebAPI.Security;
using SoftMarketing.WebAPI.Filters;
using SoftMarketing.WebAPI.Helpers;
using Microsoft.AspNetCore.SignalR;

namespace SoftMarketing.WebAPI.Controllers.MarketingControllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ApiControllerBase
    {
        MessageService MessageService { get; set; }
        private readonly IHubContext<ServerHub> _hubContext;
        public MessageController(IHubContext<ServerHub> hubContext)
        {
            MessageService = new MessageService();
            _hubContext = hubContext;
        }

        [HttpGet("GetAllScheduledMessages")]
        public IActionResult GetAllScheduledMessages()
        {
            var response = new Response<SchedulMessage>();
            try
            {
                var user = (User)HttpContext.Items["User"];
                var result = MessageService.GetAllScheduledMessages(user.id);
                response.IsSuccess = true;
                response.Items = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("GetTodayScheduledMessages")]
        public IActionResult GetTodayScheduledMessages()
        {
            var response = new Response<TodayScheduledMessages>();
            try
            {
                var user = (User)HttpContext.Items["User"];
                var result = MessageService.GetTodayScheduledMessages(user.id);
                response.IsSuccess = true;
                response.Items = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
        }


        [HttpPost("updateUserMessage")]
        [UMessageTS]
        public IActionResult UpdateUserMessage(SchedulMessage schedulMessage)
        {
            var response = new Response<SchedulMessage>();
            try
            {

                var user = (User)HttpContext.Items["User"];
                schedulMessage.sales_userId = user.id;
                var result = MessageService.UpdateUserMessage(schedulMessage);
                if(result > 0)
                {
                    response.IsSuccess = true;
                    response.Item = schedulMessage;
                    response.SuccessMessage = "Schedual message updated";
                    try
                    {
                        new ServerHub(null, _hubContext).UpdateClientData("update_schedual_message", user, schedulMessage);
                    }
                    catch (Exception)
                    {
                    }
                    return Ok(response);
                }
                else
                {
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = Helper.GetProperMessage(ex.Message, "Message");
                return BadRequest(response);
            }

        }

        [HttpGet("updateSentFlag")]
        [UMessageTS]
        public IActionResult UpdateSentFlag(string str)
        {
            var response = new Response<string>();
            try
            {
                var YesNoLists = GetYesNoList(str);
                var user = (User)HttpContext.Items["User"];
                int result = 0;
                if (!string.IsNullOrEmpty(YesNoLists.Item1))
                {
                     result = MessageService.UpdateSentFlag(YesNoLists.Item1, user.id,1);
                }
                //if (!string.IsNullOrEmpty(YesNoLists.Item2))
                //{
                //    result = MessageService.UpdateSentFlag(YesNoLists.Item1, user.id, 0);
                //}
                if(result > 0)
                {
                    response.IsSuccess = true;
                    response.SuccessMessage = "Schedual message updated";
                    try
                    {
                        new ServerHub(null, _hubContext).UpdateClientData("update_sent_message", user, str);
                    }
                    catch (Exception)
                    {
                    }
                    return Ok(response);
                }
                else
                {
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = Helper.GetProperMessage(ex.Message, "Message");
                return BadRequest(response);
            }
        }

        private Tuple<string,string> GetYesNoList(string input)
        {
            //string input = "2:no,1:yes,61:no,100:no";
            var pairs = input.Split(',');

            var yesList = new List<int>();
            var noList = new List<int>();

            foreach (string pair in pairs)
            {
                string[] parts = pair.Split(':');
                if (parts.Length == 2)
                {
                    int number;
                    if (int.TryParse(parts[0], out number))
                    {
                        if (parts[1] == "yes")
                        {
                            yesList.Add(number);
                        }
                        else if (parts[1] == "no")
                        {
                            noList.Add(number);
                        }
                    }
                }
            }
            string yesListString = string.Join(", ", yesList);
            string noListString = string.Join(", ", noList);
            return Tuple.Create(yesListString, noListString);
        }

        [HttpPost("insertUserMessage")]
        [UMessageTS]
        public IActionResult InsertUserMessage(SchedulMessage message)
        {
            var response = new Response<SchedulMessage>();
            try
            {
                var user = (User)HttpContext.Items["User"];
                message.sales_userId = user.id;
                var result = MessageService.InsertUserMessage(message);
                response.IsSuccess = true;
                response.Item = result;
                response.SuccessMessage = "Schedual message inserted";
                try
                {
                    new ServerHub(null, _hubContext).UpdateClientData("add_schedual_message", user, result);
                }
                catch (Exception)
                {
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = Helper.GetProperMessage(ex.Message, "Message");
                return BadRequest(response);
            }
        }


        [HttpDelete("deleteUserMessage")]
        [UMessageTS]
        public IActionResult DeleteUserMessage(long uMessageId)
        {
            var response = new Response<SchedulMessage>();
            try
            {
                var user = (User)HttpContext.Items["User"];
                var result = MessageService.DeleteUserMessage(uMessageId, user.id);
                if(result > 0)
                {
                    response.IsSuccess = true;
                    response.SuccessMessage = "Schedual message deleted";
                    try
                    {
                        new ServerHub(null, _hubContext).UpdateClientData("delete_schedual_message", user, result);
                    }
                    catch (Exception)
                    {
                    }
                    return Ok(response);
                }
                else
                {
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
        }

    }
}