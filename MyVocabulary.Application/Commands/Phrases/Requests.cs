using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;

namespace MyVocabulary.Application.Commands.Phrases;

/// <summary>
/// Add phrase
/// </summary>
public record AddPhraseRequest(PhraseDTO Entity) : IRequest<Result<PhraseDTO>>;

/// <summary>
/// Edit phrase
/// </summary>
public record EditPhraseRequest(PhraseDTO Entity) : IRequest<Result<PhraseDTO>>;

/// <summary>
/// Delete phrase
/// </summary>
public record DeletePhraseRequest(Guid Id) : IRequest<Result>;

/// <summary>
/// Return phrase or create it, if phrase doesn't exist
/// </summary>
public record GetOrCreatePhraseRequest(string Phrase, Language Language) : IRequest<Result<PhraseDTO>>;