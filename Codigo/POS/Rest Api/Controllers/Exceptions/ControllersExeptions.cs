namespace Rest_Api.Controllers.Exceptions
{
    public class Controller_ArgumentException : Exception
    {
        public Controller_ArgumentException(string message) : base(message) { }
    }

    public class Controller_ObjectAlreadyExistsException : Exception
    {
        public Controller_ObjectAlreadyExistsException(string message) : base(message) { }
    }

    public class Controller_ObjectHandlingException : Exception
    {
        public Controller_ObjectHandlingException(string message) : base(message) { }
    }
}
