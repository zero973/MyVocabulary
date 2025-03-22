using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Enums;
using MyVocabulary.Application.Models;

namespace MyVocabulary.Application.Queries.TopicPractice;

public sealed record GetTopicPracticeResultRequest(TopicDTO Topic) : IRequest<Result<TopicPracticeResult>>;

public sealed record GetStudyVariantsRequest() : IRequest<Dictionary<StudyVariants, string>>;

/// <summary>
/// Represents a request to retrieve phrase usages for language practice.
/// Generates a set of phrases based on the selected study variant.
/// </summary>
/// <param name="Variant">The study strategy (random, fix mistakes, or learn new words).</param>
/// <param name="CountPhraseUsagesToStudy">The number of phrases to include in the study session.</param>
/// <param name="Topic">The topic containing the phrase usages to be studied.</param>
public sealed record GetPhraseUsagesForPracticeRequest(StudyVariants Variant, 
        uint CountPhraseUsagesToStudy, TopicDTO Topic) 
    : IRequest<Result<(List<PhraseUsageDTO> OriginalPhrases, List<PhraseUsageDTO> ReversedPhrases)>>;