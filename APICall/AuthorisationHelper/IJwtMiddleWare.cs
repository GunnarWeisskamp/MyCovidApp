using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace APICall.AuthorisationHelper
{
    public interface IJwtMiddleWare
    {
        Task Invoke(HttpContext context);
        void AttachUserToContext(HttpContext context, string token);
    }
}
