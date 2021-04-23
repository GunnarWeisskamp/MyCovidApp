namespace APICall.Model
{
    public class AppUserAuthAPI : IAppUserAuthAPI
    {
        public AppUserAuthAPI()
        {
            UserName = "Not authorized";
            BearerToken = string.Empty;
        }
        public string UserName { get; set; }
        public string BearerToken { get; set; }
        public bool IsAuthenticated { get; set; }
        public string ErrorMsg { get; set; }
    }
}
