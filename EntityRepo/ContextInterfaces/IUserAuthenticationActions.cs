using EntityRepo.CovidAppModels.CustomModel;

namespace EntityRepo.ContextInterfaces
{
    public interface IUserAuthenticationActions
    {
        AppUserAuth ValidateUserNameAndGetCredentials(string UserName, string Password);
    }
}
