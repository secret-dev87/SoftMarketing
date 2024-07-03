using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using SoftMarketing.Service;
using SoftMarketing.WebAPI.Core;
using SoftMarketing.Model;
using System.Net;
using SoftMarketing.Model.SalesModels;
using SoftMarketing.Services.Sales;

namespace SoftMarketing.Event
{

    [ApiController]
    [Route("[controller]")]
    public class CenterUserController : ApiControllerBase
    {
        CenterUserService CenterUserService { get; set; }
        public CenterUserController()
        {
            CenterUserService = new();
        }

        [HttpGet]
        [Route("/GetCenterUser")]
        public ActionResult GetCenterUser([FromBody] CenterUser CenterUser)
        {
            try
            {
                var result = CenterUserService.GetCenterUser(CenterUser);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}