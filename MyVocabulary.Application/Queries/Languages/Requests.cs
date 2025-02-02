using MediatR;
using MyVocabulary.Application.Models;

namespace MyVocabulary.Application.Queries.Languages;

public record GetLanguagesRequest() : IRequest<Language[]>;