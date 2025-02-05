using Ardalis.Result;
using MediatR;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.WordUsages.Handlers;

internal class DeleteWordUsageHandler : IRequestHandler<DeleteWordUsageRequest, Result>
{

    private readonly IRepository<WordUsage> _wordUsagesRepository;

    public DeleteWordUsageHandler(IRepository<WordUsage> wordUsagesRepository)
    {
        _wordUsagesRepository = wordUsagesRepository;
    }

    public async Task<Result> Handle(DeleteWordUsageRequest request, CancellationToken cancellationToken)
    {
        var wordUsage = await _wordUsagesRepository.GetByIdAsync(request.Id);
        await _wordUsagesRepository.DeleteAsync(wordUsage!);
        return Result.Success();
    }

}