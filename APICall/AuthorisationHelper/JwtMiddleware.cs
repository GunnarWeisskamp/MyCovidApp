using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICall.Model;
using APICall.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace APICall.AuthorisationHelper
{
    public class JwtMiddleware : IJwtMiddleWare
    {
        private readonly RequestDelegate _next;
        public IConfiguration _configuration { get; }
        private IJwtSettings _settings = null;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration, IJwtSettings settings)
        {
            _next = next;
            _configuration = configuration;
            _settings = settings;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                AttachUserToContext(context, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                _settings.Key = _configuration["JwtSettings:key"];
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_settings.Key);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var canAccessEditPatientPage = jwtToken.Claims.First(x => x.Type == "CanAccessEditPatientPage").Value;
                var canAccessInsertPatientPage = jwtToken.Claims.First(x => x.Type == "CanAccessInsertPatientPage").Value;

                // attach user to context on successful jwt validation
                context.Session.SetString("CanAccessEditPatientPage", canAccessEditPatientPage);
                context.Session.SetString("CanAccessInsertPatientPage", canAccessInsertPatientPage);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }

        void IJwtMiddleWare.AttachUserToContext(HttpContext context, string token)
        {
            throw new NotImplementedException();
        }
    }
}