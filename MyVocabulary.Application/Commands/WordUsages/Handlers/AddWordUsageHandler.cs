using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Commands.Words;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.WordUsages;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.WordUsages.Handlers;

internal class AddWordUsageHandler : IRequestHandler<AddWordUsageRequest, Result<WordUsageDTO>>
{

    private readonly IRepository<WordUsage> _wordUsagesRepository;
    private readonly ISender _sender;

    public AddWordUsageHandler(IRepository<WordUsage> wordUsagesRepository, ISender sender)
    {
        _wordUsagesRepository = wordUsagesRepository;
        _sender = sender;
    }

    public async Task<Result<WordUsageDTO>> Handle(AddWordUsageRequest request, CancellationToken cancellationToken)
    {
        var nativeWord = await _sender.Send(new GetOrCreateWordRequest(
            request.Entity.NativeWord.Value, request.Entity.NativeWord.Language));
        var translationWord = await _sender.Send(new GetOrCreateWordRequest(
            request.Entity.TranslationWord.Value, request.Entity.TranslationWord.Language));

        var wordUsage = new WordUsage(request.Entity.Topic.Id,
            nativeWord.Value.Id,
            translationWord.Value.Id,
            request.Entity.NativeSentence, 
            request.Entity.TranslatedSentence, 
            request.Entity.PhotoUrl);

        var result = await _wordUsagesRepository.AddAsync(wordUsage);
        var wordUsageDTO = await _sender.Send(new GetWordUsageRequest(result.Id));

        return wordUsageDTO;
    }

}