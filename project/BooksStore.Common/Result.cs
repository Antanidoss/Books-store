using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooksStore.Common
{
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static Result Success()
        {
            return new Result(true, new string[] { });
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }

        public override string ToString()
        {
            string result = "";
            foreach(var p in Errors)
            {
                result += p + ". ";
            }
            return result;
        }
    }
}
