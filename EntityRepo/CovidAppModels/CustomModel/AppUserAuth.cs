namespace EntityRepo.CovidAppModels.CustomModel
{
    public class AppUserAuth
    {
        public AppUserAuth(string userName, string bearerToken, bool isAuthenticated, bool canAccessInsertPatientPage,
                           bool canAccessEditPatientPage, string fullName)
        {
            UserName = userName;
            BearerToken = bearerToken;
            IsAuthenticated = isAuthenticated;
            CanAccessInsertPatientPage = canAccessInsertPatientPage;
            CanAccessEditPatientPage = canAccessEditPatientPage;
            FullName = fullName;
        }

        public AppUserAuth()
        {

        }

        public string UserName { get; set; }
        public string BearerToken { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool CanAccessInsertPatientPage { get; set; }
        public bool CanAccessEditPatientPage { get; set; }
        public string FullName { get; set; }
    }
}
