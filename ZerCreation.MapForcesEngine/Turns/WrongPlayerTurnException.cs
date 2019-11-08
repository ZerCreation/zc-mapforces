using System;
using System.Runtime.Serialization;

namespace ZerCreation.MapForcesEngine.Turns
{
    public class WrongPlayerTurnException : Exception
    {
        public WrongPlayerTurnException()
        {
        }

        public WrongPlayerTurnException(string message) : base(message)
        {
        }

        public WrongPlayerTurnException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongPlayerTurnException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
