using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Domain.FileStorage
{
    public class FileSavingModel
    {
        public string Name { get; set; }

        public string Extension { get; set; }

        public string ContentType { get; set; }

        public long Size { get; set; }

        public byte[] File { get; set; }

        public Guid CreatorId { get; set; }

        public Guid CorrelationId { get; set; }

        public FileSavingModel(string name, string extension, string contentType, long size, byte[] file, Guid creatorId, Guid correlationId)
        {
            Name = name;
            Extension = extension;
            ContentType = contentType;
            Size = size;
            File = file;
            CreatorId = creatorId;
            CorrelationId = correlationId;
        }
    }
}
