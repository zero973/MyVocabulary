using FluentValidation;
using MyVocabulary.Application.Commands.Topics;
using MyVocabulary.Application.Validators.Models;

namespace MyVocabulary.Application.Validators.Requests;

public class AddTopicRequestValidator : AbstractValidator<AddTopicRequest>
{
    public AddTopicRequestValidator()
    {
        RuleFor(x => x.Entity).SetValidator(new TopicDtoValidator());
    }
}