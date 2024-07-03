//using System;
//using System.Data;
//using System.Data.Common;
//using System.Collections;
//using SoftMarketing.Model;
//using SoftMarketing.WebAPI.Core;
//using SoftMarketing.Service;
//using Microsoft.AspNetCore.Mvc;
//using System.Net;

//namespace SoftMarketing.WebAPI
//{

//    [ApiController]
//    [Route("[controller]")]
//    public class User_messageController : ApiControllerBase
//    {
//        User_MessageService user_MessageService { get; set; }
//        public User_messageController()
//        {
//            user_MessageService = new();
//        }

//        [HttpPost(Name = "Add")]
//        public HttpResponseMessage Add(HttpRequestMessage request, User_Message entity)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = user_MessageService.Add(entity, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "AddMultiple")]
//        public HttpResponseMessage AddMultiple(HttpRequestMessage request, List<User_Message> entityCollection)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    user_MessageService.AddMultiple(entityCollection, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex); ;
//                }
//            });
//        }

//        [HttpPost(Name = "Update")]
//        public HttpResponseMessage Update(HttpRequestMessage request, User_Message entity)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = user_MessageService.Update(entity);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex); ;
//                }
//            });
//        }


//        [HttpPost(Name = "UpdateMultiple")]
//        public HttpResponseMessage UpdateMultiple(HttpRequestMessage request, List<User_Message> entityCollection)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    user_MessageService.UpdateMultiple(entityCollection, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex); ;
//                }
//            });

//        }

//        [HttpPost(Name = "Delete")]
//        public HttpResponseMessage Delete(HttpRequestMessage request, int cmid)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    user_MessageService.Delete(cmid, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex); ;
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
//                    var response = user_MessageService.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex); ;
//                }
//            });
//        }

//        [HttpGet(Name = "Get")]
//        public HttpResponseMessage Get(HttpRequestMessage request, Int32 cmid)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = user_MessageService.Get(cmid); ;
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex); ;
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
//                    var response = user_MessageService.GetAll(); ;
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex); ;
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
//                    var response = user_MessageService.CountAll();
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex); ;
//                }
//            });
//        }

//        public HttpResponseMessage GetByPk(HttpRequestMessage request, Int32 cmid)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = user_MessageService.GetById(cmid);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex); ;
//                }
//            });
//        }

//    }
//}