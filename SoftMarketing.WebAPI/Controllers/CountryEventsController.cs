//using System;
//using System.Data;
//using System.Data.Common;
//using System.Collections;
//using SoftMarketing.Service;
//using Microsoft.AspNetCore.Mvc;
//using SoftMarketing.Model;
//using SoftMarketing.WebAPI.Core;
//using System.Net;

//namespace SoftMarketing.countryevents
//{

//    [ApiController]
//    [Route("[controller]")]
//    public class CountryEventsController : ApiControllerBase
//    {
//        CountryEventsService CountryEventsService { set; get; }
//        public CountryEventsController()
//        {
//            CountryEventsService = new();
//        }

//        [HttpPost(Name = "Add")]
//        public HttpResponseMessage Add(HttpRequestMessage request, CountryEvents entity)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = CountryEventsService.Add(entity, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "AddMultiple")]
//        public HttpResponseMessage AddMultiple(HttpRequestMessage request, List<CountryEvents> entityCollection)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    CountryEventsService.AddMultiple(entityCollection, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "Update")]
//        public HttpResponseMessage Update(HttpRequestMessage request, CountryEvents entity)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = CountryEventsService.Update(entity, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "UpdateMultiple")]
//        public HttpResponseMessage UpdateMultiple(HttpRequestMessage request, List<CountryEvents> entityCollection)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    CountryEventsService.UpdateMultiple(entityCollection, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "Delete")]
//        public HttpResponseMessage Delete(HttpRequestMessage request, int countryEventId)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    CountryEventsService.Delete(countryEventId, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }
        
//        [HttpPost(Name = "GetByID")]
//        public HttpResponseMessage GetByID(HttpRequestMessage request, int id)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = CountryEventsService.GetByID(id);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }
        
//        [HttpGet(Name = "GetByID")]
//        public CountryEvents GetByID(Int32 id)
//        {
//            return CountryEventsService.GetByID(id);
//        }
//    }
//}