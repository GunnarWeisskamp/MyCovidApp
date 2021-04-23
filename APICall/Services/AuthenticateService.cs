using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using APICall.AuthorisationHelper;
using APICall.Model;
using EntityRepo.ContextInterfaces;
using EntityRepo.CovidAppModels.CustomModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace APICall.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUserAuthenticationActions _userAuth;
        private IConfiguration _configuration { get; }
        private IJwtSettings _settings = null;

        public AuthenticateService(IConfiguration configuration, IUserAuthenticationActions userAuth, IJwtSettings settings)
        {
            _configuration = configuration;
            _userAuth = userAuth;
            _settings = settings;
        }

        public AppUserAuth Authenticate(UserData model)
        {
            var user = _userAuth.ValidateUserNameAndGetCredentials(model.Username, model.Password);
            // return null if user not found
            if (user == null || user.IsAuthenticated == false) return null;
            var token = GenerateJwtToken(user);
            // if we have no toke make the overall object unauthorized
            if (token != null)
            {
                user.BearerToken = token;
            }
            else
            {
                user = null;
            }
            return user;
        }

        private string GenerateJwtToken(AppUserAuth user)
        {
            _settings.Issuer = _configuration["JwtSettings:issuer"];
            _settings.Audience = _configuration["JwtSettings:audience"];
            _settings.MinutesToExpiration = Convert.ToInt32(_configuration["JwtSettings:minutesToExpiration"]);
            _settings.Key = _configuration["JwtSettings:key"];
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.Key));

            // Create standard JWT claims
            List<Claim> jwtClaims = new List<Claim>();
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Sub,
                user.UserName));
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()));

            // Add custom claims
            jwtClaims.Add(new Claim("isAuthenticated",
                user.IsAuthenticated.ToString().ToLower()));
            jwtClaims.Add(new Claim("CanAccessEditPatientPage",
                user.CanAccessEditPatientPage.ToString().ToLower()));
            jwtClaims.Add(new Claim("CanAccessInsertPatientPage",
                user.CanAccessInsertPatientPage.ToString().ToLower()));
            jwtClaims.Add(new Claim("fullName",
                user.FullName.ToString().ToLower()));

            var token = new JwtSecurityToken(
              issuer: _settings.Issuer,
              audience: _settings.Audience,
              claims: jwtClaims,
              notBefore: DateTime.UtcNow,
              expires: DateTime.UtcNow.AddMinutes(_settings.MinutesToExpiration),
              signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token); ;
        }
    }
}
