using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using SoftMarketing.WebAPI.Core;
using SoftMarketing.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using SoftMarketing.Model.SalesModels;
using SoftMarketing.WebAPI.Security;
using SoftMarketing.Services.Marketing;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using System.Drawing.Imaging;
using SoftMarketing.WebAPI.Helpers;
using SoftMarketing.WebAPI.Filters;
using Microsoft.AspNetCore.SignalR;
using SoftMarketing.Service.Marketing;

namespace SoftMarketing.WebAPI.Controllers.MarketingControllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TemplatesController : ApiControllerBase
    {
        TemplateService TemplateService { get; set; }
        IWebHostEnvironment WebHostEnvironment { get; set; }
        private readonly IHubContext<ServerHub> _hubContext;


        public TemplatesController(IWebHostEnvironment webHostEnvironment, IHubContext<ServerHub> hubContext)
        {
            TemplateService = new TemplateService();
            WebHostEnvironment = webHostEnvironment;
            _hubContext = hubContext;
        }

        [HttpGet("GetCountryTemplates")]
        public IActionResult GetCountryTemplates()
        {
            var response = new Response<UserTemplateCountry>();
            try
            {
                var user = (User)HttpContext.Items["User"];
                var result = TemplateService.GetCountryTemplates(user.id, user.phone_countryId);
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

        /// <summary>
        /// /This API used to subscribe the user in a country template  
        /// </summary>
        [HttpPost("SubsicribeToCountryTemplates")]
        public IActionResult SubsicribeToCountryTemplates(List<SubscriptionTemplate> userTemplates)
        {
            var response = new Response<string>();
            try
            {
                var user = (User)HttpContext.Items["User"];
                foreach (var ut in userTemplates)
                {
                    ut.sales_userId = user.id;
                }
                var result = TemplateService.SubsicribeToCountryTemplates(userTemplates, user.id);
                if (result > 0)
                {
                    response.IsSuccess = true;
                    response.SuccessMessage = "The templates added successfully";
                    return Ok(response);
                }
                else
                {
                    response.IsSuccess = true;
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
        [HttpPost("SubsicribeToCountryTemplate")]
        [UTemplateTS]
        public IActionResult SubsicribeToCountryTemplate(SubscriptionTemplate userTemplates)
        {
            var response = new Response<string>();
            try
            {
                var user = (User)HttpContext.Items["User"];
                var result = TemplateService.SubsicribeToCountryTemplate(userTemplates,user.id);
                int userTemplateId;
                int.TryParse(result, out userTemplateId);
                if (userTemplateId > 0)
                {
                    response.Item = result;
                    response.IsSuccess = true;
                    response.SuccessMessage = "The templates added successfully";
                    return Ok(response);
                }
                else
                {
                    response.IsSuccess = true;
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
        /// <summary>
        /// This API used to get all user templates
        /// </summary>
        [HttpGet("GetAllUserTemplates")]
        public IActionResult GetAllUserTemplates()
        {
            var response = new Response<User_Template>();
            try
            {
                var user = (User)HttpContext.Items["User"];
                var result = TemplateService.GetAllUserTemplates(user.id);
                response.IsSuccess = true;
                response.Items = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
                return Ok(response);
            }
        }

        /// <summary>
        /// This API used to get all user templates
        /// </summary>
        [HttpGet("GetAllUserTemplatesWithImages")]
        public IActionResult GetAllUserTemplatesWithImages()
        {
            var response = new Response<User_Template>();
            try
            {
                var user = (User)HttpContext.Items["User"];
                var result = TemplateService.GetAllUserTemplatesWithImages(user.id);
                response.IsSuccess = true;
                response.Items = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
                return Ok(response);
            }
        }
        /// <summary>
        /// This API used to get all user templates
        /// </summary>
        [HttpGet("GetSpecificUserTemplates")]
        public IActionResult GetSpecificUserTemplates(string uTemplateIDs)
        {
            var response = new Response<User_Template>();
            try
            {
                var user = (User)HttpContext.Items["User"];
                var result = TemplateService.GetSpecificUserTemplates(user.id, uTemplateIDs);
                response.IsSuccess = true;
                response.Items = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
                return Ok(response);
            }
        }

        [HttpPost("AddCustomTemplate")]
        [UTemplateTS]
        public IActionResult AddCustomTemplate(User_Template template)
        {
            var response = new Response<string>();
            try
            {
                var user = (User)HttpContext.Items["User"];
                template.sales_userId = user.id;
                var result = TemplateService.AddCustomTemplate(template, user.id);
                int uTemplateId = 0;
                int.TryParse(result, out uTemplateId);
                if (uTemplateId > 0)
                {
                    response.IsSuccess = true;
                    response.SuccessMessage = "The templates added successfully";
                    response.Item = result;
                    try
                    {
                        if (template.image?.Length > 0)
                        {
                            byte[] bytes = Convert.FromBase64String(template.image);
                            string imagePath = Helper.GitUserImagePath(WebHostEnvironment.WebRootPath, uTemplateId.ToString());
                            System.IO.File.WriteAllBytes(imagePath, bytes);
                        }
                        try
                        {
                            template.usertemplate_id = uTemplateId;
                            new ServerHub(null, _hubContext).UpdateClientData("add_template", user, template);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    catch (Exception)
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = "The template updated but the image not";
                        return BadRequest(response);
                    }
                    return Ok(response);
                }
                else
                {
                    if (!string.IsNullOrEmpty(result)){
                        if (result.Contains("maximum_custom_message_reached"))
                        {
                            response.ErrorMessage = "You reached the maximum number of templates!";
                        }
                    } 
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

        /// <summary>
        /// This API used to :
        /// 1- update predefined template ex: country template or custom template
        /// </summary>
        [HttpPost("UpdateUserTemplate")]
        [UTemplateTS]
        public IActionResult UpdateUserTemplate(User_Template template)
        {
            var response = new Response<string>();
            try
            {
                var user = (User)HttpContext.Items["User"];
                template.sales_userId = user.id;
                var result = TemplateService.UpdateUserTemplate(template, user.id);
                if (result > 0)
                {
                    response.IsSuccess = true;
                    response.SuccessMessage = "The templates updated successfully";
                    try
                    {
                        if (template.image?.Length > 0)
                        {

                            byte[] bytes = Convert.FromBase64String(template.image);
                            string imagePath = Helper.GitUserImagePath(WebHostEnvironment.WebRootPath, template.usertemplate_id.ToString());
                            System.IO.File.WriteAllBytes(imagePath, bytes);
                        }
                        try
                        {
                            new ServerHub(null, _hubContext).UpdateClientData("update_template", user, template);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    catch (Exception)
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = "The template updated but the image not";
                        return BadRequest(response);
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


        [HttpDelete("DeleteUserTemplate")]
        [UTemplateTS]
        public IActionResult DeleteTemplate(int templateId)
        {
            if (templateId == null)
            {
                return BadRequest();
            }
            var response = new Response<string>();
            try
            {
                var user = (User)HttpContext.Items["User"];
                var result = TemplateService.DeleteUserTemplate(templateId, user.id);
                string imagePath = Helper.GitUserImagePath(WebHostEnvironment.WebRootPath, templateId.ToString());
               
                if (result > 0)
                {
                    try
                    {
                        if (!Directory.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        try
                        {
                            new ServerHub(null, _hubContext).UpdateClientData("delete_template", user, templateId);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    catch (Exception)
                    {
                    }
                    response.IsSuccess = true;
                    response.SuccessMessage = "Deleted successfully";
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