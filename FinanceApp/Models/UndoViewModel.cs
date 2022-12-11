namespace FinanceApp.Models
{
    public class UndoViewModel
    {
        public IEnumerable<CurrentPaymentViewModel> DeletedPayments { get; set; }

        public IEnumerable<PaymentTypeViewModel> DeletedPaymentTypes { get; set; }

    }
}
