

namespace IMgzavri.Domain.Models
{
    public class Users
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MobileNumber { get; set; }

        public string? IdNumber { get; set; }

        public string? NumberLicense { get; set; }

        public bool VerifyUser { get; set; }

        public Guid? PhotoId { get; set; }

        public DateTime CreateDate { get; set; }

        public int? RendomCode { get; set; }

        public double? Rate { get; set; }
        public int? RateCount { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<Car> Cars { get; set; }
        public ICollection<Statement> Statements { get; set; }
        public ICollection<Client> Clients { get; set; }
        public ICollection<ProfileImages> ProfileImages { get; set; }

    }

}
