using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.core.Patterns
{
    public class SpecificationException : Exception
    {
        public SpecificationException()
        {
        }

        public SpecificationException(string message)
            : base(message)
        {
        }

        public SpecificationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
