using CleanCodeExamination.Repositories;
using CleanCodeExamination.Controllers;
using CleanCodeExamination.Interfaces;
using CleanCodeExamination.Views;

namespace CleanCodeExamination
{
    internal class Program
    {
        private static void Main()
        {
            IStringIo ui = new ConsoleIo();
            IRepository repository = new GuessGameFileRepository();
            GuessGameController gameController = new(ui, repository);
            gameController.RunGameSelection();
        }
    }
}
