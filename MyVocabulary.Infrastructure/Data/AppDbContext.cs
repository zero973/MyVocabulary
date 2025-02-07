using Microsoft.EntityFrameworkCore;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Infrastructure.Data;

public class AppDbContext : DbContext
{

    public DbSet<Topic> Topics { get; set; }

    public DbSet<UserAnswer> UserAnswers { get; set; }

    public DbSet<Phrase> Phrases { get; set; }

    public DbSet<PhraseUsage> PhraseUsages { get; set; }

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

        var joy = new Phrase("joy", engCulture);
        var outcome = new Phrase("outcome", engCulture);
        var consequence = new Phrase("consequence", engCulture);
        var admit = new Phrase("admit", engCulture);
        var prevent = new Phrase("prevent", engCulture);
        var illuminate = new Phrase("illuminate", engCulture);
        var investigation = new Phrase("investigation", engCulture);
        var investigate = new Phrase("investigate", engCulture);
        var consider = new Phrase("consider", engCulture);
        var promise = new Phrase("promise", engCulture);
        var kidnap = new Phrase("kidnap", engCulture);
        var hijacking = new Phrase("hijacking", engCulture);
        var coincidence = new Phrase("coincidence", engCulture);
        var disaster = new Phrase("disaster", engCulture);
        var suspicious = new Phrase("suspicious", engCulture);
        var demand = new Phrase("demand", engCulture);
        var besides = new Phrase("besides", engCulture);
        var obtain = new Phrase("obtain", engCulture);
        var envelope = new Phrase("envelope", engCulture);

        var joy_ru = new Phrase("радость", ruCulture);
        var outcome_ru = new Phrase("исход", ruCulture);
        var consequence_ru = new Phrase("последствие", ruCulture);
        var admit_ru = new Phrase("признавать", ruCulture);
        var prevent_ru = new Phrase("предотвращать", ruCulture);
        var illuminate_ru = new Phrase("освещать", ruCulture);
        var investigation_ru = new Phrase("расследование", ruCulture);
        var investigate_ru = new Phrase("расследовать", ruCulture);
        var consider_ru = new Phrase("рассматривать", ruCulture);
        var promise_ru = new Phrase("обещать", ruCulture);
        var kidnap_ru = new Phrase("похищение", ruCulture);
        var hijacking_ru = new Phrase("угон", ruCulture);
        var coincidence_ru = new Phrase("совпадение", ruCulture);
        var disaster_ru = new Phrase("катастрофа", ruCulture);
        var suspicious_ru = new Phrase("подозрительный", ruCulture);
        var demand_ru = new Phrase("требование", ruCulture);
        var besides_ru = new Phrase("кроме того", ruCulture);
        var obtain_ru = new Phrase("получать", ruCulture);
        var envelope_ru = new Phrase("конверт", ruCulture);

        context.Set<Phrase>().AddRange(joy, outcome, consequence, admit, prevent, illuminate, investigation, investigate, consider,
            promise, kidnap, hijacking, coincidence, disaster, suspicious, demand, besides, obtain, envelope,
            joy_ru, outcome_ru, consequence_ru, admit_ru, prevent_ru, illuminate_ru, investigation_ru, investigate_ru,
            consider_ru, promise_ru, kidnap_ru, hijacking_ru, coincidence_ru, disaster_ru, suspicious_ru,
            demand_ru, besides_ru, obtain_ru, envelope_ru);

        var noImageUrl = "https://sun9-58.userapi.com/impg/T2LyUzjz8C3DKtVoI7p6fo2edXNP04AOcYsDZQ/2Pj8k46yrqk.jpg?size=383x341&quality=95&sign=3f0a715c29c549d26b93b073a344df45";

        var phraseUsages = new List<PhraseUsage>();
        var topic = new Topic(engCulture, ruCulture, "My new topic", "Test topic ...", noImageUrl, phraseUsages);
        phraseUsages = new List<PhraseUsage>()
        {
            new PhraseUsage(topic.Id, joy.Id, joy_ru.Id, "Sentence with joy word", "Предложение с использованием слова радость", noImageUrl),
            new PhraseUsage(topic.Id, outcome.Id, outcome_ru.Id, "Sentence with outcome word", "Предложение с использованием слова исход", noImageUrl),
            new PhraseUsage(topic.Id, consequence.Id, consequence_ru.Id, "Sentence with consequence word", "Предложение с использованием слова последствие", noImageUrl),
            new PhraseUsage(topic.Id, admit.Id, admit_ru.Id, "Sentence with admit word", "Предложение с использованием слова признавать", noImageUrl),
            new PhraseUsage(topic.Id, prevent.Id, prevent_ru.Id, "Sentence with prevent word", "Предложение с использованием слова предотвращать", noImageUrl),
            new PhraseUsage(topic.Id, illuminate.Id, illuminate_ru.Id, "Sentence with illuminate word", "Предложение с использованием слова освещать", noImageUrl),
            new PhraseUsage(topic.Id, investigation.Id, investigation_ru.Id, "Sentence with investigation word", "Предложение с использованием слова расследование", noImageUrl),
            new PhraseUsage(topic.Id, investigate.Id, investigate_ru.Id, "Sentence with investigate word", "Предложение с использованием слова расследовать", noImageUrl),
            new PhraseUsage(topic.Id, consider.Id, consider_ru.Id, "Sentence with consider word", "Предложение с использованием слова рассматривать", noImageUrl),
            new PhraseUsage(topic.Id, promise.Id, promise_ru.Id, "Sentence with promise word", "Предложение с использованием слова обещать", noImageUrl),
            new PhraseUsage(topic.Id, kidnap.Id, kidnap_ru.Id, "Sentence with kidnap word", "Предложение с использованием слова похищение", noImageUrl),
            new PhraseUsage(topic.Id, hijacking.Id, hijacking_ru.Id, "Sentence with hijacking word", "Предложение с использованием слова угон", noImageUrl),
            new PhraseUsage(topic.Id, coincidence.Id, coincidence_ru.Id, "Sentence with coincidence word", "Предложение с использованием слова совпадение", noImageUrl),
            new PhraseUsage(topic.Id, disaster.Id, disaster_ru.Id, "Sentence with disaster word", "Предложение с использованием слова катастрофа", noImageUrl),
            new PhraseUsage(topic.Id, suspicious.Id, suspicious_ru.Id, "Sentence with suspicious word", "Предложение с использованием слова подозрительный", noImageUrl),
            new PhraseUsage(topic.Id, demand.Id, demand_ru.Id, "Sentence with demand word", "Предложение с использованием слова требование", noImageUrl),
            new PhraseUsage(topic.Id, besides.Id, besides_ru.Id, "Sentence with besides word", "Предложение с использованием слова кроме того", noImageUrl),
            new PhraseUsage(topic.Id, obtain.Id, obtain_ru.Id, "Sentence with obtain word", "Предложение с использованием слова получать", noImageUrl),
            new PhraseUsage(topic.Id, envelope.Id, envelope_ru.Id, "Sentence with envelope word", "Предложение с использованием слова конверт", noImageUrl)
        };

        context.Set<Topic>().AddRange(topic);
        context.Set<PhraseUsage>().AddRange(phraseUsages);
    }
}