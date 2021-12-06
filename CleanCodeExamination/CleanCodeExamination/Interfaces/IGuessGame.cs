namespace CleanCodeExamination.Interfaces
{
    public interface IGuessGame
    {
        string CreateGoal();
        string CheckGuess(string goal, string guess);
    }
}
