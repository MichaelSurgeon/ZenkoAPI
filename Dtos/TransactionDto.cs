using FluentValidation;
using ZenkoAPI.Helpers;

namespace ZenkoAPI.Dtos
{
    public record TransactionDto(
    string Name,
    string Amount,
    string Location,
    string Date,
    string Category);


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
