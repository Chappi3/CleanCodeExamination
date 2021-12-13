using CleanCodeExamination.Interfaces;
using System;

namespace CleanCodeExamination.Games
{
    public class MooGame : IGuessGame
    {
        private const int numberAmount = 4;

        public string CreateGoal()
        {
            Random randomGenerator = new();
            string goal = "";
            for (int i = 0; i < numberAmount; i++)
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
        public (string result, bool isCorrect) CheckGuess(string goal, string guess)
        {
            int cows = 0, bulls = 0;
            guess += guess.PadRight(numberAmount, ' ');
            for (int i = 0; i < numberAmount; i++)
            {
                for (int j = 0; j < numberAmount; j++)
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
            return ($"{"BBBB".Substring(0, bulls)},{"CCCC".Substring(0, cows)}", bulls == numberAmount);
        }
    }
}
