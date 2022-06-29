using IMgzavri.Shared.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Shared.Contracts
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
