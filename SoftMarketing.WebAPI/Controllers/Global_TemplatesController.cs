//using System;
//using System.Data;
//using System.Data.Common;
//using System.Collections;
//using SoftMarketing.WebAPI.Core;
//using SoftMarketing.Service;
//using SoftMarketing.Model;
//using System.Net;
//using Microsoft.AspNetCore.Mvc;

//namespace SoftMarketing.global_templates
//{

//    public class Global_templatesController : ApiControllerBase
//    {

//        Global_TemplatesService Global_TemplatesService { get; set; }
//        public Global_templatesController()
//        {
//            Global_TemplatesService = new();
//        }

//        [HttpPost(Name = "Add")]
//        public HttpResponseMessage Add(HttpRequestMessage request, Global_Templates Global_Templates)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = Global_TemplatesService.Add(Global_Templates, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "Update")]
//        public HttpResponseMessage Update(HttpRequestMessage request, Global_Templates Global_Templates)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = Global_TemplatesService.Update(Global_Templates, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "Delete")]
//        public HttpResponseMessage Delete(HttpRequestMessage request, String langugage_or_country, String name)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    Global_TemplatesService.Delete(langugage_or_country, name, dbTransaction);
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
//                    var response = Global_TemplatesService.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpGet(Name = "Get")]
//        public HttpResponseMessage Get(HttpRequestMessage request, Int32 langugage_or_country, string name)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = Global_TemplatesService.Get(langugage_or_country, name);
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
//                    var response = Global_TemplatesService.CountAll();
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
//                    var response = Global_TemplatesService.GetAll();
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpGet(Name = "GetByPk")]
//        public HttpResponseMessage GetByPk(HttpRequestMessage request, Int32 langugage_or_country, string name)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = Global_TemplatesService.GetByPk(langugage_or_country, name);
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