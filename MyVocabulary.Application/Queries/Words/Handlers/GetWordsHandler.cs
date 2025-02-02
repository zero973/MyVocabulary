using Ardalis.Result;
using MediatR;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.Words.Handlers;

public class GetWordsHandler : IRequestHandler<GetWordsRequest, Result<List<Word>>>
{

    private readonly IRepository<Word> _wordsRepository;

    public GetWordsHandler(IRepository<Word> wordsRepository)
    {
        _wordsRepository = wordsRepository;
    }

    public async Task<Result<List<Word>>> Handle(GetWordsRequest request, CancellationToken cancellationToken)
    {
        return await _wordsRepository.ListAsync(request.Specification);
    }

}