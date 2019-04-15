using System;
using System.Runtime.Serialization;

namespace PeriodicTable.Model.Support
{
    [Serializable]
    public class PeriodicTableException : Exception
    {
        public PeriodicTableException() : base()
        {

        }

        public PeriodicTableException(string message) : base(message)
        {

        }

        public PeriodicTableException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public PeriodicTableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
