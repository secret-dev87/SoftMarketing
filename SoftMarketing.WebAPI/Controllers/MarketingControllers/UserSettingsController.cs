using Microsoft.AspNetCore.Mvc;
using SoftMarketing.Model;
using SoftMarketing.Model.MarketingModels;
using SoftMarketing.Model.SalesModels;
using SoftMarketing.Services.Marketing;
using SoftMarketing.WebAPI.Core;
using SoftMarketing.WebAPI.Security;

namespace SoftMarketing.WebAPI.Controllers.MarketingControllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class UserSettingsController : ApiControllerBase
	{
		private readonly UserSettingsService UserSettingsService;
		public UserSettingsController()
		{
			UserSettingsService = new();
		}
		[HttpGet("GetCategoriesWithTemplates")]
		public IActionResult GetCategoriesWithTemplates(int? categoryDetailId)
        {
			var response = new Response<UserCategory>();
			try
			{
				var user = (User)HttpContext.Items["User"];
				response.Items = UserSettingsService.GetCategoriesWithTemplates(user.id, categoryDetailId);
				response.IsSuccess = true;
				return Ok(response);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.ErrorMessage = ex.Message;
				return BadRequest(response);
			}
		}
		[HttpGet("GetUserSettings")]
		public IActionResult GetUserSettings()
		{
			var response = new Response<UserSettings>();
			try
			{
				var user = (User)HttpContext.Items["User"];
				response.Item = UserSettingsService.GetUserSettings(user.id);
				response.IsSuccess = true;
				return Ok(response);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.ErrorMessage = ex.Message;
				return BadRequest(response);
			}
		}
		[HttpPost("UpdateReminderSettings")]
		public IActionResult UpdateReminderSettings([FromBody] UserSettings UserSettings)
		{
			var response = new Response<string>();
			try
			{
				var user = (User)HttpContext.Items["User"];
				var result = UserSettingsService.UpdateReminderSettings(user.id, UserSettings);
				if (result > 0)
				{
					response.IsSuccess = true;
					response.SuccessMessage = "Updated successfully.";
				}
				else
				{
					response.IsSuccess = false;
					response.SuccessMessage = "Something went wrong!";
				}
				return Ok(response);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.ErrorMessage = ex.Message;
				return BadRequest(response);
			}
		}
		[HttpPost("UpdateUserSettings")]
		public IActionResult UpdateUserSettings([FromBody] UserSettings UserSettings)
		{
			var response = new Response<string>();
			try
			{
				var user = (User)HttpContext.Items["User"];
				var result = UserSettingsService.UpdateUserSettings(user.id, UserSettings);
				if (result > 0)
				{
					response.IsSuccess = true;
					response.SuccessMessage = "Updated successfully.";
				}
				else
				{
					response.IsSuccess = false;
					response.SuccessMessage = "Something went wrong!";
				}
				return Ok(response);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.ErrorMessage = ex.Message;
				return BadRequest(response);
			}
		}
		[HttpPost("UpdateEventsSetting")]
		public IActionResult UpdateEventSetting([FromBody] UserSettings UserSettings)
		{
			var response = new Response<string>();
			try
			{
				var user = (User)HttpContext.Items["User"];
				var result = UserSettingsService.UpdateEventsSetting(user.id, UserSettings);
				if (result > 0)
				{
					response.IsSuccess = true;
					response.SuccessMessage = "Updated successfully.";
				}
				else
				{
					response.IsSuccess = false;
					response.SuccessMessage = "Something went wrong!";
				}
				return Ok(response);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.ErrorMessage = ex.Message;
				return BadRequest(response);
			}
		}
		[HttpPost("UpdateAdvertiseSetting")]
		public IActionResult UpdateAdvertiseSetting([FromBody] UserSettings UserSettings)
		{
			var response = new Response<string>();
			try
			{
				var user = (User)HttpContext.Items["User"];
				var result = UserSettingsService.UpdateAdvertiseSetting(user.id, UserSettings);
				if (result > 0)
				{
					response.IsSuccess = true;
					response.SuccessMessage = "Updated successfully.";
				}
				else
				{
					response.IsSuccess = false;
					response.SuccessMessage = "Something went wrong!";
				}
				return Ok(response);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.ErrorMessage = ex.Message;
				return BadRequest(response);
			}
		}
		[HttpPost("UpdateFeedbackSetting")]
		public IActionResult UpdateFeedbackSetting([FromBody] UserSettings UserSettings)
		{
			var response = new Response<string>();
			try
			{
				var user = (User)HttpContext.Items["User"];
				var result = UserSettingsService.UpdateFeedbackSetting(user.id, UserSettings);
				if (result > 0)
				{
					response.IsSuccess = true;
					response.SuccessMessage = "Updated successfully.";
				}
				else
				{
					response.IsSuccess = false;
					response.SuccessMessage = "Something went wrong!";
				}
				return Ok(response);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.ErrorMessage = ex.Message;
				return BadRequest(response);
			}
		}
		[HttpPost("UpdateBirthdaySetting")]
		public IActionResult UpdateBirthdaySetting([FromBody] UserSettings UserSettings)
		{
			var response = new Response<string>();
			try
			{
				var user = (User)HttpContext.Items["User"];
				var result = UserSettingsService.UpdateBirthdaySetting(user.id, UserSettings);
				if (result > 0)
				{
					response.IsSuccess = true;
					response.SuccessMessage = "Updated successfully.";
				}
				else
				{
					response.IsSuccess = false;
					response.SuccessMessage = "Something went wrong!";
				}
				return Ok(response);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.ErrorMessage = ex.Message;
				return BadRequest(response);
			}
		}

		[HttpPost("InsertUserCategory")]
		public IActionResult InsertUserCategory([FromBody] UserCategory userCategory)
		{
			var response = new Response<string>();
			try
			{
				var user = (User)HttpContext.Items["User"];
				var result = UserSettingsService.InsertUserCategory(user.id, userCategory.listing_category_detailId.Value);
				//int userCategoryId;
				//int.TryParse(result, out userCategoryId);
				if (result == "success")
				{
					response.IsSuccess = true;
					response.SuccessMessage = "category has been added!";
				}
				else
				{
					response.IsSuccess = false;
					if(result == "error_maximum_category_reached|p_sales_userId")
					{
                        response.ErrorMessage = "You can select up to four categories!";
                    }
					else
					{
						response.ErrorMessage = result;
                    }
				}
				return Ok(response);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.ErrorMessage = ex.Message;
				return BadRequest(response);
			}
		}
		[HttpPost("InsertAdvertisementTemplate")]
		public IActionResult InsertAdvertisementTemplate([FromBody] UserCategory userCategory)
		{
			var response = new Response<string>();
			try
			{
				var user = (User)HttpContext.Items["User"];
				var result = UserSettingsService.InsertAdvertisementTemplate(user.id, userCategory.marketing_template_detail_id.Value);
				int userTemplateId;
				int.TryParse(result, out userTemplateId);
				if (userTemplateId>0)
				{
					response.Item = result;
                    response.IsSuccess = true;
					response.SuccessMessage = "template has been added!";
				}
				else
				{
					response.IsSuccess = false;
					if(result == "error_maximum_advertisement_reached|p_sales_userId")
					{
                        response.ErrorMessage = "You can select up to four advertisement templates!";
                    }
					else
					{
						response.ErrorMessage = result;
                    }
				}
				return Ok(response);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.ErrorMessage = ex.Message;
				return BadRequest(response);
			}
		}
		[HttpPost("DeleteUserCategory")]
		public IActionResult DeleteUserCategory([FromBody] UserCategory userCategory)
		{
			var response = new Response<string>();
			try
			{
				var user = (User)HttpContext.Items["User"];
				var result = UserSettingsService.DeleteUserCategory(user.id, userCategory.listing_category_detailId.Value);
				//int userCategoryId;
				//int.TryParse(result, out userCategoryId);
				if (result == "success")
				{
					response.IsSuccess = true;
					response.SuccessMessage = "category has been deleted!";
				}
				else
				{
					response.IsSuccess = false;
						response.ErrorMessage = result;
				}
				return Ok(response);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.ErrorMessage = ex.Message;
				return BadRequest(response);
			}
		}


		//[HttpPost("UpdateDeleteCustomerFlag")]
		//public IActionResult UpdateDeleteCustomerFlag(int dcflag)
		//{
		//    var response = new Response<string>();
		//    try
		//    {
		//        var user = (User)HttpContext.Items["User"];
		//        var result = UserSettingsService.UpdateDeleteCustomerFlag(user.id, dcflag);
		//        if (result > 0)
		//        {
		//            response.IsSuccess = true;
		//            response.SuccessMessage = "Updated successfully.";
		//        }
		//        else
		//        {
		//            response.IsSuccess = false;
		//            response.SuccessMessage = "Something went wrong!";
		//        }
		//        return Ok(response);
		//    }
		//    catch (Exception ex)
		//    {
		//        response.IsSuccess = false;
		//        response.ErrorMessage = ex.Message;
		//        return BadRequest(response);
		//    }
		//}
		//[HttpPost("UpdateSendReminderFlag")]
		//public IActionResult UpdateSendReminderFlag(int srflag)
		//{
		//    var response = new Response<string>();
		//    try
		//    {
		//        var user = (User)HttpContext.Items["User"];
		//        var result = UserSettingsService.UpdateSendReminderFlag(user.id, srflag);
		//        if (result > 0)
		//        {
		//            response.IsSuccess = true;
		//            response.SuccessMessage = "Updated successfully.";
		//        }
		//        else
		//        {
		//            response.IsSuccess = false;
		//            response.SuccessMessage = "Something went wrong!";
		//        }
		//        return Ok(response);
		//    }
		//    catch (Exception ex)
		//    {
		//        response.IsSuccess = false;
		//        response.ErrorMessage = ex.Message;
		//        return BadRequest(response);
		//    }
		//}
		//[HttpPost("UpdateReminderDuration")]
		//public IActionResult UpdateReminderDuration(int reminderDuration)
		//{
		//    var response = new Response<string>();
		//    try
		//    {
		//        var user = (User)HttpContext.Items["User"];
		//        var result = UserSettingsService.UpdateReminderDuration(user.id, reminderDuration);
		//        if (result > 0)
		//        {
		//            response.IsSuccess = true;
		//            response.SuccessMessage = "Updated successfully.";
		//        }
		//        else
		//        {
		//            response.IsSuccess = false;
		//            response.SuccessMessage = "Something went wrong!";
		//        }
		//        return Ok(response);
		//    }
		//    catch (Exception ex)
		//    {
		//        response.IsSuccess = false;
		//        response.ErrorMessage = ex.Message;
		//        return BadRequest(response);
		//    }
		//}

		//[HttpPost("UpdateReminderTimes")]
		//public IActionResult UpdateReminderTimes(int reminderTimes)
		//{
		//    var response = new Response<string>();
		//    try
		//    {
		//        var user = (User)HttpContext.Items["User"];
		//        var result = UserSettingsService.UpdateReminderTimes(user.id, reminderTimes);
		//        if (result > 0)
		//        {
		//            response.IsSuccess = true;
		//            response.SuccessMessage = "Updated successfully.";
		//        }
		//        else
		//        {
		//            response.IsSuccess = false;
		//            response.SuccessMessage = "Something went wrong!";
		//        }
		//        return Ok(response);
		//    }
		//    catch (Exception ex)
		//    {
		//        response.IsSuccess = false;
		//        response.ErrorMessage = ex.Message;
		//        return BadRequest(response);
		//    }
		//}

	}
}
