using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Topics;
using MyVocabulary.Application.Queries.Phrases;
using MyVocabulary.Application.Specifications;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.PhraseUsages.Handlers;

internal class GetPhraseUsageHandler : IRequestHandler<GetPhraseUsageRequest, Result<PhraseUsageDTO>>
{

    private readonly IRepository<PhraseUsage> _phraseUsagesRepository;
    private readonly ISender _sender;

    public GetPhraseUsageHandler(IRepository<PhraseUsage> phraseUsagesRepository, ISender sender)
    {
        _phraseUsagesRepository = phraseUsagesRepository;
        _sender = sender;
    }

    public async Task<Result<PhraseUsageDTO>> Handle(GetPhraseUsageRequest request, CancellationToken cancellationToken)
    {
        var phraseUsage = (await _phraseUsagesRepository.GetByIdAsync(request.Id))!;
        var topic = await _sender.Send(new GetTopicRequest(phraseUsage.TopicId));
        var phrases = (await _sender.Send(new GetPhrasesRequest(
                new PhrasesSpecification(phraseUsage.NativePhraseId, phraseUsage.TranslationPhraseId))))
            .Value.ToDictionary(x => x.Id, x => x);

        return new PhraseUsageDTO(phraseUsage.Id, 
            topic, 
            phrases[phraseUsage.NativePhraseId], 
            phrases[phraseUsage.TranslationPhraseId], 
            phraseUsage.NativeSentence, 
            phraseUsage.TranslatedSentence, 
            phraseUsage.PhotoUrl);
    }

}