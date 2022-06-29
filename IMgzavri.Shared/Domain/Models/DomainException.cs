using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Shared.Domain.Models
{
    public enum ExceptionLevel
    {
        Info = 1,
        Warning = 2,
        Error = 3,
        Fatal = 4
    }

    public class DomainException : Exception
    {
        public ExceptionLevel Level { get; protected set; }

        public DomainException(string message, ExceptionLevel level, Exception innerException = null) : base(message, innerException)
        {
            Level = level;
        }
    }

    public class FileStorageException : Exception
    {
        public ExceptionLevel Level { get; protected set; }

        public FileStorageException(string message, ExceptionLevel level, Exception innerException = null) : base(message, innerException)
        {
            Level = level;
        }
    }
}
