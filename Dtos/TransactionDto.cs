using FluentValidation;

namespace ZenkoAPI.Dtos
{
    public class TransactionDto
    {
        public string Name { get; set; }
        public string Amount { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public string Category { get; set; }
    }

    public class TransactionDTOValidator : AbstractValidator<TransactionDto>
    {
        public TransactionDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.Location).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
