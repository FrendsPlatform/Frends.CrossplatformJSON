using System;

#pragma warning disable 1591

namespace Frends.JSON.Transform.Definitions
{
    /// <summary>
    /// Error details returned when a task failure occurs and ThrowErrorOnFailure is false.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Human-readable error message describing what went wrong.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// The original exception that caused the failure.
        /// </summary>
        public Exception AdditionalInfo { get; private set; }

        internal Error(string message, Exception additionalInfo)
        {
            Message = message;
            AdditionalInfo = additionalInfo;
        }
    }
}
