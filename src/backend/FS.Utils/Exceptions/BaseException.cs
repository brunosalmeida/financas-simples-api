namespace FS.Utils.Exceptions
{
    using System;

    public abstract class BaseException : Exception
    {
        public string ErrorCode { get; set; }

        public BaseException()
        {
        }

        public BaseException(string message)
            : base(message)
        {
        }

        public BaseException(string message, Exception inner)
            : base(message, inner)
        {
            this.ErrorCode = message;
        }

        public BaseException(string errorCode, string message, Exception inner)
            : this(message, inner)
        {
            this.ErrorCode = errorCode;
        }
    }
}