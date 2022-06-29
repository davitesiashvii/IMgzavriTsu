using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Domain.Models
{
    public class Statement
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public string? Description { get; set; }
        public int Seat { get; set; }
        public DateTime CreatedDate { get; set; }
        public double Price { get; set; }
        public int RoutFromId { get; set; }
        public int RouteToId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool? IsComplited { get; set; }

        public Guid CreateUserId { get; set; }
        public Users CreateUser { get;set; }
    }
}
