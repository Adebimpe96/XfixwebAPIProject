using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicePlatform.Utility.ErrorHandling
{
    public static class ErrorMessage
    {
        public static class Generic
        {
            public static string SomethingWentWrong = "Somethign went wrong. Please try again later.";
            public static string UnableToProcess = "Unable to process request";
            public static string InvalidPayload = "Invalid payload";
            public static string TypeBadRequest = "Bad Request";
        }

        public static class Profile
        {
            public static string UserNotFound = "User not found";
        }
    }
}
