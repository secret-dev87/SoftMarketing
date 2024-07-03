//public static string Decompress(String s)
//{
//    try
//    {
//        var bytes = Convert.FromBase64String(s);
//        using (var MemoryStream = new MemoryStream(bytes))
//        using (var MemoryStream2 = new MemoryStream())
//        {
//            using (var gipstream = new GZipStream(MemoryStream, CompressionMode.Decompress))
//            {
//                gipstream.CopyTo(MemoryStream2);
//            }
//            return Encoding.Unicode.GetString(MemoryStream2.ToArray());
//        }
//    }
//    catch (Exception exception)
//    {
//        MessageBox.Show(exception.ToString());
//    }
//    return ("Invalid string");
//}





//public static void CopyTo(Stream src, Stream dest)
//{
//    byte[] bytes = new byte[4096];

//    int cnt;

//    while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
//    {
//        dest.Write(bytes, 0, cnt);
//    }
//}

//public static byte[] Zip(string str)
//{
//    var bytes = Encoding.UTF8.GetBytes(str);

//    using (var msi = new MemoryStream(bytes))
//    using (var mso = new MemoryStream())
//    {
//        using (var gs = new GZipStream(mso, CompressionMode.Compress))
//        {
//            //msi.CopyTo(gs);
//            CopyTo(msi, gs);
//        }

//        return mso.ToArray();
//    }
//}

//public static string Unzip(byte[] bytes)
//{
//    using (var msi = new MemoryStream(bytes))
//    using (var mso = new MemoryStream())
//    {
//        using (var gs = new GZipStream(msi, CompressionMode.Decompress))
//        {
//            //gs.CopyTo(mso);
//            CopyTo(gs, mso);
//        }

//        return Encoding.UTF8.GetString(mso.ToArray());
//    }
//}

//static void Main(string[] args)
//{
//    byte[] r1 = Zip("StringStringStringStringStringStringStringStringStringStringStringStringStringString");
//    string r2 = Unzip(r1);
//}




//using System;
//using System.Data;
//using System.Data.Common; 
//using System.Collections;
//using SoftMarketing.Service;
//using SoftMarketing.Model;
//using Microsoft.AspNetCore.Mvc;
//using System.Net;
//using System.Net.Http;
//using SoftMarketing.WebAPI.Core;

//namespace SoftMarketing.WebAPI{

//	[ApiController]
//	[Route("[controller]")]
//	public class ClientController : ApiControllerBase
//	{
//		ClientService clientService { get; set; }
//		public ClientController()
//		{
//			clientService = new();
//		}

//		[HttpPost(Name = "Add")]
//		public HttpResponseMessage Add(HttpRequestMessage request, Client entity)
//		{
//			return GetHttpResponse(request, () =>
//			{
//                try
//                {
//                    var response = clientService.Add(entity, dbTransaction);
//					return request.CreateResponse(HttpStatusCode.OK, response); ;
//				}
//                catch (Exception ex)
//                {
//					return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
//				}
//			});
//		}

//		[HttpPost(Name = "AddMultiple")]
//        public HttpResponseMessage AddMultiple(HttpRequestMessage request, List<Client> entityCollection)
//        {
//			return GetHttpResponse(request, () =>
//			{
//				try
//				{
//					var response = clientService.AddMultiple(entityCollection, dbTransaction);
//					return request.CreateResponse(HttpStatusCode.OK, response); ;
//				}
//				catch (Exception ex)
//				{
//					return request.CreateResponse(HttpStatusCode.InternalServerError, ex); ;
//				}
//			});
//        }

//		[HttpPost(Name = "Update")]
//		public HttpResponseMessage Update(HttpRequestMessage request, Client entity)
//		{
//			return GetHttpResponse(request, () =>
//			{
//				try
//				{
//					var response = clientService.Update(entity, dbTransaction);
//					return request.CreateResponse(HttpStatusCode.OK, response); ;
//				}
//				catch (Exception ex)
//				{
//					return request.CreateResponse(HttpStatusCode.InternalServerError, ex); ;
//				}
//			});
//		}

//		[HttpPost(Name = "UpdateMultiple")]
//		public HttpResponseMessage UpdateMultiple(HttpRequestMessage request, List<Client> entityCollection)
//        {
//			return GetHttpResponse(request, () =>
//			{
//				try
//				{
//					var response = clientService.UpdateMultiple(entityCollection, dbTransaction);

//					return request.CreateResponse(HttpStatusCode.OK, response); ;
//				}
//				catch (Exception ex)
//				{

//					return request.CreateResponse(HttpStatusCode.InternalServerError, ex); ;
//				}
//			});

//        }

//		[HttpPost(Name = "Delete")]
//		public HttpResponseMessage Delete(HttpRequestMessage request, int clientId)
//		{
//			return GetHttpResponse(request, () =>
//			{
//				try
//				{
//					clientService.Delete(clientId, dbTransaction);

//					return request.CreateResponse(HttpStatusCode.OK); ;
//				}
//				catch (Exception ex)
//				{

//					return request.CreateResponse(HttpStatusCode.InternalServerError, ex); ;
//				}
//			});

//		}

//		[HttpGet(Name = "GetByID")]
//		public HttpResponseMessage GetByID(HttpRequestMessage request, Int32 id)
//		{
//			return GetHttpResponse(request, () =>
//			{
//				try
//				{
//					var response = clientService.GetByClientID(id);

//					return request.CreateResponse(HttpStatusCode.OK, response); ;
//				}
//				catch (Exception ex)
//				{

//					return request.CreateResponse(HttpStatusCode.InternalServerError, ex); ;
//				}
//			});
//		}

//		[HttpGet(Name = "GetByCustomerId")]
//		public HttpResponseMessage GetByCustomerId(HttpRequestMessage request, Int32 customerId)
//		{
//			return GetHttpResponse(request, () =>
//			{
//				try
//				{
//					var response = clientService.GetByCustomerId(customerId);

//					return request.CreateResponse(HttpStatusCode.OK, response); ;
//				}
//				catch (Exception ex)
//				{
//					return request.CreateResponse(HttpStatusCode.InternalServerError, ex); ;
//				}
//			});
//		}
//	}
//}











