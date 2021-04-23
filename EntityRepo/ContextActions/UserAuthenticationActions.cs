using System.Linq;
using EntityRepo.ContextInterfaces;
using EntityRepo.CovidAppModels;
using EntityRepo.CovidAppModels.CustomModel;

namespace EntityRepo.ContextActions
{
    public class UserAuthenticationActions : CovidAppContext, IUserAuthenticationActions
    {
        public AppUserAuth ValidateUserNameAndGetCredentials(string UserName, string Password)
        {
            PatientDetailsAndAdressesCustom patDetailsAndAdd = new PatientDetailsAndAdressesCustom();
            AppUserAuth appUser = new AppUserAuth();

            using (var context = new CovidAppContext())
            {
                var userCreds = (context.Users.Where(a => a.Username == UserName && a.Password == Password).SingleOrDefault() != null) ? context.Users.Where(a => a.Username == UserName && a.Password == Password).SingleOrDefault() : null;
                if (userCreds != null)
                {
                    var userClaims = context.UserClaims.Where(a => a.UserIdFk == userCreds.UserId).ToList();
                    if (userClaims == null)
                    {
                        appUser.UserName = userCreds.Username;
                        appUser.IsAuthenticated = true;
                        appUser.CanAccessEditPatientPage = false;
                        appUser.CanAccessInsertPatientPage = false;
                        appUser.FullName = userCreds.FullName;
                    }
                    else
                    {
                        appUser.UserName = userCreds.Username;
                        appUser.FullName = userCreds.FullName;
                        appUser.IsAuthenticated = true;
                        userClaims.ForEach(el =>
                        {
                            if (el.ClaimType == "CanAccessEditPatientPage" && el.ClaimValue == true)
                            {
                                appUser.CanAccessEditPatientPage = true;
                            }
                            if (el.ClaimType == "CanAccessInsertPatientPage" && el.ClaimValue == true)
                            {
                                appUser.CanAccessInsertPatientPage = true;
                            }
                        });
                    }
                }
                else
                {
                    appUser.IsAuthenticated = false;
                }
            }

            return appUser;
        }
    }
}
