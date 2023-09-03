﻿namespace Rest_Api.Services.Exceptions
{
    public class Service_ArgumentException : Exception
    {
        public Service_ArgumentException(string message) : base(message) { }
    }

    public class Service_ObjectAlreadyExistsException : Exception
    {
        public Service_ObjectAlreadyExistsException(string message) : base(message) { }
    }

    public class Service_ObjectHandlingException : Exception
    {
        public Service_ObjectHandlingException(string message) : base(message) { }
    }
}
