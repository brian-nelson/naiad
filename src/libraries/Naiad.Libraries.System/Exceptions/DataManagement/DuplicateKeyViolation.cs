using System;

namespace Naiad.Libraries.System.Exceptions.DataManagement
{
    public class DuplicateKeyViolation : Exception
    {
        public DuplicateKeyViolation(){}

        public DuplicateKeyViolation(string message)
            : base(message)
        {

        }
    }
}
