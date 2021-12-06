using CleanCodeExamination.Interfaces;
using CleanCodeExamination.Models;
using CleanCodeExamination.Games;
using System.Collections.Generic;
using System;

namespace CleanCodeExamination.Controllers
{
    public class GuessGameController
    {
        private readonly IStringIo _ui;
        private readonly IRepository _repository;
        private readonly Dictionary<string, IGuessGame> games;

        public GuessGameController(IStringIo ui, IRepository repository)
        {
            _ui = ui;
            _repository = repository;
            games = new Dictionary<string, IGuessGame> 
            {
                { "Moo Game", new MooGame()},
                { "Master Mind", new MasterMindGame() }
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
                        RunGame("mooGame", games["Moo Game"!]);
                        break;
                    case "2":
                        RunGame("masterMindGame", games["Master Mind"!]);
                        break;
                    case "q":
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }

            } while (isSelectingGame);
        }

        private void RunGame(string selectedGame, IGuessGame guessGame)
        {
            var playOn = true;
            _repository.LoadData(selectedGame);
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
                    _repository.SaveData(selectedGame);
                    playOn = false;
                }
            } while (playOn);
        }

        private void PrintMenu()
        {
            _ui.Output("----- Games -----");
            var counter = 1;
            foreach (var game in games)
            {
                _ui.Output($"{counter}. {game.Key}");
                counter++;
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
            string result;
            int guesses = 0;
            do
            {
                guesses++;
                var guess = _ui.Input();
                _ui.Output(guess + "\n");
                result = guessGame.CheckGuess(goal, guess);
                _ui.Output(result + "\n");
            } while (result != "BBBB,");
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
