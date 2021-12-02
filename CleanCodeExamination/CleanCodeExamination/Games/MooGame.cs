using CleanCodeExamination.Interfaces;
using CleanCodeExamination.Models;
using System;

namespace CleanCodeExamination.Games
{
    public class MooGame : IGuessGame
    {
        private readonly IStringIo _ui;
        private readonly IRepository _repository;

        public MooGame(IStringIo ui, IRepository repository)
        {
            _ui = ui;
            _repository = repository;
        }

        public void Run()
        {
            var playOn = true;
            _repository.LoadData();
            var player = GetPlayerByInput();
            do
            {
                var goal = CreateGoal();
                var guesses = PlayGame(goal);
                player.Update(guesses);
                PrintTopList();
                _ui.Output($"Correct, it took {guesses} guesses\nContinue?");
                string answer = _ui.Input();
                if (!string.IsNullOrEmpty(answer) && answer.Substring(0, 1) == "n")
                {
                    _repository.SaveData();
                    playOn = false;
                }
            } while (playOn);
        }
        public void PrintTopList()
        {
            var sortedTopList = _repository.GetPlayersSortedByAverage();
            _ui.Output("Player   games average");
            foreach (PlayerData p in sortedTopList)
            {
                _ui.Output(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.Games, p.Average()));
            }
        }
        public int PlayGame(string goal)
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
                result = CheckGuess(goal, guess);
                _ui.Output(result + "\n");
            } while (result != "BBBB,");
            return guesses;
        }
        public string CreateGoal()
        {
            Random randomGenerator = new();
            string goal = "";
            for (int i = 0; i < 4; i++)
            {
                int random = randomGenerator.Next(10);
                string randomDigit = "" + random;
                while (goal.Contains(randomDigit))
                {
                    random = randomGenerator.Next(10);
                    randomDigit = "" + random;
                }
                goal += randomDigit;
            }
            return goal;
        }
        public string CheckGuess(string goal, string guess)
        {
            int cows = 0, bulls = 0;
            guess += "    ";    // if player entered less than 4 chars
            guess += guess.PadRight(4, ' ');
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (goal[i] == guess[j])
                    {
                        if (i == j)
                        {
                            bulls++;
                        }
                        else
                        {
                            cows++;
                        }
                    }
                }
            }
            return $"{"BBBB".Substring(0, bulls)},{"CCCC".Substring(0, cows)}";
        }
        public PlayerData GetPlayerByInput()
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
    }
}
