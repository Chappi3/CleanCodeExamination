using CleanCodeExamination.Interfaces;
using CleanCodeExamination.Models;
using CleanCodeExamination.Views;
using System.Collections.Generic;
using System.IO;
using System;

namespace CleanCodeExamination
{
    internal class Program
    {
        private static void Main()
        {
            IStringIo ui = new ConsoleIo();
			bool playOn = true;
            ui.Output("Enter your user name:\n");
            string name = ui.Input();
            while (playOn)
            {
                string goal = makeGoal();
                ui.Output("New game:\n");
                //comment out or remove next line to play real games!
                /*Console.WriteLine("For practice, number is: " + goal + "\n");*/
                string guess = ui.Input();
                int nGuess = 1;
                string bbcc = checkBC(goal, guess);
                ui.Output(bbcc + "\n");
                while (bbcc != "BBBB,")
                {
                    nGuess++;
                    guess = ui.Input();
                    ui.Output(guess + "\n");
                    bbcc = checkBC(goal, guess);
                    ui.Output(bbcc + "\n");
                }
                StreamWriter output = new StreamWriter("result.txt", append: true);
                output.WriteLine(name + "#&#" + nGuess);
                output.Close();
                var sortedTopList = GetSortedTopList();
                ui.Output("Player   games average");
                foreach (PlayerData p in sortedTopList)
                {
                    ui.Output(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NGames, p.Average()));
                }
                ui.Output("Correct, it took " + nGuess + " guesses\nContinue?");
                string answer = ui.Input();
                if (!string.IsNullOrEmpty(answer) && answer.Substring(0, 1) == "n")
                {
                    playOn = false;
                }
            }
		}

        static string makeGoal()
        {
            Random randomGenerator = new Random();
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
                goal = goal + randomDigit;
            }
            return goal;
        }

        static string checkBC(string goal, string guess)
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

        static List<PlayerData> GetSortedTopList()
        {
            StreamReader input = new StreamReader("result.txt");
            List<PlayerData> results = new List<PlayerData>();
            string line;
            while ((line = input.ReadLine()) != null)
            {
                string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string name = nameAndScore[0];
                int guesses = Convert.ToInt32(nameAndScore[1]);
                PlayerData pd = new PlayerData(name, guesses);
                int pos = results.IndexOf(pd);
                if (pos < 0)
                {
                    results.Add(pd);
                }
                else
                {
                    results[pos].Update(guesses);
                }
            }
            input.Close();
            results.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));
            return results;
        }
    }
}
