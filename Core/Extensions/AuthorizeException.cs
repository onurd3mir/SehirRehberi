using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    [Serializable]
    public class AuthorizeException : Exception
    {
        public AuthorizeException() { }
        public AuthorizeException(string message) : base(message) { } 
    }
}
