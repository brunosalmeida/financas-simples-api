namespace FS.Utils.Helpers
{
    using System;
    using Exceptions;

    public static class ValidationHelper
    {
        public static void Against<TException>(bool assertion, string message)
            where TException : Exception
        {
            Against<TException>(assertion, message, innerException: null);
        }
        public static void Against<TException>(bool assertion, string message, Exception innerException)
            where TException : Exception
        {
            if (assertion == false)
            {
                return;
            }

            throw (TException)typeof(TException).GetConstructor(new[] { typeof(string), typeof(Exception) })
                .Invoke(new object[] { message, innerException });
        }

        public static void Against<TException>(bool assertion, string errorCode, string message, Exception innerException = null)
            where TException : BaseException
        {
            if (assertion == false)
            {
                return;
            }

            throw (TException)typeof(TException).GetConstructor(new[] { typeof(string), typeof(string), typeof(Exception) })
                .Invoke(new object[] { errorCode, message, innerException });
        }

        public static void Against<TException>(string errorCode, string message)
            where TException : BaseException
        {
            Against<TException>(true, errorCode, message, null);
        }
    }
}