//using System;
//using System.Data;
//using System.Data.Common;
//using System.Collections;
//using SoftMarketing.WebAPI.Core;
//using SoftMarketing.Service;
//using SoftMarketing.Model;
//using Microsoft.AspNetCore.Mvc;
//using System.Net;

//namespace SoftMarketing.template_dates
//{

//    public class Template_datesController : ApiControllerBase
//    {
//        Template_DatesService Template_DatesService { get; set; }
//        public Template_datesController()
//        {
//            Template_DatesService = new();
//        }

//        [HttpPost(Name = "Add")]
//        public HttpResponseMessage Add(HttpRequestMessage request, Template_Dates Template_Dates)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = Template_DatesService.Add(Template_Dates, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "Update")]
//        public HttpResponseMessage Update(HttpRequestMessage request, Template_Dates Template_Dates)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = Template_DatesService.Update(Template_Dates, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "Delete")]
//        public HttpResponseMessage Delete(HttpRequestMessage request, Int32 id)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    Template_DatesService.Delete(id, dbTransaction);
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
//                    var response = Template_DatesService.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpGet(Name = "Get")]
//        public HttpResponseMessage Get(HttpRequestMessage request, Int32 id)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = Template_DatesService.Get(id);
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
//                    var response = Template_DatesService.CountAll();
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
//                    var response = Template_DatesService.GetAll();
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }
        
//        [HttpGet(Name = "GetByPk")]
//        public HttpResponseMessage GetByPk(HttpRequestMessage request, Int32 id)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = Template_DatesService.GetByPk(id);
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