using FluentValidation;
using MyVocabulary.Application.Commands.Phrases;
using MyVocabulary.Application.Validators.Models;

namespace MyVocabulary.Application.Validators.Requests;

public class AddPhraseRequestValidator : AbstractValidator<AddPhraseRequest>
{
    public AddPhraseRequestValidator()
    {
        RuleFor(x => x.Entity).SetValidator(new PhraseDtoValidator());
    }
}