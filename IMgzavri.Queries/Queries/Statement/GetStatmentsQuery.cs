using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.Queries.Statement
{
    public record GetStatmentsQuery(
        int page,
        int offset,
        StatmentFilter StatmentFilter,
        SearchStatment SearchStatment,
        SortStatment SortStatment
        ) : Query;


    public record StatmentFilter(
        int? Seat,
        double? PriceTo,
        double? PriceFrom,
        DateTime? DateFrom,
        DateTime? DateTo,
        int? RoutFromId,
        int? RouteToId
        );

    public record SearchStatment(
        string FirstName,
        string LastName
        );

    public record SortStatment(
        bool DateUp,
        bool DateDown,
        bool PriceUp,
        bool PriceDown,
        bool SeatUp,
        bool SeatDown
        );
    
    
}
