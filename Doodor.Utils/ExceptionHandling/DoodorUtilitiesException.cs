using System;
using System.Runtime.Serialization;

namespace Doodor.Utils.ExceptionHandling
{
    [Serializable]
    public class DoodorUtilitiesException : DoodorException
    {
        public DoodorUtilitiesException() { }

        public DoodorUtilitiesException(string message)
            : base(message) { }

        public DoodorUtilitiesException(string message, Exception innerException)
            : base(message, innerException) { }

        protected DoodorUtilitiesException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}