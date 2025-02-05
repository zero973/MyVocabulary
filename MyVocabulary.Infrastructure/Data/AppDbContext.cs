using Microsoft.EntityFrameworkCore;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Infrastructure.Data;

public class AppDbContext : DbContext
{

    public DbSet<Topic> Topics { get; set; }

    public DbSet<UserAnswer> UserAnswers { get; set; }

    public DbSet<Word> Words { get; set; }

    public DbSet<WordUsage> WordUsages { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        SQLitePCL.Batteries_V2.Init();
        // dotnet tool update --global dotnet-ef
        // dotnet ef migrations add init -p MyVocabulary.Infrastructure -s MyVocabulary.UI
        // dotnet ef migrations add init -c AppDbContext --output-dir Migrations
        // dotnet ef database update -c AppDbContext
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.EnableSensitiveDataLogging();
#endif

        optionsBuilder.UseSeeding((context, _) =>
        {
            Seed(context);
            context.SaveChanges();
        });

        optionsBuilder.UseAsyncSeeding(async (context, _, _) => 
        {
            Seed(context);
            await context.SaveChangesAsync();
        });
    }

    private void Seed(DbContext context)
    {
        var engCulture = "en";
        var ruCulture = "ru";

        var joy = new Word("joy", engCulture);
        var outcome = new Word("outcome", engCulture);
        var consequence = new Word("consequence", engCulture);
        var admit = new Word("admit", engCulture);
        var prevent = new Word("prevent", engCulture);
        var illuminate = new Word("illuminate", engCulture);
        var investigation = new Word("investigation", engCulture);
        var investigate = new Word("investigate", engCulture);
        var consider = new Word("consider", engCulture);
        var promise = new Word("promise", engCulture);
        var kidnap = new Word("kidnap", engCulture);
        var hijacking = new Word("hijacking", engCulture);
        var coincidence = new Word("coincidence", engCulture);
        var disaster = new Word("disaster", engCulture);
        var suspicious = new Word("suspicious", engCulture);
        var demand = new Word("demand", engCulture);
        var besides = new Word("besides", engCulture);
        var obtain = new Word("obtain", engCulture);
        var envelope = new Word("envelope", engCulture);

        var joy_ru = new Word("радость", ruCulture);
        var outcome_ru = new Word("исход", ruCulture);
        var consequence_ru = new Word("последствие", ruCulture);
        var admit_ru = new Word("признавать", ruCulture);
        var prevent_ru = new Word("предотвращать", ruCulture);
        var illuminate_ru = new Word("освещать", ruCulture);
        var investigation_ru = new Word("расследование", ruCulture);
        var investigate_ru = new Word("расследовать", ruCulture);
        var consider_ru = new Word("рассматривать", ruCulture);
        var promise_ru = new Word("обещать", ruCulture);
        var kidnap_ru = new Word("похищение", ruCulture);
        var hijacking_ru = new Word("угон", ruCulture);
        var coincidence_ru = new Word("совпадение", ruCulture);
        var disaster_ru = new Word("катастрофа", ruCulture);
        var suspicious_ru = new Word("подозрительный", ruCulture);
        var demand_ru = new Word("требование", ruCulture);
        var besides_ru = new Word("кроме того", ruCulture);
        var obtain_ru = new Word("получать", ruCulture);
        var envelope_ru = new Word("конверт", ruCulture);

        context.Set<Word>().AddRange(joy, outcome, consequence, admit, prevent, illuminate, investigation, investigate, consider,
            promise, kidnap, hijacking, coincidence, disaster, suspicious, demand, besides, obtain, envelope,
            joy_ru, outcome_ru, consequence_ru, admit_ru, prevent_ru, illuminate_ru, investigation_ru, investigate_ru,
            consider_ru, promise_ru, kidnap_ru, hijacking_ru, coincidence_ru, disaster_ru, suspicious_ru,
            demand_ru, besides_ru, obtain_ru, envelope_ru);

        var noImageUrl = "https://sun9-58.userapi.com/impg/T2LyUzjz8C3DKtVoI7p6fo2edXNP04AOcYsDZQ/2Pj8k46yrqk.jpg?size=383x341&quality=95&sign=3f0a715c29c549d26b93b073a344df45";

        var wordUsages = new List<WordUsage>();
        var topic = new Topic(engCulture, ruCulture, "My new topic", "Test topic ...", noImageUrl, wordUsages);
        wordUsages = new List<WordUsage>()
        {
            new WordUsage(topic.Id, joy.Id, joy_ru.Id, "Sentence with joy word", "Предложение с использованием слова радость", noImageUrl),
            new WordUsage(topic.Id, outcome.Id, outcome_ru.Id, "Sentence with outcome word", "Предложение с использованием слова исход", noImageUrl),
            new WordUsage(topic.Id, consequence.Id, consequence_ru.Id, "Sentence with consequence word", "Предложение с использованием слова последствие", noImageUrl),
            new WordUsage(topic.Id, admit.Id, admit_ru.Id, "Sentence with admit word", "Предложение с использованием слова признавать", noImageUrl),
            new WordUsage(topic.Id, prevent.Id, prevent_ru.Id, "Sentence with prevent word", "Предложение с использованием слова предотвращать", noImageUrl),
            new WordUsage(topic.Id, illuminate.Id, illuminate_ru.Id, "Sentence with illuminate word", "Предложение с использованием слова освещать", noImageUrl),
            new WordUsage(topic.Id, investigation.Id, investigation_ru.Id, "Sentence with investigation word", "Предложение с использованием слова расследование", noImageUrl),
            new WordUsage(topic.Id, investigate.Id, investigate_ru.Id, "Sentence with investigate word", "Предложение с использованием слова расследовать", noImageUrl),
            new WordUsage(topic.Id, consider.Id, consider_ru.Id, "Sentence with consider word", "Предложение с использованием слова рассматривать", noImageUrl),
            new WordUsage(topic.Id, promise.Id, promise_ru.Id, "Sentence with promise word", "Предложение с использованием слова обещать", noImageUrl),
            new WordUsage(topic.Id, kidnap.Id, kidnap_ru.Id, "Sentence with kidnap word", "Предложение с использованием слова похищение", noImageUrl),
            new WordUsage(topic.Id, hijacking.Id, hijacking_ru.Id, "Sentence with hijacking word", "Предложение с использованием слова угон", noImageUrl),
            new WordUsage(topic.Id, coincidence.Id, coincidence_ru.Id, "Sentence with coincidence word", "Предложение с использованием слова совпадение", noImageUrl),
            new WordUsage(topic.Id, disaster.Id, disaster_ru.Id, "Sentence with disaster word", "Предложение с использованием слова катастрофа", noImageUrl),
            new WordUsage(topic.Id, suspicious.Id, suspicious_ru.Id, "Sentence with suspicious word", "Предложение с использованием слова подозрительный", noImageUrl),
            new WordUsage(topic.Id, demand.Id, demand_ru.Id, "Sentence with demand word", "Предложение с использованием слова требование", noImageUrl),
            new WordUsage(topic.Id, besides.Id, besides_ru.Id, "Sentence with besides word", "Предложение с использованием слова кроме того", noImageUrl),
            new WordUsage(topic.Id, obtain.Id, obtain_ru.Id, "Sentence with obtain word", "Предложение с использованием слова получать", noImageUrl),
            new WordUsage(topic.Id, envelope.Id, envelope_ru.Id, "Sentence with envelope word", "Предложение с использованием слова конверт", noImageUrl)
        };

        context.Set<Topic>().AddRange(topic);
        context.Set<WordUsage>().AddRange(wordUsages);
    }
}