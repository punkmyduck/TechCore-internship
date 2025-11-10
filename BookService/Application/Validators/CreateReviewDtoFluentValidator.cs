using FluentValidation;
using Domain.DTOs;

namespace BookService.Application.Validators
{
    public class CreateReviewDtoFluentValidator : AbstractValidator<CreateReviewDto>
    {
        public CreateReviewDtoFluentValidator()
        {
            RuleFor(createReviewDto => createReviewDto.Rating)
                .Must(r => r >= 1 && r <= 10);

            RuleFor(createReviewDto => createReviewDto.Comment)
                .NotEmpty()
                .MaximumLength(1024);
        }
    }
}
