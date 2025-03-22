namespace MyVocabulary.Application.Models;

/// <summary>
/// 
/// </summary>
public class TopicPracticeResult
{

    public uint CorrectAnswers { get; set; }
    
    public uint WrongAnswers { get; set; }
    
    public double StudyProgress { get; set; }
    
    public int StudyProgressPercent => (int)Math.Round(StudyProgress * 100, 0);
    
    public TopicPracticeResult(uint correctAnswers, uint wrongAnswers, double studyProgress)
    {
        CorrectAnswers = correctAnswers;
        WrongAnswers = wrongAnswers;
        StudyProgress = studyProgress;
    }
    
}