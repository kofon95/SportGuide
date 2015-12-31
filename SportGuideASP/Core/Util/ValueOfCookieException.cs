using System;

namespace SportGuideASP.Core.Util
{
    [Serializable]
    public class ValueOfCookieException : Exception
    {
        public ValueOfCookieException() { }
        public ValueOfCookieException(string message) : base(message) { }
        public ValueOfCookieException(string message, Exception inner) : base(message, inner) { }
        protected ValueOfCookieException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}