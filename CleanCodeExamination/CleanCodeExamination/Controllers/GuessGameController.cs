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
        private readonly Dictionary<string, IGuessGame> games;
        private readonly IRepository _repository;

        public GuessGameController(IStringIo ui, IRepository repository)
        {
            _ui = ui;
            games = new Dictionary<string, IGuessGame> 
            {
                { "Moo Game", new MooGame(_ui, repository)},
                { "Master Mind", new MasterMindGame() }
            };
            _repository = repository;
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
                        IRepository mooGameRepository = new GuessGameFileRepository();
                        IGuessGame mooGame = new MooGame(_ui, mooGameRepository);
                        mooGame.Run();
                        break;
                    case "2":
                        IGuessGame masterMindGame = new MasterMindGame();
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
