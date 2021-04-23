using System;
using System.Threading.Tasks;
using APICall.Model;
using APICall.Services;
using EntityRepo.ContextInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace APICall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : BaseApiController
    {
        private readonly IUserAuthenticationActions _userAuth;
        public IAppUserAuthAPI _userAuthAPI;
        private IAuthenticateService _authService;
        private IConfiguration _iConfig;

        public UserAuthController(IUserAuthenticationActions userAuth,
                                  IAppUserAuthAPI userAuthAPI, IAuthenticateService authService, IConfiguration iConfig)
        {
            _userAuth = userAuth;
            _userAuthAPI = userAuthAPI;
            _authService = authService;
            _iConfig = iConfig;
        }

        [HttpPost]
        [Route("ValidateUserNameAndGetCredentials")]
        public IActionResult ValidateUserNameAndGetCredentials([FromBody] UserData userData)
        {
            IActionResult ret;
            try
            {
                var response = _authService.Authenticate(userData);
                if (response != null)
                {
                    _userAuthAPI.BearerToken = response.BearerToken;
                    _userAuthAPI.IsAuthenticated = response.IsAuthenticated;
                    _userAuthAPI.UserName = response.UserName;
                    ret = StatusCode(StatusCodes.Status200OK, _userAuthAPI);
                }
                else
                {

                    ret = StatusCode(StatusCodes.Status401Unauthorized, "Authorization has failed. Please contact administration");
                }
            }
            catch (Exception ex)
            {
                ret = StatusCode(StatusCodes.Status500InternalServerError, "An error has happened. Please contact administration stating the following: " + ex.Message);
            }
            return ret;
        }
    }
}
