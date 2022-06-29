using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.ViewModels.Profile
{
    public class UserInfoVm
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MobileNumber { get; set; }

        public string? IdNumber { get; set; }

        public string? NumberLicense { get; set; }

        public bool VerifyUser { get; set; }

        public string FileLink { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
