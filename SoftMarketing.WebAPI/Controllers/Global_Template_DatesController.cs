//using System;
//using System.Data;
//using System.Data.Common;
//using System.Collections;
//using SoftMarketing.Service;
//using SoftMarketing.Model;
//using SoftMarketing.WebAPI.Core;
//using Microsoft.AspNetCore.Mvc;
//using System.Net;

//namespace SoftMarketing.global_template_dates
//{

//    public class Global_template_datesController : ApiControllerBase
//    {
//        Global_Template_DatesService Global_Template_DatesService { set; get; }

//        public Global_template_datesController()
//        {
//            Global_Template_DatesService = new();
//        }

//        [HttpPost(Name = "Add")]
//        public HttpResponseMessage Add(HttpRequestMessage request, Template_Dates Template_Dates)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = Global_Template_DatesService.Add(Template_Dates, dbTransaction);
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
//                    var response = Global_Template_DatesService.Update(Template_Dates, dbTransaction);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpPost(Name = "Delete")]
//        public HttpResponseMessage Delete(HttpRequestMessage request, Int32 ID)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    Global_Template_DatesService.Delete(ID, dbTransaction);
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
//                    var response = Global_Template_DatesService.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn);
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpGet(Name = "Get")]
//        public HttpResponseMessage Get(HttpRequestMessage request, Int32 ID)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = Global_Template_DatesService.Get(ID);
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
//                    var response = Global_Template_DatesService.CountAll();
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
//                    var response = Global_Template_DatesService.GetAll();
//                    return request.CreateResponse(HttpStatusCode.OK, response); ;
//                }
//                catch (Exception ex)
//                {
//                    return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//                }
//            });
//        }

//        [HttpGet(Name = "GetByPk")]
//        public HttpResponseMessage GetByPk(HttpRequestMessage request, Int32 ID)
//        {
//            return GetHttpResponse(request, () =>
//            {
//                try
//                {
//                    var response = Global_Template_DatesService.GetByPk(ID);
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