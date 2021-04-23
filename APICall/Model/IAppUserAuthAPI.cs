namespace APICall.Model
{
    public interface IAppUserAuthAPI
    {
        public string UserName { get; set; }
        public string BearerToken { get; set; }
        public bool IsAuthenticated { get; set; }
        public string ErrorMsg { get; set; }
    }
}
