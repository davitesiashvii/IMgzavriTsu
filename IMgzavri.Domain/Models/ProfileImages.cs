using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Domain.Models
{
    public class ProfileImages
    {
        public Guid Id { get; set; }
        public int Type { get; set; }
        public Guid ImageId { get; set; }

        public Guid UserId { get; set; }
        public Users User { get; set; }
    }
}
