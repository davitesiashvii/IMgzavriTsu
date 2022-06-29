using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Domain.Models
{
    public class Car
    {
        public Guid Id { get; set; }

        public int MarckId { get; set; }

        public int ModelId { get; set; }

        public Guid? MainImageId { get; set; }

        public DateTime CreateDate { get; set; }



        public Guid UserId { get; set; }
        public Users User { get; set; }

        public ICollection<CarImage> CarImages { get; set; }
    }
}
