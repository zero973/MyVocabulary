using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.Words.Handlers;

internal class GetWordsHandler : IRequestHandler<GetWordsRequest, Result<List<WordDTO>>>
{

    private readonly IRepository<Word> _wordsRepository;

    public GetWordsHandler(IRepository<Word> wordsRepository)
    {
        _wordsRepository = wordsRepository;
    }

    public async Task<Result<List<WordDTO>>> Handle(GetWordsRequest request, CancellationToken cancellationToken)
    {
        var words = await _wordsRepository.ListAsync(request.Specification);
        return words.Select(x => new WordDTO(x.Id, x.Value, new Language(x.Culture))).ToList();
    }

}