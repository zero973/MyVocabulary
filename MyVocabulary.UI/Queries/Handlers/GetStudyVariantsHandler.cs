using MediatR;
using MyVocabulary.Application.Enums;
using MyVocabulary.Application.Queries.TopicPractice;
using MyVocabulary.UI.Localization;

namespace MyVocabulary.UI.Queries.Handlers;

public class GetStudyVariantsHandler: IRequestHandler<GetStudyVariantsRequest, Dictionary<StudyVariants, string>>
{
    public async Task<Dictionary<StudyVariants, string>> Handle(GetStudyVariantsRequest request, 
        CancellationToken cancellationToken)
    {
        return await Task.FromResult(new Dictionary<StudyVariants, string>()
        {
            { StudyVariants.Random, AppResources.Random },
            { StudyVariants.FixMistakes, AppResources.FixMistakes },
            { StudyVariants.LearnNewWords, AppResources.LearnNewWords }
        });
    }
}