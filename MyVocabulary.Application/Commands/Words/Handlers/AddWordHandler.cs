using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.Words.Handlers;

internal class AddWordHandler : IRequestHandler<AddWordRequest, Result<WordDTO>>
{

    private readonly IRepository<Word> _wordsRepository;

    public AddWordHandler(IRepository<Word> wordsRepository)
    {
        _wordsRepository = wordsRepository;
    }

    public async Task<Result<WordDTO>> Handle(AddWordRequest request, CancellationToken cancellationToken)
    {
        var word = await _wordsRepository.AddAsync(
            new Word(request.Entity.Value, request.Entity.Language.Value));

        return new WordDTO(word.Id, word.Value, new Language(word.Culture));
    }

}