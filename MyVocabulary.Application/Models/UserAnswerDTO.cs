namespace MyVocabulary.Application.Models;

public class UserAnswerDTO
{

    public Guid Id { get; set; }

    public WordUsageDTO WordUsage { get; set; }

    public bool IsRight { get; set; }

    public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;

    public UserAnswerDTO(Guid id, WordUsageDTO wordUsage, bool isRight)
    {
        Id = id;
        WordUsage = wordUsage;
        IsRight = isRight;
    }

}