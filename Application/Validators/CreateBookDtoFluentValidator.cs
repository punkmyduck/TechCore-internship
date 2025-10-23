using FluentValidation;
using task_1135.Application.DTOs;

namespace task_1135.Application.Validators
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
