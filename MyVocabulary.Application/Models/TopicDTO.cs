namespace MyVocabulary.Application.Models;

public class TopicDTO
{

    public Guid Id { get; set; }

    public Language CultureFrom { get; set; }

    public Language CultureTo { get; set; }

    public string Header { get; set; }

    public string Description { get; set; }

    public string ShortDescription => Description.Length > 25
        ? $"{Description.Substring(0, 25)}..."
        : Description;

    public string? PhotoUrl { get; set; }

    public List<PhraseUsageDTO> PhraseUsages { get; set; } = new List<PhraseUsageDTO>();

    public TopicDTO(Guid id, Language cultureFrom, Language cultureTo, 
        string header, string description, string? photoUrl, 
        List<PhraseUsageDTO> phraseUsages)
    {
        Id = id;
        CultureFrom = cultureFrom;
        CultureTo = cultureTo;
        Header = header;
        Description = description;
        PhotoUrl = photoUrl;
        PhraseUsages = phraseUsages;
    }

    public override string ToString() => $"{CultureFrom}-{CultureTo} {Header}";

}