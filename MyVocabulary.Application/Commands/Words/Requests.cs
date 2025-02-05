using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;

namespace MyVocabulary.Application.Commands.Words;

/// <summary>
/// Add word
/// </summary>
public record AddWordRequest(WordDTO Entity) : IRequest<Result<WordDTO>>;

/// <summary>
/// Edit word
/// </summary>
public record EditWordRequest(WordDTO Entity) : IRequest<Result<WordDTO>>;

/// <summary>
/// Delete word
/// </summary>
public record DeleteWordRequest(Guid Id) : IRequest<Result>;

/// <summary>
/// Return word or create it, if word doesn't exists
/// </summary>
public record GetOrCreateWordRequest(string Word, Language Language) : IRequest<Result<WordDTO>>;