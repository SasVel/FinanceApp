namespace FinanceApp.Models
{
    public class UndoViewModel
    {
        public IEnumerable<PaymentViewModel> DeletedPayments { get; set; }

        public IEnumerable<PaymentTypeViewModel> DeletedPaymentTypes { get; set; }

    }
}
