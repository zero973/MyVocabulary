using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Domain.Entities;

/// <summary>
/// Represents a topic that groups words and their usage cases for language learning
/// </summary>
public class Topic : BaseEntity, IAggregateRoot
{

    /// <summary>
    /// The language of the words and sentences being studied in all <see cref="WordUsages"/> (e.g., "en-US", "fr-FR").
    /// </summary>
    public string CultureFrom { get; private set; }

    /// <summary>
    /// The target language for translations (e.g., "en-US", "fr-FR").
    /// </summary>
    public string CultureTo { get; private set; }

    /// <summary>
    /// The header or title of the topic.
    /// </summary>
    public string Header { get; private set; }

    /// <summary>
    /// A detailed description of the topic.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// An optional URL for the cover image of the topic.
    /// </summary>
    public string? PhotoUrl { get; private set; }

    /// <summary>
    /// A collection of word usage cases associated with this topic.
    /// </summary>
    private readonly List<WordUsage> _wordUsages = new List<WordUsage>();

    /// <summary>
    /// A read-only collection of word usage cases in the current topic.
    /// </summary>
    public IReadOnlyCollection<WordUsage> WordUsages => _wordUsages.AsReadOnly();

#pragma warning disable CS8618 // Required by Entity Framework
    private Topic() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Topic"/> class.
    /// </summary>
    /// <param name="cultureFrom">The source language of the words being studied.</param>
    /// <param name="cultureTo">The target language for translations.</param>
    /// <param name="header">The title or name of the topic.</param>
    /// <param name="description">The description of the topic.</param>
    /// <param name="photoUrl">An optional URL for the topic's cover image.</param>
    /// <param name="wordsCases">The initial collection of word usage cases.</param>
    /// <param name="id">The entity Id</param>
    public Topic(string cultureFrom, string cultureTo, string header,
        string description, string? photoUrl, List<WordUsage> wordsCases)
    {
        CultureFrom = cultureFrom;
        CultureTo = cultureTo;
        Header = header;
        Description = description;
        PhotoUrl = string.IsNullOrWhiteSpace(photoUrl) ? null : photoUrl;
        _wordUsages = wordsCases ?? new List<WordUsage>();
    }

    /// <summary>
    /// Updates the topic's details.
    /// </summary>
    /// <param name="cultureFrom">The updated source language of the words being studied.</param>
    /// <param name="cultureTo">The updated target language for translations.</param>
    /// <param name="header">The updated title or name of the topic.</param>
    /// <param name="description">The updated description of the topic.</param>
    /// <param name="photoUrl">The updated URL for the topic's cover image.</param>
    public void Edit(string cultureFrom, string cultureTo, string header,
        string description, string? photoUrl)
    {
        CultureFrom = cultureFrom;
        CultureTo = cultureTo;
        Header = header;
        Description = description;
        PhotoUrl = string.IsNullOrWhiteSpace(photoUrl) ? null : photoUrl;
    }

    /// <summary>
    /// Adds a new word usage case to the topic.
    /// </summary>
    /// <param name="wordUsage">The word usage case to add.</param>
    /// <returns>
    /// <see langword="true"/> if the word usage case was successfully added; 
    /// <see langword="false"/> if it already exists in the collection.
    /// </returns>
    public bool AddWordUsage(WordUsage wordUsage)
    {
        if (_wordUsages.Contains(wordUsage))
            return false;

        _wordUsages.Add(wordUsage);
        return true;
    }

    /// <summary>
    /// Removes an existing word usage case from the topic.
    /// </summary>
    /// <param name="wordUsage">The word usage case to remove.</param>
    /// <returns>
    /// <see langword="true"/> if the word usage case was successfully removed; 
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public bool RemoveWordUsage(WordUsage wordUsage)
    {
        return _wordUsages.Remove(wordUsage);
    }

}