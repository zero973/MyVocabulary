using Ardalis.Result;
using MediatR;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.Words.Handlers;

internal class DeleteWordHandler : IRequestHandler<DeleteWordRequest, Result>
{

    private readonly IRepository<Word> _wordsRepository;

    public DeleteWordHandler(IRepository<Word> wordsRepository)
    {
        _wordsRepository = wordsRepository;
    }

    public async Task<Result> Handle(DeleteWordRequest request, CancellationToken cancellationToken)
    {
        var word = await _wordsRepository.GetByIdAsync(request.Id);
        await _wordsRepository.DeleteAsync(word!);
        return Result.Success();
    }

}