//using System;
//using System.Data;
//using System.Data.Common;
//using System.Collections;
//using SoftMarketing.WebAPI.Core;
//using SoftMarketing.Service;
//using SoftMarketing.Model;
//using Microsoft.AspNetCore.Mvc;
//using System.Net;

//namespace SoftMarketing.translations_winforms
//{

//    public abstract class Translations_winformsController : ApiControllerBase
//    {

//        Translations_WinFormsService Translations_WinFormsService { get; set; }
//        public Translations_winformsController()
//        {
//            Translations_WinFormsService = new Translations_WinFormsService();
//        }

//        [HttpPost(Name = "Add")]
//        public HttpResponseMessage Add(HttpRequestMessage request, Translation Translation)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = Translations_WinFormsService.Add(Translation, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "Update")]
//        public HttpResponseMessage Update(HttpRequestMessage request, Translation Translation)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = Translations_WinFormsService.Update(Translation, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "Delete")]
//        public HttpResponseMessage Delete(HttpRequestMessage request, string english)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    Translations_WinFormsService.Delete(english, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "GetPagedData")]
//        public HttpResponseMessage GetPagedData(HttpRequestMessage request, Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = Translations_WinFormsService.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }
        
//        [HttpPost(Name = "Get")]
//        public HttpResponseMessage Get(HttpRequestMessage request, String english)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = Translations_WinFormsService.Get(english);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }
        
//        [HttpPost(Name = "CountAll")]
//        public HttpResponseMessage CountAll(HttpRequestMessage request)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = Translations_WinFormsService.CountAll();
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "GetAll")]
//        public HttpResponseMessage GetAll(HttpRequestMessage request)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = Translations_WinFormsService.GetAll();
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }
        
//        [HttpPost(Name = "GetByPk")]
//        public HttpResponseMessage GetByPk(HttpRequestMessage request, string english)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = Translations_WinFormsService.GetByPk(english);
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