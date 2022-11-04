namespace FinanceApp.Data
{
    public static class DataConstants
    {
        public static class CurrentBudget
        {
            public const int NameMaxLen = 50;
            public const int NameMinLen = 3;

            public const int DescriptionMaxLen = 400;
            public const int DescriptionMinLen = 10;

            public const decimal CostMinRange = 0.1m;
        }

        public static class PaymentType
        {
            public const int NameMaxLen = 20;
            public const int NameMinLen = 3;
        }
    }
}
