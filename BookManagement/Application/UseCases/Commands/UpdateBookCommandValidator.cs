using FluentValidation;

namespace Application.UseCases.Commands;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Author).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ISBN).NotEmpty().MaximumLength(13);
        RuleFor(x => x.PublicationDate).NotEmpty();
        RuleFor(x => x.Id).NotEmpty().Must(BeAValidGuid).WithMessage("'Property Id' must be a valid GUID.");
    }
    
    private bool BeAValidGuid(Guid guid)
    {
        return Guid.TryParse(guid.ToString(), out _);
    }
    
}