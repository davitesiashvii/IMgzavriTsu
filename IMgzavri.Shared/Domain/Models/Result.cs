using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Shared.Domain.Models
{
    public class Result
    {
        public string Message { get; set; }

        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

        public object Response { get; set; }

        public ResultStatus Status { get; set; } = ResultStatus.Success;

        public static Result Success()
        {
            return new Result(ResultStatus.Success);
        }

        public static Result Error(string message)
        {
            return new Result(message, ResultStatus.Error);
        }

        public Result()
        {
        }

        public Result(ResultStatus status)
        {
            Status = status;
        }

        public Result(string message, ResultStatus status)
        {
            Message = message;
            Status = status;
        }
    }
}
