using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.Words.Handlers;

internal class GetWordHandler : IRequestHandler<GetWordRequest, Result<WordDTO>>
{

    private readonly IRepository<Word> _wordsRepository;

    public GetWordHandler(IRepository<Word> wordsRepository)
    {
        _wordsRepository = wordsRepository;
    }

    public async Task<Result<WordDTO>> Handle(GetWordRequest request, CancellationToken cancellationToken)
    {
        var word = await _wordsRepository.FirstOrDefaultAsync(request.Specification);
        if (word == default)
            return Result.NotFound("Didn't find word with such specification");
        return new WordDTO(word.Id, word.Value, new Language(word.Culture));
    }

}