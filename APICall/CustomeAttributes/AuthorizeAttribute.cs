using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using APICall.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace APICall.CustomeAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _authType;
        public IConfiguration _configuration { get; }
        public AuthorizeAttribute(string authType)
        {
            _authType = authType;
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json");
            _configuration = builder.Build();

        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token == null)
            {
                // not logged in or role not authorized
                context.Result = new JsonResult(new { message = "You are not authorized to do this action. Please contact admin for more information" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
                if (DecryptTokenAndGetClaim(token, _authType) == 0)
                {
                    context.Result = new JsonResult(new { message = "You are not authorized to do this action. Please contact admin for more information" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
                if (DecryptTokenAndGetClaim(token, _authType) == 3)
                {
                    context.Result = new JsonResult(new { message = "You log on has expired. Please log in again" }) { StatusCode = StatusCodes.Status419AuthenticationTimeout };
                }
            }
        }

        private int DecryptTokenAndGetClaim(string token, string authType)
        {
            int retValue = 0;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("JwtSettings:key").Value.ToString())),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var claim = jwtToken.Claims.First(x => x.Type == authType);
                retValue = (claim.Value == "true") ? 1 : 0;

            }
            catch (SecurityTokenExpiredException ex)
            {
                retValue = 3;
            }
            catch (Exception ex)
            {

            }

            return retValue;
        }
    }
}
