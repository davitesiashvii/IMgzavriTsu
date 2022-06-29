using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Shared.Domain.Models
{
    public class MailRequest
    {
        public string Body { get; set; }
        public string Mail { get; set; }
        //public List<IFormFile> Attachments { get; set; }
    }
}
