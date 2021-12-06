using CleanCodeExamination.Repositories;
using CleanCodeExamination.Interfaces;
using CleanCodeExamination.Games;
using System.Collections.Generic;
using System;

namespace CleanCodeExamination.Controllers
{
    public class GuessGameController
    {
        private readonly IStringIo _ui;
        private readonly List<string> games;

        public GuessGameController(IStringIo ui)
        {
            _ui = ui;
            games = new List<string> 
            {
                { "Moo Game" },
                { "Master Mind" }
            };
        }
        public void RunGameSelection()
        {
            var isSelectingGame = true;
            do
            {
                PrintMenu();
                var input = _ui.Input();
                switch (input)
                {
                    case "1":
                        IRepository mooGameRepository = new MooGameRepository();
                        IGuessGame mooGame = new MooGame(_ui, mooGameRepository);
                        mooGame.Run();
                        break;
                    case "2":
                        IRepository masterMindRepository = new MasterMindRepository();
                        IGuessGame masterMindGame = new MasterMindGame(_ui, masterMindRepository);
                        break;
                    case "q":
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            } while (isSelectingGame);
        }
        private void PrintMenu()
        {
            _ui.Output("----- Games -----");
            var counter = 1;
            foreach (var game in games)
            {
                _ui.Output($"{counter}. {game}");
                counter++;
            }
            _ui.Output("Select a game by typing the corresponding number.");
            _ui.Output("Exit = q");
        }
    }
}
