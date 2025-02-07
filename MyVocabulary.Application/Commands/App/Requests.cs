using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;

namespace MyVocabulary.Application.Commands.App;

public record LoadUserSettingsRequest() : IRequest<Result<UserSettings>>;

public record SaveUserSettingsRequest(UserSettings UserSettings) : IRequest<Result>;