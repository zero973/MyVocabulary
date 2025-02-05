using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Commands.Words;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.WordUsages;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.WordUsages.Handlers;

internal class EditWordUsageHandler : IRequestHandler<EditWordUsageRequest, Result<WordUsageDTO>>
{

    private readonly IRepository<WordUsage> _wordUsagesRepository;
    private readonly ISender _sender;

    public EditWordUsageHandler(IRepository<WordUsage> wordUsagesRepository, ISender sender)
    {
        _wordUsagesRepository = wordUsagesRepository;
        _sender = sender;
    }

    public async Task<Result<WordUsageDTO>> Handle(EditWordUsageRequest request, CancellationToken cancellationToken)
    {
        var wordUsage = await _wordUsagesRepository.GetByIdAsync(request.Entity.Id);
        var nativeWord = await _sender.Send(new GetOrCreateWordRequest(
             request.Entity.NativeWord.Value, request.Entity.NativeWord.Language));
        var translationWord = await _sender.Send(new GetOrCreateWordRequest(
            request.Entity.TranslationWord.Value, request.Entity.TranslationWord.Language));

        wordUsage!.Edit(request.Entity.Topic.Id,
            nativeWord.Value.Id,
            translationWord.Value.Id,
            request.Entity.NativeSentence,
            request.Entity.TranslatedSentence,
            request.Entity.PhotoUrl);

        await _wordUsagesRepository.UpdateAsync(wordUsage);
        var wordUsageDto = await _sender.Send(new GetWordUsageRequest(wordUsage.Id));

        return wordUsageDto;
    }
}