using FluentValidation;

namespace MyVocabulary.Application.Validators.Fields;

public class PhotoUrlValidator : AbstractValidator<string>
{
    public PhotoUrlValidator()
    {
        RuleFor(x => x)
            .Must((entity, value) =>
            {
                if (string.IsNullOrWhiteSpace(value))
                    return true;

                return Uri.TryCreate(value, UriKind.Absolute, out Uri uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            })
            .WithMessage("Photo URL must be a valid web address (starting with http:// or https://)");
    }
}