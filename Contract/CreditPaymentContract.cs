namespace ANIMALITOS_PHARMA_API.Contracts
{
    public class CreditPayment
    {
        public int Id { get; set; } = 0;

        public int CreditId { get; set; } = 0;

        public double PaymentAmount { get; set; } = 0.0;

        public DateTime? PaymentDate { get; set; } = null;

        public int? StatusId { get; set; } = 0;
    }

    public class CreditPaymentFilter
    {
        public int Id { get; set; } = 0;

        public int CreditId { get; set; } = 0;

        public double PaymentAmount { get; set; } = 0.0;

        public DateTime? PaymentDate { get; set; } = null;

        public int? StatusId { get; set; } = 0;

        public string SortColumn { get; set; } = "";

        public int PagingBegin { get; set; } = -1;

        public int PagingRange { get; set; } = -1;
    }
}
