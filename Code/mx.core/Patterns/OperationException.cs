using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.core.Patterns
{
    public class OperationException : Exception
    {
        public OperationException()
        {
        }

        public OperationException(string message)
            : base(message)
        {
        }

        public OperationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
