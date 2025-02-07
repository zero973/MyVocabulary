using MediatR;
using MyVocabulary.Application.Models;

namespace MyVocabulary.Application.Queries.Languages;

/// <summary>
/// Returns all languages
/// </summary>
public record GetLanguagesRequest() : IRequest<Language[]>;

/// <summary>
/// Returns languages that have localization in app
/// </summary>
public record GetLocalizedLanguagesRequest() : IRequest<Language[]>;

/// <summary>
/// Returns all languages sorted by preference
/// </summary>
public record GetSortedLanguagesRequest() : IRequest<Language[]>;