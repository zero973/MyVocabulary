using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Domain.Entities;

/// <summary>
/// Topic
/// </summary>
public class Topic : BaseEntity, IAggregateRoot
{

    /// <summary>
    /// The language in which all <see cref="WordsCases"/> are described
    /// </summary>
    public string CultureFrom { get; private set; }

    /// <summary>
    /// The language to translate
    /// </summary>
    public string CultureTo { get; private set; }

    /// <summary>
    /// Topic header
    /// </summary>
    public string Header { get; private set; }

    /// <summary>
    /// Topic description
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Cover Url
    /// </summary>
    public string? PhotoUrl { get; private set; }

    /// <summary>
    /// Words cases in current topic
    /// </summary>
    private readonly List<WordCase> _wordsCases = new List<WordCase>();

    /// <summary>
    /// Words cases in current topic
    /// </summary>
    public IReadOnlyCollection<WordCase> WordsCases => _wordsCases.AsReadOnly();

#pragma warning disable CS8618 // Required by Entity Framework
    private Topic() { }

    public Topic(string cultureFrom, string cultureTo, string header, 
        string description, string? photoUrl, List<WordCase> wordsCases)
    {
        CultureFrom = cultureFrom;
        CultureTo = cultureTo;
        Header = header;
        Description = description;
        PhotoUrl = photoUrl;
        _wordsCases = wordsCases;
    }

    /// <summary>
    /// Edit topic
    /// </summary>
    public void Edit(string cultureFrom, string cultureTo, string header,
        string description, string? photoUrl)
    {
        CultureFrom = cultureFrom;
        CultureTo = cultureTo;
        Header = header;
        Description = description;
        PhotoUrl = photoUrl;
    }

    /// <summary>
    /// Add new word case
    /// </summary>
    public bool AddWordCase(WordCase wordCase)
    {
        if (_wordsCases.Contains(wordCase))
            return false;

        _wordsCases.Add(wordCase);
        return true;
    }

    /// <summary>
    /// Remove word case
    /// </summary>
    public bool RemoveWordCase(WordCase wordCase)
    {
        return _wordsCases.Remove(wordCase);
    }

}