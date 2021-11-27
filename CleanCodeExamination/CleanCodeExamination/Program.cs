using CleanCodeExamination.Repositories;
using CleanCodeExamination.Interfaces;
using CleanCodeExamination.Views;
using CleanCodeExamination.Games;

namespace CleanCodeExamination
{
    internal class Program
    {
        private static void Main()
        {
            IStringIo ui = new ConsoleIo();
            IStatistics repository = new MooGameRepository();
            IGuessGame mooGame = new MooGame(ui, repository);
            mooGame.Run();
        }
    }
}
