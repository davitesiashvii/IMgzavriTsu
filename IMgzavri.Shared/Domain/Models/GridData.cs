using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Shared.Domain.Models
{
    public class GridData<T>
    {
        public IQueryable<T> Data { get; set; }
        public int TotalItemCount { get; set; }
        public int Page { get; set; }
        public int Offset { get; set; }
        public int PageCount { get; set; }

    }
}
