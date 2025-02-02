using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Topics;
using MyVocabulary.Application.Queries.Words;
using MyVocabulary.Application.Specifications;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.WordUsages.Handlers;

public class GetWordUsagesHandler : IRequestHandler<GetWordUsagesRequest, Result<List<WordUsageDTO>>>
{

    private readonly IRepository<WordUsage> _wordUsagesRepository;
    private readonly ISender _sender;

    public GetWordUsagesHandler(IRepository<WordUsage> wordUsagesRepository, 
        ISender sender)
    {
        _wordUsagesRepository = wordUsagesRepository;
        _sender = sender;
    }

    public async Task<Result<List<WordUsageDTO>>> Handle(GetWordUsagesRequest request, CancellationToken cancellationToken)
    {
        var wordUsages = await _wordUsagesRepository.ListAsync(request.Specification);

        var words = (await _sender.Send(new GetWordsRequest(
                new WordsSpecification(
                    wordUsages.Select(x => x.NativeWordId)
                        .Union(wordUsages.Select(x => x.TranslationWordId))
                        .ToArray()))))
            .Value.ToDictionary(x => x.Id, x => x);

        List<TopicDTO> topics = await _sender.Send(new GetTopicsRequest(
            new TopicsSpecification(wordUsages.Select(x => x.TopicId).ToArray())));

        var result = wordUsages.Select(x => new WordUsageDTO(x.Id,
            topics.Single(y => y.Id == x.TopicId), words[x.NativeWordId], words[x.TranslationWordId], 
            x.NativeSentence, x.TranslatedSentence, x.PhotoUrl)).ToList();

        return result;
    }

}