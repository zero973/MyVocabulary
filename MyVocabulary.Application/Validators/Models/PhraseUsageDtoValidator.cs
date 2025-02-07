using FluentValidation;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Validators.Fields;

namespace MyVocabulary.Application.Validators.Models;

public class PhraseUsageDtoValidator : AbstractValidator<PhraseUsageDTO>
{
    public PhraseUsageDtoValidator()
    {
        RuleFor(x => x.NativePhrase).SetValidator(new PhraseDtoValidator());

        RuleFor(x => x.TranslationPhrase).SetValidator(new PhraseDtoValidator());

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

        RuleFor(x => x.NativePhrase)
            .Must((phraseUsage, nativePhrase) => 
            {
                return !nativePhrase.Equals(phraseUsage.TranslationPhrase);
            })
            .WithMessage("Native and translation phrase must be different");

        RuleFor(x => x.NativePhrase.Language.Value)
            .Equal(x => x.Topic.CultureFrom.Value)
            .WithMessage("Native phrase must have same culture with topic");

        RuleFor(x => x.TranslationPhrase.Language.Value)
            .Equal(x => x.Topic.CultureTo.Value)
            .WithMessage("Phrase translation must have same culture with topic");
    }
}