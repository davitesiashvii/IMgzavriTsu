using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Models.ResponceModels
{
    public class SaveFileModel
    {
        public string Name { get; set; }

        public string Extension { get; set; }

        public string ContentType { get; set; }

        public long Size { get; set; }

        public string File { get; set; }
    }
}
