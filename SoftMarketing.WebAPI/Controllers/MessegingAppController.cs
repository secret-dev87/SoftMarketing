//using System;
//using System.Data;
//using System.Data.Common;
//using System.Collections;
//using SoftMarketing.Service;
//using SoftMarketing.Model;
//using Microsoft.AspNetCore.Mvc;
//using SoftMarketing.WebAPI.Core;
//using System.Net;

//namespace SoftMarketing.messegingapp
//{

//    public class MessegingappController : ApiControllerBase
//    {
//        MessegingAppService MessegingAppService { get; set; }
//        public MessegingappController()
//        {
//            MessegingAppService = new();
//        }

//        [HttpPost(Name = "Add")]
//        public HttpResponseMessage Add(HttpRequestMessage request, MessegingApp MessegingApp)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = MessegingAppService.Add(MessegingApp, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "Update")]
//        public HttpResponseMessage Update(HttpRequestMessage request, MessegingApp MessegingApp)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = MessegingAppService.Update(MessegingApp, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "Delete")]
//        public HttpResponseMessage Delete(HttpRequestMessage request, Int32 trial_id_1)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    MessegingAppService.Delete(trial_id_1, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpGet(Name = "GetPagedData")]
//        public HttpResponseMessage GetPagedData(HttpRequestMessage request, Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = MessegingAppService.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpGet(Name = "Get")]
//        public HttpResponseMessage Get(HttpRequestMessage request, Int32 trial_id_1)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = MessegingAppService.Get(trial_id_1);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpGet(Name = "CountAll")]
//        public HttpResponseMessage CountAll(HttpRequestMessage request)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = MessegingAppService.CountAll();
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpGet(Name = "GetAll")]
//        public HttpResponseMessage GetAll(HttpRequestMessage request)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = MessegingAppService.GetAll();
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpGet(Name = "GetByPk")]
//        public HttpResponseMessage GetByPk(HttpRequestMessage request, Int32 trial_id_1)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = MessegingAppService.GetByPk(trial_id_1);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//    }
//}