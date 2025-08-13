using System;

namespace SchoolGrading.Exceptions
{
    public class MissingFieldException : Exception
    {
        public MissingFieldException(string message) : base(message) { }
    }
}
