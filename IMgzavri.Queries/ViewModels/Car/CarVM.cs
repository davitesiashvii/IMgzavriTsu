using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.ViewModels.Car
{
    public class CarVM
    {
        public Guid CarId { get; set; }
        public string Marck { get; set; }
        public string Model { get; set; }
        public DateTime CreatedDate { get; set; }
        public string MainImageLink { get; set; }
        public List<string> Images { get; set; }
    }
}
