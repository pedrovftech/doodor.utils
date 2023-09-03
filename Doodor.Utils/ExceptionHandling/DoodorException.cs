using System;
using System.Runtime.Serialization;

namespace Doodor.Utils.ExceptionHandling
{
    public class DoodorException : Exception
    {
        protected DoodorException() { }

        protected DoodorException(string message)
            : base(message) { }

        protected DoodorException(string message, Exception innerException)
            : base(message, innerException) { }

        protected DoodorException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}