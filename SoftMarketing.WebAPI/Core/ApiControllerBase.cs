using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using SoftMarketing.Service;
using SoftMarketing.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

namespace SoftMarketing.WebAPI.Core
{
    
    public class ApiControllerBase : ControllerBase
    {
        //protected DbTransaction dbTransaction { get; set; }
        //protected HttpResponseMessage GetHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> codeToExecute)
        //{
        //    return codeToExecute.Invoke();
        //}
    }
}
