using Ardalis.Result;
using MediatR;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.PhraseUsages.Handlers;

internal class DeletePhraseUsageHandler(IRepository<PhraseUsage> phraseUsagesRepository)
    : IRequestHandler<DeletePhraseUsageRequest, Result>
{
    public async Task<Result> Handle(DeletePhraseUsageRequest request, CancellationToken cancellationToken)
    {
        var phraseUsage = await phraseUsagesRepository.GetByIdAsync(request.Id, cancellationToken);
        await phraseUsagesRepository.DeleteAsync(phraseUsage!, cancellationToken);
        return Result.Success();
    }
}