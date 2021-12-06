using CleanCodeExamination.Interfaces;
using System;

namespace CleanCodeExamination.Games
{
    public class MooGame : IGuessGame
    {
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
    }
}
