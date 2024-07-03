//using System;
//using System.Data;
//using System.Data.Common;
//using System.Collections;
//using SoftMarketing.WebAPI.Core;
//using SoftMarketing.Service;
//using SoftMarketing.Model;
//using System.Net;
//using Microsoft.AspNetCore.Mvc;

//namespace SoftMarketing.messagetypes
//{

//    public class MessagetypesController : ApiControllerBase
//    {
//        MessageTypesService MessageTypesService { get; set; }
//        public MessagetypesController()
//        {
//            MessageTypesService = new MessageTypesService();
//        }

//        [HttpPost(Name = "Add")]
//        public HttpResponseMessage Add(HttpRequestMessage request, MessageTypes MessageTypes)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = MessageTypesService.Add(MessageTypes, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "Update")]
//        public HttpResponseMessage Update(HttpRequestMessage request, MessageTypes MessageTypes)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = MessageTypesService.Update(MessageTypes, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "Delete")]
//        public HttpResponseMessage Delete(HttpRequestMessage request, Int32 trial_messagetypeid_1)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    MessageTypesService.Delete(trial_messagetypeid_1, dbTransaction);
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
//                    var response = MessageTypesService.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpGet(Name = "Get")]
//        public HttpResponseMessage Get(HttpRequestMessage request, Int32 trial_messagetypeid_1)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = MessageTypesService.Get(trial_messagetypeid_1);
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
//                    var response = MessageTypesService.CountAll();
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
//                    var response = MessageTypesService.GetAll();
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpGet(Name = "GetByPk")]
//        public HttpResponseMessage GetByPk(HttpRequestMessage request, Int32 trial_messagetypeid_1)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = MessageTypesService.GetByPk(trial_messagetypeid_1);
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