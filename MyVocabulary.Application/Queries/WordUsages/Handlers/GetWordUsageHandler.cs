using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Topics;
using MyVocabulary.Application.Queries.Words;
using MyVocabulary.Application.Specifications;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.WordUsages.Handlers;

internal class GetWordUsageHandler : IRequestHandler<GetWordUsageRequest, Result<WordUsageDTO>>
{

    private readonly IRepository<WordUsage> _wordUsagesRepository;
    private readonly ISender _sender;

    public GetWordUsageHandler(IRepository<WordUsage> wordUsagesRepository, ISender sender)
    {
        _wordUsagesRepository = wordUsagesRepository;
        _sender = sender;
    }

    public async Task<Result<WordUsageDTO>> Handle(GetWordUsageRequest request, CancellationToken cancellationToken)
    {
        var wordUsage = (await _wordUsagesRepository.GetByIdAsync(request.Id))!;
        var topic = await _sender.Send(new GetTopicRequest(wordUsage.TopicId));
        var words = (await _sender.Send(new GetWordsRequest(
                new WordsSpecification(wordUsage.NativeWordId, wordUsage.TranslationWordId))))
            .Value.ToDictionary(x => x.Id, x => x);

        return new WordUsageDTO(wordUsage.Id, 
            topic, 
            words[wordUsage.NativeWordId], 
            words[wordUsage.TranslationWordId], 
            wordUsage.NativeSentence, 
            wordUsage.TranslatedSentence, 
            wordUsage.PhotoUrl);
    }
}