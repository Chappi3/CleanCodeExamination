using CleanCodeExamination.Interfaces;
using System;

namespace CleanCodeExamination.Games
{
    public class MasterMindGame : IGuessGame
    {
        private const int numberAmount = 4;

        public string CreateGoal()
        {
            Random randomGenerator = new();
            string goal = "";
            for (int i = 0; i < numberAmount; i++)
            {
                int random = randomGenerator.Next(10);
                string randomDigit = random.ToString();
                goal += randomDigit;
            }
            return goal;
        }

        public (string result, bool isCorrect) CheckGuess(string goal, string guess)
        {
            int wrongPlace = 0, rightPlace = 0;
            guess += guess.PadRight(numberAmount, ' ');
            for (int i = 0; i < numberAmount; i++)
            {
                for (int j = 0; j < numberAmount; j++)
                {
                    if (goal[i] == guess[j])
                    {
                        if (i == j)
                        {
                            rightPlace++;
                        }
                        else
                        {
                            wrongPlace++;
                        }
                    }
                }
            }
            return ($"{"RRRR".Substring(0, rightPlace)},{"WWWW".Substring(0, wrongPlace)}", rightPlace == numberAmount);
        }
    }
}
