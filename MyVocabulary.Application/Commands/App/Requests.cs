using Ardalis.Result;
using MediatR;

namespace MyVocabulary.Application.Commands.App;

public record OnAppStartedRequest() : IRequest<Result>;