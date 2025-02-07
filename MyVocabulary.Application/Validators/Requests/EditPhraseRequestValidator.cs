using FluentValidation;
using MyVocabulary.Application.Commands.Phrases;
using MyVocabulary.Application.Validators.Models;

namespace MyVocabulary.Application.Validators.Requests;

public class EditPhraseRequestValidator : AbstractValidator<EditPhraseRequest>
{
    public EditPhraseRequestValidator()
    {
        RuleFor(x => x.Entity).SetValidator(new PhraseDtoValidator());
    }
}