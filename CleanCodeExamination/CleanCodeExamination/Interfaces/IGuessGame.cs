using CleanCodeExamination.Models;

namespace CleanCodeExamination.Interfaces
{
    public interface IGuessGame
    {
        void Run();
        string CreateGoal();
        string CheckGuess(string goal, string guess);
        PlayerData GetPlayerByInput();
        int PlayGame(string goal);
        void PrintTopList();
    }
}
