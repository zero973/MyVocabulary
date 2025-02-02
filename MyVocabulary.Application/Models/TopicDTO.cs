namespace MyVocabulary.Application.Models;

public class TopicDTO
{

    public Guid Id { get; set; }

    public Language CultureFrom { get; set; }

    public Language CultureTo { get; set; }

    public string Header { get; set; }

    public string Description { get; set; }

    public string? PhotoUrl { get; set; }

    public List<WordUsageDTO> WordUsages { get; set; } = new List<WordUsageDTO>();

    public TopicDTO(Guid id, Language cultureFrom, Language cultureTo, 
        string header, string description, string? photoUrl, 
        List<WordUsageDTO> wordUsages)
    {
        Id = id;
        CultureFrom = cultureFrom;
        CultureTo = cultureTo;
        Header = header;
        Description = description;
        PhotoUrl = photoUrl;
        WordUsages = wordUsages;
    }

}