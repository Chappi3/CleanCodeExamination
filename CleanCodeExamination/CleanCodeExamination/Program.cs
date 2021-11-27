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
            GuessGameController gameController = new(ui);
            gameController.RunGameSelection();
        }
    }
}
