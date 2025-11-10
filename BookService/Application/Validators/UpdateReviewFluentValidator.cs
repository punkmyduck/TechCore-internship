using FluentValidation;
using Domain.DTOs;

namespace BookService.Application.Validators
{
    public class UpdateReviewFluentValidator : AbstractValidator<UpdateReviewDto>
    {
        public UpdateReviewFluentValidator()
        {
            RuleFor(u => u.Rating)
                .Must(r => r >= 1 && r <= 10);
            RuleFor(u => u.Comment)
                .NotEmpty()
                .MaximumLength(1024);
        }
    }
}
