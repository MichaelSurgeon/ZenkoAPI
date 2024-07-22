namespace ZenkoAPI.Dtos
{
    public class BudgetSplitResponse
    {
        public decimal Needs { get; set; }
        public decimal Wants { get; set; }
        public decimal DebtsAndSavings { get; set; }
    }
}
