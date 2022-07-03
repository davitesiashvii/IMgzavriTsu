using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Domain.Models
{
    public class Client
    {
        public Guid Id { get; set; }
        public int ReservedSeat { get; set; }

        public bool ClientStart { get; set; }

        public Guid UserId { get; set; }
        //public Users User { get; set; }

        public Guid StatmentId { get; set; }
        //public Statement Statement { get; set; }
    }
}
