using System;

namespace Doodor.Utils.Utilities.ExceptionHandling.ErrorProneActionHandler_
{
    public struct TryResult
    {
        public TryResult(bool succeeded, Exception error)
        {
            Succeeded = succeeded;
            Error = error;
        }

        public bool Succeeded { get; }
        public Exception Error { get; }
    }
}