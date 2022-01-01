using System;

namespace GyanDyan.Exceptions
{
    public class DaysClashingException : Exception
    {
        public DaysClashingException()
        {

        }

        public DaysClashingException(string message) : base(message)
        {

        }
    }
}
