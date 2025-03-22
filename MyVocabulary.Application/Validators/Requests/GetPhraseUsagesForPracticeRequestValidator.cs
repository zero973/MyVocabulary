using FluentValidation;
using MyVocabulary.Application.Queries.TopicPractice;

namespace MyVocabulary.Application.Validators.Requests;

public class GetPhraseUsagesForPracticeRequestValidator : AbstractValidator<GetPhraseUsagesForPracticeRequest>
{
    public GetPhraseUsagesForPracticeRequestValidator()
    {
        RuleFor(x => x.Topic).NotNull();
        
        RuleFor(x => x.CountPhraseUsagesToStudy)
            .Must((request, value) => value <= request.Topic.PhraseUsages.Count)
            .WithMessage("You cant write bigger count than count of phrase usages in this topic");
        
        RuleFor(x => x.CountPhraseUsagesToStudy)
            .GreaterThan((uint)0)
            .WithMessage("Count of phrase usages can't be zero");
    }
}