using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.PhraseUsages;
using MyVocabulary.Application.Specifications;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.UserAnswers.Handlers;

internal class GetUserAnswersHandler : IRequestHandler<GetUserAnswersRequest, Result<List<UserAnswerDTO>>>
{

    private readonly IRepository<UserAnswer> _userAnswersRepository;
    private readonly ISender _sender;

    public GetUserAnswersHandler(IRepository<UserAnswer> userAnswersRepository, 
        ISender sender)
    {
        _userAnswersRepository = userAnswersRepository;
        _sender = sender;
    }

    public async Task<Result<List<UserAnswerDTO>>> Handle(GetUserAnswersRequest request, CancellationToken cancellationToken)
    {
        var userAnswers = await _userAnswersRepository.ListAsync(request.Specification);
        List<PhraseUsageDTO> phraseUsages = await _sender.Send(new GetPhraseUsagesRequest(
            new PhraseUsagesSpecification(userAnswers
                .Select(x => x.PhraseUsageId).Distinct().ToArray())));

        var result = userAnswers.Select(x => new UserAnswerDTO(x.Id, 
            phraseUsages.Single(y => y.Id == x.PhraseUsageId), x.IsRight)).ToList();

        return result;
    }

}