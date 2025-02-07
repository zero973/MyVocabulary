using FluentValidation;
using MyVocabulary.Application.Commands.Phrases;

namespace MyVocabulary.Application.Validators.Requests;

public class GetOrCreatePhraseRequestValidator : AbstractValidator<GetOrCreatePhraseRequest>
{
    public GetOrCreatePhraseRequestValidator()
    {
        RuleFor(x => x.Phrase)
            .MinimumLength(1)
            .WithMessage("Phrase or word must contain at least 1 character");

        RuleFor(x => x.Phrase)
            .MaximumLength(50)
            .WithMessage("Phrase or word must not exceed 50 characters");

        RuleFor(x => x.Phrase)
            .Must((phrase, value) => value.All(char.IsLetter))
            .WithMessage("Phrase or word can't contain numbers and special symbols");

        RuleFor(x => x.Language)
            .NotNull();
    }
}