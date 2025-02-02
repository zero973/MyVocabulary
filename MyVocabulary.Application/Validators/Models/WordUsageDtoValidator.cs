using FluentValidation;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Validators.Fields;

namespace MyVocabulary.Application.Validators.Models;

public class WordUsageDtoValidator : AbstractValidator<WordUsageDTO>
{
    public WordUsageDtoValidator()
    {
        RuleFor(x => x.NativeSentence)
            .MinimumLength(2)
            .WithMessage("Native sentence must contain at least 2 characters");

        RuleFor(x => x.NativeSentence)
            .MaximumLength(200)
            .WithMessage("Native sentence must not exceed 200 characters");

        RuleFor(x => x.TranslatedSentence)
            .MinimumLength(2)
            .WithMessage("Translated sentence must contain at least 2 characters");

        RuleFor(x => x.TranslatedSentence)
            .MaximumLength(200)
            .WithMessage("Translated sentence must not exceed 200 characters");

        RuleFor(x => x.PhotoUrl).SetValidator(new PhotoUrlValidator());
    }
}