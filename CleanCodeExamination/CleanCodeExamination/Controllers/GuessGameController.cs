using CleanCodeExamination.Interfaces;
using CleanCodeExamination.Models;
using CleanCodeExamination.Games;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CleanCodeExamination.Controllers
{
    public class GuessGameController
    {
        private readonly IStringIo _ui;
        private readonly IRepository _repository;
        private readonly Dictionary<(int number, string name), Func<IGuessGame>> games;

        public GuessGameController(IStringIo ui, IRepository repository)
        {
            _ui = ui;
            _repository = repository;
            games = new Dictionary<(int, string), Func<IGuessGame>>
            {
                { (1, "Moo Game"), () => new MooGame() },
                { (2, "Master Mind"), () => new MasterMindGame() }
            };
        }

        public void RunGameSelection()
        {
            var isSelectingGame = true;
            do
            {
                PrintMenu();
                var input = _ui.Input();

                if (input.Equals("q"))
                {
                    isSelectingGame = false;
                }
                else if (int.TryParse(input, out int chosenGameNumber))
                {
                    var game = games.FirstOrDefault(k => k.Key.number == chosenGameNumber);
                    
                    if (game.Value is not null)
                    {
                        RunGame(game.Value.Invoke());
                    }
                    else
                    {
                        _ui.Clear();
                        _ui.Output("Game not found!");
                    }
                }
                else
                {
                    _ui.Clear();
                    _ui.Output("Command not found!");
                }
            } while (isSelectingGame);
            Environment.Exit(0);
        }

        private void RunGame(IGuessGame guessGame)
        {
            var playOn = true;
            _repository.LoadData(guessGame.GetType().Name);
            var player = GetPlayerByInput();
            do
            {
                var goal = guessGame.CreateGoal();
                var guesses = PlayGame(goal, guessGame);
                player.Update(guesses);
                PrintTopList();
                _ui.Output($"Correct, it took {guesses} guesses\nContinue?");
                string answer = _ui.Input();
                if (!string.IsNullOrEmpty(answer) && answer.Substring(0, 1) == "n")
                {
                    _repository.SaveData(guessGame.GetType().Name);
                    playOn = false;
                }
            } while (playOn);
        }

        private void PrintMenu()
        {
            _ui.Output("----- Games -----");
            foreach (var game in games)
            {
                _ui.Output($"{game.Key.number}. {game.Key.name}");
            }
            _ui.Output("Select a game by typing the corresponding number.");
            _ui.Output("Exit = q");
        }

        private PlayerData GetPlayerByInput()
        {
            string name;
            do
            {
                _ui.Clear();
                _ui.Output("Enter your username:\n");
                name = _ui.Input();
            } while (name.Length < 1);

            PlayerData player = _repository.GetPlayerByName(name);
            if (player is null)
            {
                player = new PlayerData(name);
            }
            return player;
        }

        private int PlayGame(string goal, IGuessGame guessGame)
        {
            _ui.Output("New game:\n");
            //comment out or remove next line to play real games!
            //_ui.Output($"For practice, number is: {goal}\n");
            bool isCorrect;
            int guesses = 0;
            do
            {
                guesses++;
                var guess = _ui.Input();
                _ui.Output(guess + "\n");
                var checkedGuess = guessGame.CheckGuess(goal, guess);
                _ui.Output(checkedGuess.result + "\n");
                isCorrect = checkedGuess.isCorrect;
            } while (!isCorrect);
            return guesses;
        }

        private void PrintTopList()
        {
            var sortedTopList = _repository.GetPlayersSortedByAverage();
            _ui.Output("Player   games average");
            foreach (PlayerData p in sortedTopList)
            {
                _ui.Output(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.Games, p.Average()));
            }
        }
    }
}
