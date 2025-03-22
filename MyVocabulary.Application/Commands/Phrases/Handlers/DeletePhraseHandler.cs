using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Queries.PhraseUsages;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.Phrases.Handlers;

internal class DeletePhraseHandler(IRepository<Phrase> phrasesRepository, ISender sender)
    : IRequestHandler<DeletePhraseRequest, Result>
{
    public async Task<Result> Handle(DeletePhraseRequest request, CancellationToken cancellationToken)
    {
        var phraseUsages = await sender.Send(new GetPhraseUsagesRequest(
            new Specifications.PhraseUsagesSpecification(request.Id)));
        if (phraseUsages.Value.Any())
            return Result.Error($"Phrase or word is used in a phrase usage");

        var phrase = await phrasesRepository.GetByIdAsync(request.Id, cancellationToken);
        await phrasesRepository.DeleteAsync(phrase!, cancellationToken);

        return Result.Success();
    }
}