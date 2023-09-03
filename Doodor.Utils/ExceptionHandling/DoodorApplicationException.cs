using System;
using System.Runtime.Serialization;

namespace Doodor.Utils.ExceptionHandling
{
    [Serializable]
    public class DoodorApplicationException : DoodorException
    {
        public DoodorApplicationException() { }

        public DoodorApplicationException(string message)
            : base(message) { }

        public DoodorApplicationException(string message, Exception innerException)
            : base(message, innerException) { }

        protected DoodorApplicationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}