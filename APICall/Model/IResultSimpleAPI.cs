namespace APICall.Model
{
    public interface IResultSimpleAPI
    {
        public string EndMessage { get; set; }
        public bool IsError { get; set; }
    }
}
