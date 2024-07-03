//using System;
//using System.Data;
//using System.Data.Common; 
//using System.Collections;
//using SoftMarketing.WebAPI.Core;
//using SoftMarketing.Service;
//using SoftMarketing.Model;
//using System.Net;
//using Microsoft.AspNetCore.Mvc;

//namespace SoftMarketing.subscriptiontype{
	
//	public class SubscriptiontypeController: ApiControllerBase
//	{
//		SubscriptionTypeService SubscriptionTypeService { get; set; }
//		public SubscriptiontypeController()
//		{
//			SubscriptionTypeService = new();
//		}

//		[HttpPost(Name = "Add")]
//		public HttpResponseMessage Add(HttpRequestMessage request, SubscriptionType SubscriptionType)
//		{
//			return GetHttpResponse(request, () =>
//			{
//				try
//				{
//					var response = SubscriptionTypeService.Add(SubscriptionType, dbTransaction);
//					return request.CreateResponse(HttpStatusCode.OK, response); ;
//				}
//				catch (Exception ex)
//				{
//					return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//				}
//			});
//		}

//		[HttpPost(Name = "Update")]
//		public HttpResponseMessage Update(HttpRequestMessage request, SubscriptionType SubscriptionType)
//		{
//			return GetHttpResponse(request, () =>
//			{
//				try
//				{
//					var response = SubscriptionTypeService.Update(SubscriptionType, dbTransaction);
//					return request.CreateResponse(HttpStatusCode.OK, response); ;
//				}
//				catch (Exception ex)
//				{
//					return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//				}
//			});
//		}

//		[HttpPost(Name = "Delete")]
//		public HttpResponseMessage Delete(HttpRequestMessage request, Int32 subscriptiontypeid)
//		{
//			return GetHttpResponse(request, () =>
//			{
//				try
//				{
//					SubscriptionTypeService.Delete(subscriptiontypeid, dbTransaction);
//					return request.CreateResponse(HttpStatusCode.OK); ;
//				}
//				catch (Exception ex)
//				{
//					return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//				}
//			});
//		}

//		[HttpGet(Name = "GetPagedData")]
//		public HttpResponseMessage GetPagedData(HttpRequestMessage request, Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn)
//		{
//			return GetHttpResponse(request, () =>
//			{
//				try
//				{
//					var response = SubscriptionTypeService.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn);
//					return request.CreateResponse(HttpStatusCode.OK, response); ;
//				}
//				catch (Exception ex)
//				{
//					return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//				}
//			});
//		}

//		[HttpGet(Name = "Get")]
//		public HttpResponseMessage Get(HttpRequestMessage request, Int32 subscriptiontypeid)
//		{
//			return GetHttpResponse(request, () =>
//			{
//				try
//				{
//					var response = SubscriptionTypeService.Get(subscriptiontypeid);
//					return request.CreateResponse(HttpStatusCode.OK, response); ;
//				}
//				catch (Exception ex)
//				{
//					return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//				}
//			});
//		}

//		[HttpGet(Name = "CountAll")]
//		public HttpResponseMessage CountAll(HttpRequestMessage request)
//		{
//			return GetHttpResponse(request, () =>
//			{
//				try
//				{
//					var response = SubscriptionTypeService.CountAll();
//					return request.CreateResponse(HttpStatusCode.OK, response); ;
//				}
//				catch (Exception ex)
//				{
//					return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//				}
//			});
//		}

//		[HttpGet(Name = "GetAll")]
//		public HttpResponseMessage GetAll(HttpRequestMessage request)
//		{
//			return GetHttpResponse(request, () =>
//			{
//				try
//				{
//					var response = SubscriptionTypeService.GetAll();
//					return request.CreateResponse(HttpStatusCode.OK, response); ;
//				}
//				catch (Exception ex)
//				{
//					return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//				}
//			});
//		}

//		[HttpGet(Name = "GetByPk")]
//		public HttpResponseMessage GetByPk(HttpRequestMessage request, Int32 eventid)
//		{
//			return GetHttpResponse(request, () =>
//			{
//				try
//				{
//					var response = SubscriptionTypeService.GetByPk(eventid);
//					return request.CreateResponse(HttpStatusCode.OK, response); ;
//				}
//				catch (Exception ex)
//				{
//					return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//				}
//			});
//		}

//	}
//}