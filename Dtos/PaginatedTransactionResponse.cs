namespace ZenkoAPI.Dtos
{
    public class PaginatedTransactionResponse
    {
        public List<TransactionDto> Transactions { get; set; }
        public int TotalPages { get; set; }
    }
}
