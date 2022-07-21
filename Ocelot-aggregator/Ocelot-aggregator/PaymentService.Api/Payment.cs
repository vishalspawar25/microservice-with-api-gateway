namespace PaymentService.Api
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
    }
}
