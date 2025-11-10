using FluentValidation;
using Domain.DTOs;

namespace BookService.Application.Validators
{
    public class UpdateBookDtoFluentValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookDtoFluentValidator()
        {
            RuleFor(updateBookDto => updateBookDto.Title)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(updateBookDto => updateBookDto.YearPublished)
                .Must(b => (b > 1900) && b <= (DateTime.Now.Year));
        }
    }
}
