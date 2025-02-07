using FluentValidation;
using MyVocabulary.Application.Commands.PhraseUsages;
using MyVocabulary.Application.Validators.Models;

namespace MyVocabulary.Application.Validators.Requests;

public class EditPhraseUsageRequestValidator : AbstractValidator<EditPhraseUsageRequest>
{
    public EditPhraseUsageRequestValidator()
    {
        RuleFor(x => x.Entity).SetValidator(new PhraseUsageDtoValidator());
    }
}