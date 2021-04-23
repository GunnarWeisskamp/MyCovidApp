namespace APICall.Model
{
    public class ResultSimpleAPI : IResultSimpleAPI
    {
        public ResultSimpleAPI()
        {
        }
        public string EndMessage { get; set; }
        public bool IsError { get; set; }
    }
}
