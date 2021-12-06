using CleanCodeExamination.Interfaces;
using System;

namespace CleanCodeExamination.Games
{
    class MasterMindGame : IGuessGame
    {
        public string CreateGoal()
        {
            var colors = "GBYPL"; // Green, Blue, Yellow, Purple, Lime
            var randomGenerator = new Random();
            var goal = "";

            for (int i = 0; i < 4; i++)
            {
                goal += colors[randomGenerator.Next(colors.Length - 1)];
            }

            return goal;
        }

        public string CheckGuess(string goal, string guess)
        {
            int whitePins = 0, RedPins = 0;
            guess += guess.PadRight(4, ' ').ToUpper();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (goal[i] == guess[j])
                    {
                        if (i == j)
                        {
                            RedPins++;
                        }
                        else
                        {
                            whitePins++;
                        }
                    }
                }
            }
            return $"{"RRRR".Substring(0, RedPins)},{"WWWW".Substring(0, whitePins)}";
        }
    }
}
