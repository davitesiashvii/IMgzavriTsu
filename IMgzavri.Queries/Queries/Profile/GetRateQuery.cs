using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.Queries.Profile
{
    public record GetRateQuery(string UserId,string StatmentId):Query; 
}
