namespace FinanceApp.Models
{
    public class DashboardViewModel
    {
        public decimal FullBudget { get; set; }

        public decimal CurrentBudget { get; set; }

        public decimal EstimatedBudget { get; set; }

        public string Currency { get; set; }

        public IEnumerable<PaymentTypeViewModel> PaymentsInfo { get; set; }
    }
}
