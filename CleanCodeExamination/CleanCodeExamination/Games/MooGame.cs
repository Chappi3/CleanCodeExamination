using CleanCodeExamination.Interfaces;
using CleanCodeExamination.Models;
using System;

namespace CleanCodeExamination.Games
{
    public class MooGame : IGuessGame
    {
        private readonly IStringIo _ui;
        private readonly IStatistics _repository;

        public MooGame(IStringIo ui, IStatistics repository)
        {
            _ui = ui;
            _repository = repository;
        }
        public void Run()
        {
            bool playOn = true;
            _ui.Output("Enter your username:\n");
            string name = _ui.Input();
            while (playOn)
            {
                string goal = MakeGoal();
                _ui.Output("New game:\n");
                //comment out or remove next line to play real games!
                //ui.Output("For practice, number is: " + goal + "\n");
                string guess = _ui.Input();
                int nGuess = 1;
                string result = CheckGuess(goal, guess);
                _ui.Output(result + "\n");
                while (result != "BBBB,")
                {
                    nGuess++;
                    guess = _ui.Input();
                    _ui.Output(guess + "\n");
                    result = CheckGuess(goal, guess);
                    _ui.Output(result + "\n");
                }
                _repository.SaveGame(name, nGuess);
                var sortedTopList = _repository.GetSortedTopList();
                _ui.Output("Player   games average");
                foreach (PlayerData p in sortedTopList)
                {
                    _ui.Output(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NGames, p.Average()));
                }
                _ui.Output("Correct, it took " + nGuess + " guesses\nContinue?");
                string answer = _ui.Input();
                if (!string.IsNullOrEmpty(answer) && answer.Substring(0, 1) == "n")
                {
                    playOn = false;
                }
            }
        }
        public string MakeGoal()
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
            guess += "    ";     // if player entered less than 4 chars
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
            return "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);
        }
    }
}
