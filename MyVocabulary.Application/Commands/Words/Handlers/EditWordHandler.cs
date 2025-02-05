using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.Words.Handlers;

internal class EditWordHandler : IRequestHandler<EditWordRequest, Result<WordDTO>>
{

    private readonly IRepository<Word> _wordsRepository;

    public EditWordHandler(IRepository<Word> wordsRepository)
    {
        _wordsRepository = wordsRepository;
    }

    public async Task<Result<WordDTO>> Handle(EditWordRequest request, CancellationToken cancellationToken)
    {
        var word = await _wordsRepository.GetByIdAsync(request.Entity.Id);
        word!.Edit(request.Entity.Value, request.Entity.Language.Value);

        await _wordsRepository.UpdateAsync(word);

        return new WordDTO(word.Id, word.Value, new Language(word.Culture));
    }

}