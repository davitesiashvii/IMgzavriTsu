using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Domain.FileStorage
{
    public class FileSavingResult
    {
        public Guid FileId { get; set; }
        public Guid CorrelationId { get; set; }

        public FileSavingResult(Guid fileId, Guid correlationId)
        {
            FileId = fileId;
            CorrelationId = correlationId;
        }
    }
}
