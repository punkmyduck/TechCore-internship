using FluentValidation;
using Domain.DTOs;

namespace BookService.Application.Validators
{
    public class CreateBookDtoFluentValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookDtoFluentValidator()
        {
            RuleFor(createBookDto => createBookDto.Title)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(createBookDto => createBookDto.YearPublished)
                .Must(b => (b > 1900) && b <= (DateTime.Now.Year));
        }
    }
}
