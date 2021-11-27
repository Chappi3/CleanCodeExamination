namespace CleanCodeExamination.Interfaces
{
    public interface IGuessGame
    {
        void Run();
        string MakeGoal();
        string CheckGuess(string goal, string guess);
    }
}
