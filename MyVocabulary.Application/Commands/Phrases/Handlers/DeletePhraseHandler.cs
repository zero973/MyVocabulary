using Ardalis.Result;
using MediatR;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.Phrases.Handlers;

internal class DeletePhraseHandler : IRequestHandler<DeletePhraseRequest, Result>
{

    private readonly IRepository<Phrase> _phrasesRepository;

    public DeletePhraseHandler(IRepository<Phrase> phrasesRepository)
    {
        _phrasesRepository = phrasesRepository;
    }

    public async Task<Result> Handle(DeletePhraseRequest request, CancellationToken cancellationToken)
    {
        var phrase = await _phrasesRepository.GetByIdAsync(request.Id);
        await _phrasesRepository.DeleteAsync(phrase!);
        return Result.Success();
    }

}