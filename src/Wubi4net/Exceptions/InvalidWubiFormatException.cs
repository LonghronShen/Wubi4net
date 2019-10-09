using System;

namespace Wubi4net.Exceptions
{

    /// <summary>
    /// An exception class indicates the wrong combination of wubi output formats
    /// </summary>
    public class InvalidWubiFormatException
        : Exception
    {

        public InvalidWubiFormatException()
            : base()
        {
        }

        public InvalidWubiFormatException(string message)
            : base(message)
        {
        }

    }

}
