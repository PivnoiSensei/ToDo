namespace ToDoAPI.Infrastructure.Exceptions
{
    public class RequestValidationException : Exception
    {
        public IReadOnlyCollection<string> Errors { get; }

        public RequestValidationException(IEnumerable<string> errors)
            : base("Validation failed.")
        {
            Errors = errors.ToList();
        }
    }
}
