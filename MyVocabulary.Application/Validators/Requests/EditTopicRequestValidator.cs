using FluentValidation;
using MyVocabulary.Application.Commands.Topics;
using MyVocabulary.Application.Validators.Models;

namespace MyVocabulary.Application.Validators.Requests;

public class EditTopicRequestValidator : AbstractValidator<EditTopicRequest>
{
    public EditTopicRequestValidator()
    {
        RuleFor(x => x.Entity).SetValidator(new TopicDtoValidator());
    }
}