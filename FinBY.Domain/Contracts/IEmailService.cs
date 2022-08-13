using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Contracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipient, string body);
    }
}
