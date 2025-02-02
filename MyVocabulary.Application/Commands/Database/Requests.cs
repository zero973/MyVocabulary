using Ardalis.Result;
using MediatR;

namespace MyVocabulary.Application.Commands.Database;

public record MigrateDatabase() : IRequest<Result>;