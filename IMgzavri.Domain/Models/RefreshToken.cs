using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Domain.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }    

        public string? Token { get; set; }

        public string? JwtId { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? IsUsed { get; set; }

        public bool? IsInvalidated { get; set; }


        public Guid UserId { get; set; }
        public Users User { get; set; }


        public static void Validate(RefreshToken token)
        {

        }
    }
}
