using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.ViewModels.Statment
{
    public class StatmentVm
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public int Seat { get; set; }
        public Double Price { get; set; }
        public string RoutFrom { get; set; }
        public string RouteTo { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool? IsComplited { get; set; }

        public Guid CreateUserId { get; set; }

        public string ImageLink { get; set; }

    }
}
