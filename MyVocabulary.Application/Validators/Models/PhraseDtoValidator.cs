using FluentValidation;
using MyVocabulary.Application.Models;

namespace MyVocabulary.Application.Validators.Models;

public class PhraseDtoValidator : AbstractValidator<PhraseDTO>
{
    public PhraseDtoValidator()
    {
        RuleFor(x => x.Value)
            .MinimumLength(1)
            .WithMessage("Phrase or word must contain at least 1 character");

        RuleFor(x => x.Value)
            .MaximumLength(50)
            .WithMessage("Phrase or word must not exceed 50 characters");

        RuleFor(x => x.Value)
            .Must((phrase, value) => value.All(x => char.IsLetter(x) || x == ' ' || x == '-'))
            .WithMessage("Phrase or word can't contain numbers and special symbols (exclude whitespace and dash)");

        RuleFor(x => x.Language)
            .NotNull();
    }
}