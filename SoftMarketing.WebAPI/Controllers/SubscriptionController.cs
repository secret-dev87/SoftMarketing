//using System;
//using System.Data;
//using System.Data.Common;
//using System.Collections;
//using SoftMarketing.Service;
//using SoftMarketing.WebAPI.Core;
//using SoftMarketing.Model;
//using System.Net;
//using Microsoft.AspNetCore.Mvc;

//namespace SoftMarketing.subscription
//{

//    public class SubscriptionController : ApiControllerBase
//    {


//        SubscriptionService SubscriptionService { get; set; }
//        public SubscriptionController()
//        {
//            SubscriptionService = new();
//        }

//        [HttpPost(Name = "Add")]
//        public HttpResponseMessage Add(HttpRequestMessage request, Subscription Subscription)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = SubscriptionService.Add(Subscription, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "Update")]
//        public HttpResponseMessage Update(HttpRequestMessage request, Subscription Subscription)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = SubscriptionService.Update(Subscription, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "Delete")]
//        public HttpResponseMessage Delete(HttpRequestMessage request, Int32 subscriptionid)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    SubscriptionService.Delete(subscriptionid, dbTransaction);
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
//                    var response = SubscriptionService.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpGet(Name = "Get")]
//        public HttpResponseMessage Get(HttpRequestMessage request, Int32 subscriptionid)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = SubscriptionService.Get(subscriptionid);
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
//                    var response = SubscriptionService.CountAll();
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
//                    var response = SubscriptionService.GetAll();
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpGet(Name = "GetByPk")]
//        public HttpResponseMessage GetAll(HttpRequestMessage request, Int32 subscriptionid)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = SubscriptionService.GetByPk(subscriptionid);
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