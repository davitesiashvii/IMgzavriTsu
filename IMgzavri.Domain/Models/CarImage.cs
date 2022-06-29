using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Domain.Models
{
    public class CarImage
    {
        public Guid Id { get; set; }

        public Guid ImageId { get; set; }

        public Guid CarId { get; set; }
        public Car Car { get; set; }
    }
}
