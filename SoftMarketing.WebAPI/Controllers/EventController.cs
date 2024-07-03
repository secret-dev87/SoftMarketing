using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using SoftMarketing.Service;
using SoftMarketing.WebAPI.Core;
using SoftMarketing.Model;
using System.Net;

namespace SoftMarketing.Event
{

    [ApiController]
    [Route("Event")]
    public class EventController : ApiControllerBase
    {
        EeventService EeventService { get; set; }
        public EventController()
        {
            EeventService = new();
        }

        [HttpPost(Name = "Add")]
        public int Add(/*HttpRequestMessage request,*/ Events Event)
        {
            //return EeventService.Add(Event, dbTransaction);

            return 0;
            //return GetHttpResponse(request, () =>
            //{
            //try
            //    {
            //        var response = EeventService.Add(Event, dbTransaction);
            //        return request.CreateResponse(HttpStatusCode.OK, response); ;
            //    }
            //    catch (Exception ex)
            //    {
            //        return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            //    }
            //});
        }

        //[HttpPost(Name = "Update")]
        //public HttpResponseMessage Update(HttpRequestMessage request, Events Event)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        try
        //        {
        //            var response = EeventService.Update(Event, dbTransaction);
        //            return request.CreateResponse(HttpStatusCode.OK, response); ;
        //        }
        //        catch (Exception ex)
        //        {
        //            return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
        //        }
        //    });
        //}

        //[HttpPost(Name = "Delete")]
        //public HttpResponseMessage Delete(HttpRequestMessage request, Int32 eventid)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        try
        //        {
        //            EeventService.Delete(eventid, dbTransaction);
        //            return request.CreateResponse(HttpStatusCode.OK); ;
        //        }
        //        catch (Exception ex)
        //        {
        //            return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
        //        }
        //    });
        //}

        //[HttpGet(Name = "GetPagedData")]
        //public HttpResponseMessage GetPagedData(HttpRequestMessage request, Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        try
        //        {
        //            var response = EeventService.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn);
        //            return request.CreateResponse(HttpStatusCode.OK, response); ;
        //        }
        //        catch (Exception ex)
        //        {
        //            return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
        //        }
        //    });
        //}

        //[HttpGet(Name = "Get")]
        //public HttpResponseMessage Get(HttpRequestMessage request, Int32 eventid)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        try
        //        {
        //            var response = EeventService.Get(eventid);
        //            return request.CreateResponse(HttpStatusCode.OK, response); ;
        //        }
        //        catch (Exception ex)
        //        {
        //            return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
        //        }
        //    });
        //}

        [HttpGet(Name = "/CountAll")]
        public ActionResult CountAll(/*HttpRequestMessage request*/)
        {
            //var response = EeventService.CountAll();
            //return Ok(response);

            EeventService.AddAndUpdatCustomer();
            return Ok();

            //return GetHttpResponse(request, () =>
            //{
            //    try
            //    {
            //        var response = EeventService.CountAll();
            //        return request.CreateResponse(HttpStatusCode.OK, response); ;
            //    }
            //    catch (Exception ex)
            //    {
            //        return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            //    }
            //});
        }
        //[HttpGet(Name = "/AddAndremove")]
        //[Route("/AddAndremove")]
        //public ActionResult AddAndremove(/*HttpRequestMessage request*/)
        //{
        //    EeventService.AddAndUpdatCustomer();
        //    return Ok();
        //    //return GetHttpResponse(request, () =>
        //    //{
        //    //    try
        //    //    {
        //    //        var response = EeventService.CountAll();
        //    //        return request.CreateResponse(HttpStatusCode.OK, response); ;
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
        //    //    }
        //    //});
        //}

        //[HttpGet(Name = "GetAll")]
        //public HttpResponseMessage GetAll(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        try
        //        {
        //            var response = EeventService.GetAll();
        //            return request.CreateResponse(HttpStatusCode.OK, response); ;
        //        }
        //        catch (Exception ex)
        //        {
        //            return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
        //        }
        //    });
        //}

        //[HttpGet(Name = "GetByPk")]
        //public HttpResponseMessage GetByPk(HttpRequestMessage request, Int32 eventid)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        try
        //        {
        //            var response = EeventService.GetByPk(eventid);
        //            return request.CreateResponse(HttpStatusCode.OK, response); ;
        //        }
        //        catch (Exception ex)
        //        {
        //            return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
        //        }
        //    });
        //}

    }
}