using APICall.Model;
using EntityRepo.CovidAppModels.CustomModel;

namespace APICall.Services
{
    public interface IAuthenticateService
    {
        AppUserAuth Authenticate(UserData model);
    }
}
