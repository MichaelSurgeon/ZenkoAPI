using FluentValidation;
using ZenkoAPI.Helpers;

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
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("There has been no name found");
            RuleFor(x => x.Amount).NotEmpty()
                .WithMessage("There has been no amount found");
            RuleFor(x => x.Location).NotEmpty()
                .WithMessage("There has been no location found");
            RuleFor(x => x.Date).NotEmpty()
                .WithMessage("There has been no date found");
            RuleFor(x => x.Category.ToLower())
                .Must(AcceptedCategories.categories.Contains)
                .WithMessage("This category is not accepted");
        }
    }
}
