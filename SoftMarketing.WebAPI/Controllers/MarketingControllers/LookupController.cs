using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using SoftMarketing.Service.Marketing;
using SoftMarketing.WebAPI.Core;
using SoftMarketing.Model.MarketingModels;
using System.Net;
using SoftMarketing.Model.SalesModels;
using SoftMarketing.Service;
using SoftMarketing.WebAPI.Security;
using SoftMarketing.Services.Marketing;
using SoftMarketing.Model;

namespace SoftMarketing.WebAPI.Controllers.MarketingControllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LookupController : ApiControllerBase
    {
        LookupService LookupService { get; set; }
        TemplateService TemplateService { get; set; }
        public LookupController()
        {
            LookupService = new();
            TemplateService = new();
        }

        [HttpGet]
        [Route("GetSocialApp")]
        public ActionResult GetSocialApp()
        {
            try
            {
                //var user = (User)HttpContext.Items["User"];
                var result = LookupService.GetSocialApp();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        [Route("GetMessagingApp")]
        public ActionResult GetMessagingApp()
        {
            try
            {
                //var user = (User)HttpContext.Items["User"];
                var result = LookupService.GetMessagingApp();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

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
        [HttpGet("GetCategoryTypes")]
        public IActionResult GetCategoryTypes()
        {
            var response = new Response<CategoryType>();
            try
            {
                var result = LookupService.GetCategoryTypes();
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

		[HttpGet("GetMainCategoryList")]
        public IActionResult GetMainCategoryList(int categoryTypeId)
        {
            var response = new Response<MainCategory>();
            try
            {
                var result = LookupService.GetMainCategoryList(categoryTypeId);
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

		[HttpGet("GetChildCategoryList")]
        public IActionResult GetChildCategoryList(int categoryDetailId)
        {
            var response = new Response<ChildCategory>();
            try
            {
                var result = LookupService.GetChildCategoryList(categoryDetailId);
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
    }
}