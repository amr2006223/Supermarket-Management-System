using System;

namespace Supermarket_Managment_System.Exception
{

    public class NotFoundException : IOException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}

