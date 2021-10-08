using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.WebApi.Utils
{
    public class ValidException : Exception
    {
        public int ExceptionCode { get; set; } = 0;

        public ValidException(string message, int code = -1) : base(message)
        {
            this.ExceptionCode = code;
        }
    }
}
