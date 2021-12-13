namespace CleanCodeExamination.Interfaces
{
    public interface IGuessGame
    {
        string CreateGoal();
        (string result, bool isCorrect) CheckGuess(string goal, string guess);
    }
}
