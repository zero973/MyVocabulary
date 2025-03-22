using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Commands.App;
using MyVocabulary.Application.Enums;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Specifications;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.TopicPractice.Handlers;

/// <summary>
/// Handles the request to retrieve phrase usages for language practice.
/// Selects phrases based on the chosen study variant and generates a reversed copy.
/// </summary>
public class GetPhraseUsagesForPracticeHandler(IRepository<UserAnswer> userAnswersRepository, ISender sender) 
    : IRequestHandler<GetPhraseUsagesForPracticeRequest, Result<(List<PhraseUsageDTO> OriginalPhrases, List<PhraseUsageDTO> ReversedPhrases)>>
{
    
    public async Task<Result<(List<PhraseUsageDTO> OriginalPhrases, List<PhraseUsageDTO> ReversedPhrases)>> Handle(
        GetPhraseUsagesForPracticeRequest request, CancellationToken cancellationToken)
    {
        var random = new Random();
        var originalPhraseUsages = request.Topic.PhraseUsages;
        var originalPhrases = new List<PhraseUsageDTO>((int)request.CountPhraseUsagesToStudy);
        var reversedPhrases = new List<PhraseUsageDTO>((int)request.CountPhraseUsagesToStudy);
        var settings = (await sender.Send(new LoadUserSettingsRequest())).Value;
        
        var answers = await userAnswersRepository.ListAsync(
            new UserAnswersSpecification(originalPhraseUsages.Select(x => x.Id).ToArray(), 
                settings.CountMonthsValidAnswers));

        switch (request.Variant)
        {
            case StudyVariants.Random:
                RandomizePhraseUsages(originalPhraseUsages, request.CountPhraseUsagesToStudy, 
                    originalPhrases, reversedPhrases);
                break;
            case StudyVariants.FixMistakes:
                var wrongAnswers = answers.Where(x => !x.IsRight).ToList();
                var wrongPhraseUsages = originalPhraseUsages
                    .Where(x => wrongAnswers.Any(y => y.PhraseUsageId == x.Id)).ToList();
                
                // while wrongPhraseUsages.Count < originalPhraseUsages.Count
                // we must fill list with random phrase usages
                while (wrongPhraseUsages.Count < originalPhraseUsages.Count)
                {
                    var index = random.Next(0, originalPhraseUsages.Count);
                    if (wrongPhraseUsages.Any(x => x.Id == originalPhraseUsages[index].Id))
                        continue;
                    
                    wrongPhraseUsages.Add(originalPhraseUsages[index]);
                }
                
                RandomizePhraseUsages(wrongPhraseUsages, request.CountPhraseUsagesToStudy, 
                    originalPhrases, reversedPhrases);
                break;
            case StudyVariants.LearnNewWords:
                var touchedPhraseUsages = answers.Select(y => y.PhraseUsageId).ToList();
                var untouchedPhraseUsages = originalPhraseUsages
                    .Where(x => !touchedPhraseUsages.Contains(x.Id)).ToList();

                // while untouchedPhraseUsages.Count < originalPhraseUsages.Count
                // we must fill list with random phrase usages
                while (untouchedPhraseUsages.Count < originalPhraseUsages.Count)
                {
                    var index = random.Next(0, originalPhraseUsages.Count);
                    if (untouchedPhraseUsages.Any(x => x.Id == originalPhraseUsages[index].Id))
                        continue;
                    
                    untouchedPhraseUsages.Add(originalPhraseUsages[index]);
                }
                
                RandomizePhraseUsages(untouchedPhraseUsages, request.CountPhraseUsagesToStudy, 
                    originalPhrases, reversedPhrases);
                break;
            default: throw new NotImplementedException($"Not implemented variant '{request.Variant}'");
        }

        return await Task.FromResult((originalPhrases, reversedPhrases));
    }

    /// <summary>
    /// Randomly selects a specified number of phrase usages from the given list.
    /// Generates both original and reversed versions.
    /// </summary>
    /// <param name="phraseUsages">The list of available phrase usages.</param>
    /// <param name="countPhraseUsagesToStudy">The number of phrases to select.</param>
    /// <param name="originalPhrases">The list where original phrases will be stored.</param>
    /// <param name="reversedPhrases">The list where reversed phrases will be stored.</param>
    private void RandomizePhraseUsages(List<PhraseUsageDTO> phraseUsages, uint countPhraseUsagesToStudy, 
        List<PhraseUsageDTO> originalPhrases, List<PhraseUsageDTO> reversedPhrases)
    {
        var random = new Random();
        // add original phrase usages
        var indexes = new List<int>((int)countPhraseUsagesToStudy);
        while (originalPhrases.Count != countPhraseUsagesToStudy)
        {
            var index = random.Next(0, phraseUsages.Count);
            if (indexes.Contains(index))
                continue;
                    
            indexes.Add(index);
            originalPhrases.Add(phraseUsages[index]);
        }
        
        // add reversed phrase usages
        indexes.Clear();
        while (reversedPhrases.Count != countPhraseUsagesToStudy)
        {
            var index = random.Next(0, originalPhrases.Count);
            if (indexes.Contains(index))
                continue;
                    
            indexes.Add(index);
            reversedPhrases.Add(ReversPhraseUsage(originalPhrases[index]));
        }
    }

    /// <summary>
    /// Creates a reversed version of the given phrase usage, swapping native and translated text.
    /// </summary>
    /// <param name="topicPhraseUsage">The phrase usage to reverse.</param>
    /// <returns>A new instance of <see cref="PhraseUsageDTO"/> with swapped native and translated phrases.</returns>
    private PhraseUsageDTO ReversPhraseUsage(PhraseUsageDTO topicPhraseUsage)
    {
        var result = topicPhraseUsage.Clone();
        result.TranslationPhrase = topicPhraseUsage.NativePhrase;
        result.TranslatedSentence = topicPhraseUsage.NativeSentence;
        result.NativePhrase = topicPhraseUsage.TranslationPhrase;
        result.NativeSentence = topicPhraseUsage.TranslatedSentence;

        return result;
    }
    
}