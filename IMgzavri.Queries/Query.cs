using IMgzavri.Shared.Domain.Models;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries
{
    public record Query : IQuery<Result>
    {
        public Guid Id { get; }
        public DateTimeOffset CreatedOn { get; }
        public string CreatedBy { get; }
    }
}
