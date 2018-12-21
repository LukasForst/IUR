using System;

namespace IurGoogleApi.Extensions
{
    public class GoogleApiException : Exception
    {
        public GoogleApiException(Exception originalException) : base(originalException.Message, originalException)
        {
            OriginalException = originalException;
        }

        public Exception OriginalException { get; }
    }
}