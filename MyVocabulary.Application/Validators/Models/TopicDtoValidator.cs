using FluentValidation;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Validators.Fields;

namespace MyVocabulary.Application.Validators.Models;

public class TopicDtoValidator : AbstractValidator<TopicDTO>
{
    public TopicDtoValidator()
    {
        RuleFor(x => x.Header)
            .MinimumLength(2)
            .WithMessage("Topic header must contain more than 2 symbols");

        RuleFor(x => x.Header)
            .MaximumLength(100)
            .WithMessage("Topic header maximum length must be 100");

        RuleFor(x => x.Description)
            .MinimumLength(2)
            .WithMessage("Topic description must contain more than 2 symbols");

        RuleFor(x => x.Description)
            .MaximumLength(300)
            .WithMessage("Topic description maximum length must be 300");

        RuleFor(x => x.PhotoUrl).SetValidator(new PhotoUrlValidator());

        RuleForEach(x => x.WordUsages)
            .SetValidator(new WordUsageDtoValidator());
    }
}