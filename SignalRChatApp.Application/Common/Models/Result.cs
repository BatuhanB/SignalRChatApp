namespace SignalRChatApp.Application.Common.Models
{
    public class Result
    {
        public Result(bool status,IEnumerable<string> errors)
        {
            Status = status;
            Errors = errors.ToArray();
        }

        public bool Status { get; set; }
        public string[] Errors { get; set; }

        public static Result Success()
        {
            return new Result(true, Array.Empty<string>());
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }
}
