using FinanceApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Services.Contracts
{
    public interface IPaymentService
    {
        Task AddPaymentTypeAsync(PaymentType model);

        Task<IEnumerable<PaymentType>> GetAllPaymentTypes();

    }
}
