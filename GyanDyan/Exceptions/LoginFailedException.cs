﻿using System;

namespace GyanDyan.Exceptions
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException()
        {

        }

        public LoginFailedException(string message) : base(message)
        {

        }
    }
}
