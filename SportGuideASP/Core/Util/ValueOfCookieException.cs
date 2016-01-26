using System;
using System.Runtime.Serialization;

namespace SportGuideASP.Core.Util
{
    [Serializable]
    public class ValueOfCookieException : Exception
    {
        public ValueOfCookieException() { }
        public ValueOfCookieException(string message) : base(message) { }
        public ValueOfCookieException(string message, Exception inner) : base(message, inner) { }
        protected ValueOfCookieException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}