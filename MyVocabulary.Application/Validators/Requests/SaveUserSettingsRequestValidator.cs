using FluentValidation;
using MyVocabulary.Application.Commands.App;

namespace MyVocabulary.Application.Validators.Requests;

public class SaveUserSettingsRequestValidator : AbstractValidator<SaveUserSettingsRequest>
{
    public SaveUserSettingsRequestValidator()
    {
        RuleFor(x => x.UserSettings).NotNull();
    }
}