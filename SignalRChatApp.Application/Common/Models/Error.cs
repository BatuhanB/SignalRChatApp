namespace SignalRChatApp.Application.Common.Models
{
    public class Error
    {
        public List<string>? Errors { get; private set; }
        public bool IsShow { get; private set; }

        public Error()
        {
            Errors = new List<string>();
        }
        public Error(string error, bool isShow)
        {
            Errors.Add(error);
            isShow = true;
        }
        public Error(List<string> errors, bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }
    }
}
