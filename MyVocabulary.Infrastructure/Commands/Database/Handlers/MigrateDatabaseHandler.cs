using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyVocabulary.Application.Commands.Database;
using MyVocabulary.Infrastructure.Data;

namespace MyVocabulary.Infrastructure.Commands.Database.Handlers;

internal class MigrateDatabaseHandler(AppDbContext context, ILogger<MigrateDatabaseHandler> logger)
    : IRequestHandler<MigrateDatabase, Result>
{
    public async Task<Result> Handle(MigrateDatabase request, CancellationToken cancellationToken)
    {
        try
        {
            var migrations = (await context.Database.GetPendingMigrationsAsync(cancellationToken)).ToList();

            if (!migrations.Any())
                return Result.Success();

            logger.LogInformation("Unfulfilled migrations detected:"
                + Environment.NewLine + string.Join(Environment.NewLine, migrations));

            logger.LogInformation("Launching migrations...");
            await context.Database.MigrateAsync(cancellationToken);
            logger.LogInformation("Migrations completed");

            return Result.Success();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error when performing migrations");
            return Result.Error($"Error when performing migrations: {e.Message}");
        }
    }
}