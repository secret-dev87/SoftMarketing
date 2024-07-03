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
using SoftMarketing.WebAPI.Security;
using SoftMarketing.Model;
using SoftMarketing.WebAPI.Filters;
using SoftMarketing.WebAPI.Helpers;
using Microsoft.AspNetCore.SignalR;
using SoftMarketing.WebAPI.Model;

namespace SoftMarketing.WebAPI.Controllers.MarketingControllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ApiControllerBase
    {
        CustomerService CustomerService { get; set; }
        private readonly IHubContext<ServerHub> _hubContext;
       
        public CustomerController(IHubContext<ServerHub> hubContext)
        {
            CustomerService = new();
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("Add")]
        [UCustomerTS]
        public ActionResult Add([FromBody] Customer customer)
        {
            var response = new Response<Customer>();
            try
            {
                var user = (User)HttpContext.Items["User"];
                customer.sales_userId = user.id;
                var result = CustomerService.Add(customer,user.id);
                if (result != null && result.id > 0)
                {
                    response.IsSuccess = true;
                    response.SuccessMessage = "Customer has been added!";
                    response.Item = result;
                    try
                    {
                        new ServerHub(null, _hubContext).UpdateClientData("add_customer", user, response.Item);
                    }
                    catch (Exception)
                    {
                    }
                    return Ok(response);
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Customer not added, please try again!";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = Helper.GetProperMessage(ex.Message, "Customer"); ;
                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("Update")]
        [UCustomerTS]
        public ActionResult Update([FromBody] Customer customer)
        {

            var response = new Response<Customer>();
            try
            {
                var user = (User)HttpContext.Items["User"];
                var result = CustomerService.Update(customer, user.id);
                if (result > 0)
                {
                    response.IsSuccess = true;
                    response.SuccessMessage = "Customer has been updated!";
                    try
                    {
                        new ServerHub(null, _hubContext).UpdateClientData("update_customer", user, customer);
                    }
                    catch (Exception)
                    {
                    }
                    return Ok(response);
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Customer not updated!";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = Helper.GetProperMessage(ex.Message, "Customer"); ;
                return BadRequest(response);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        [UCustomerTS]
        public ActionResult Delete(int id)
        {
            var response = new Response<Customer>();
            try
            {
                var user = (User)HttpContext.Items["User"];
                var result = CustomerService.Delete(id, user.id);
                if (result)
                {
                    response.IsSuccess = true;
                    response.SuccessMessage = "Customer has been deleted!";
                    try
                    {
                        new ServerHub(null, _hubContext).UpdateClientData("delete_customer", user, id);
                    }
                    catch (Exception)
                    {
                    }
                    return Ok(response);
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Customer not deleted!";
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

        [HttpGet]
        [Route("Get")]
        public ActionResult Get(int id)
        {
            var response = new Response<Customer>();
            try
            {
                var user = (User)HttpContext.Items["User"];
                var result = CustomerService.Get(id, user.id);
                if(result != null)
                {
                    response.Item = result;
                    response.IsSuccess = true; 
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

        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAll()
        {
            var response = new Response<Customer>();
            try
            {
                var user = (User)HttpContext.Items["User"];
                var result = CustomerService.GetAll(user.id);
                response.Items = result;
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

    }
}