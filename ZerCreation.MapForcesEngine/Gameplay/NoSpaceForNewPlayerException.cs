using System;
using System.Runtime.Serialization;

namespace ZerCreation.MapForcesEngine.Gameplay
{
    public class NoSpaceForNewPlayerException : Exception
    {
        public NoSpaceForNewPlayerException()
        {
        }

        public NoSpaceForNewPlayerException(string message) : base(message)
        {
        }

        public NoSpaceForNewPlayerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoSpaceForNewPlayerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
