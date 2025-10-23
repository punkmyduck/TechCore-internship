using FluentValidation;
using task_1135.Application.DTOs;

namespace task_1135.Application.Validators
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
