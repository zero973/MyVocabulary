using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyVocabulary.Application.Commands.Database;
using MyVocabulary.Infrastructure.Data;

namespace MyVocabulary.Infrastructure.Commands.Database.Handlers;

public class MigrateDatabaseHandler : IRequestHandler<MigrateDatabase, Result>
{

    private readonly AppDbContext _context;
    private readonly ILogger<MigrateDatabaseHandler> _logger;

    public MigrateDatabaseHandler(AppDbContext context, ILogger<MigrateDatabaseHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result> Handle(MigrateDatabase request, CancellationToken cancellationToken)
    {
        try
        {
            var migrations = (await _context.Database.GetPendingMigrationsAsync()).ToList();

            if (!migrations.Any())
                return Result.Success();

            _logger.LogInformation("Unfulfilled migrations detected:"
                + Environment.NewLine + string.Join(Environment.NewLine, migrations));

            _logger.LogInformation("Launching migrations...");
            await _context.Database.MigrateAsync(cancellationToken);
            _logger.LogInformation("Migrations completed");

            return Result.Success();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error when performing migrations");
            return Result.Error($"Error when performing migrations: {e.Message}");
        }
    }

}