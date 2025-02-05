using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Commands.Database;

namespace MyVocabulary.Application.Commands.App.Handlers;

internal class OnAppStartedHandler : IRequestHandler<OnAppStartedRequest, Result>
{

    private readonly ISender _sender;

    public OnAppStartedHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task<Result> Handle(OnAppStartedRequest request, CancellationToken cancellationToken)
    {
        return await _sender.Send(new MigrateDatabase());

        // todo set culture from app settings
    }
}