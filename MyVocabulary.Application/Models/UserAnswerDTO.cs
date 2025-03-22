namespace MyVocabulary.Application.Models;

public class UserAnswerDTO
{

    public Guid Id { get; set; }

    public PhraseUsageDTO PhraseUsage { get; set; }

    public bool IsRight { get; set; }

    public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
    
    public string ShortDate => Date.ToString("dd/MM/yyyy HH:mm");

    public UserAnswerDTO(Guid id, PhraseUsageDTO phraseUsage, bool isRight)
    {
        Id = id;
        PhraseUsage = phraseUsage;
        IsRight = isRight;
    }

    public override string ToString() => $"{PhraseUsage}";

}