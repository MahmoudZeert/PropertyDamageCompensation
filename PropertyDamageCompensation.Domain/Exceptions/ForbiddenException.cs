﻿namespace PropertyDamageCompensation.Domain.Exceptions
{
    public class ForbiddenException : CustomException
    {
        public ForbiddenException(string message) : base(message)
        {
        }
    }
}
