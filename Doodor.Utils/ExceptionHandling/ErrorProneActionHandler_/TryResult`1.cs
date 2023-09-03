using System;

namespace Doodor.Utils.Utilities.ExceptionHandling
{
    public struct TryResult<T>
    {
        public TryResult(bool succeeded, T value, Exception error)
        {
            Succeeded = succeeded;
            Value = value;
            Error = error;
        }

        public bool Succeeded { get; private set; }
        public T Value { get; private set; }
        public Exception Error { get; private set; }
    }
}