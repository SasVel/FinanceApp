﻿using FinanceApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Core.Contracts
{
    /// <summary>
    /// An interface for the PaymentService class
    /// </summary>
    public interface IPaymentService
    {
        Task AddPaymentTypeAsync(PaymentType model);

        Task<IEnumerable<PaymentType>> GetAllPaymentTypes();


    }
}
