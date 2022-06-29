using IMgzavri.Shared.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Shared.ExternalServices
{
    public static class GridDataExtention
    {
        public static GridData<T> GetGridData<T>(this IQueryable<T> orderdeQuery, int page, int offset)
        {
            var count = orderdeQuery.Count();
            var list = orderdeQuery.Skip((page - 1) * offset).Take(offset);
            var data = new GridData<T>()
            {
                Data = list,
                TotalItemCount = count,
                Page = page,
                Offset = offset,
                PageCount = count % offset == 0 ? count / offset : count / offset + 1
            };
            return data;
        }
       

        public static Object SeeMore<T>(this List<T> orderdeQuery, int page, int offset)
        {
            var count = orderdeQuery.Count();
            var list = orderdeQuery.Skip((page - 1) * offset).Take(offset).ToList();
            return new
            {
                data = list,
                totalCount = count,
            };
        }
    }
}
