using FluentValidation;
using MyVocabulary.Application.Commands.PhraseUsages;
using MyVocabulary.Application.Validators.Models;

namespace MyVocabulary.Application.Validators.Requests;

public class AddPhraseUsageRequestValidator : AbstractValidator<AddPhraseUsageRequest>
{
    public AddPhraseUsageRequestValidator()
    {
        RuleFor(x => x.Entity).SetValidator(new PhraseUsageDtoValidator());
    }
}