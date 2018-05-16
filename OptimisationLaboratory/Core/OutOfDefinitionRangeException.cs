using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class OutOfDefinitionRangeException : Exception
    {
        List<int> functionsOutOfBounds;

        public OutOfDefinitionRangeException()
        {
            functionsOutOfBounds = new List<int>();
        }

        public OutOfDefinitionRangeException(string message) : base(message)
        {
            functionsOutOfBounds = new List<int>();
        }

        public OutOfDefinitionRangeException(string message, Exception innerException) : base(message, innerException)
        {
            functionsOutOfBounds = new List<int>();
        }

        protected OutOfDefinitionRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            functionsOutOfBounds = new List<int>();
        }

        public void AddFunction(int i)
        {
            functionsOutOfBounds.Add(i);
        }
    }
}
