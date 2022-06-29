using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Domain.FileStorage
{
    public class CheckResult
    {
        public bool Exists { get; set; }

        public string Path { get; set; }

        public IMgzavri.Domain.Models.File File { get; set; }
    }
}
