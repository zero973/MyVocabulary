using Ardalis.Result;
using MediatR;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.PhraseUsages.Handlers;

internal class DeletePhraseUsageHandler : IRequestHandler<DeletePhraseUsageRequest, Result>
{

    private readonly IRepository<PhraseUsage> _phraseUsagesRepository;

    public DeletePhraseUsageHandler(IRepository<PhraseUsage> phraseUsagesRepository)
    {
        _phraseUsagesRepository = phraseUsagesRepository;
    }

    public async Task<Result> Handle(DeletePhraseUsageRequest request, CancellationToken cancellationToken)
    {
        var phraseUsage = await _phraseUsagesRepository.GetByIdAsync(request.Id);
        await _phraseUsagesRepository.DeleteAsync(phraseUsage!);
        return Result.Success();
    }

}