using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Utilities.Exceptions
{
    public class QLSVException : Exception
    {
        public QLSVException()
        {
        }

        public QLSVException(string message)
            : base(message)
        {
        }

        public QLSVException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
