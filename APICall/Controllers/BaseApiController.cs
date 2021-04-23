using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICall.Controllers
{
    public class BaseApiController : Controller
    {
        protected IActionResult HandleException(Exception ex, string errorMsg)
        {
            IActionResult ret;
            ret = StatusCode(StatusCodes.Status500InternalServerError, new Exception(errorMsg));

            return ret;
        }
    }
}
