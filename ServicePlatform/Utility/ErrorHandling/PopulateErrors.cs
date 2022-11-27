using ServicePlatform.DTO.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicePlatform.Utility.ErrorHandling
{
    public class PopulateErrors
    {
        public Error PopulateError(int code, string message, string type)
        {
            return new Error()
            {
                Code = code,
                Message = message,
                Type = type,
            };
        }
    }

}
