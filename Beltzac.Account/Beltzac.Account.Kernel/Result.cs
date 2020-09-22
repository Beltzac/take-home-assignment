using System;
using System.Collections.Generic;

namespace Beltzac.Account.Kernel
{
    public class Result
    {
        public List<Error> Errors { get; set; }
        public bool IsOK => Errors.Count == 0;

        public Result()
        {
            Errors = new List<Error>();
        }

        public void Merge(Result result)
        {
            Errors.AddRange(result.Errors);
        }
    }

    public class Result<T> : Result
    {
        public T Data { get; set; }
    }
}
