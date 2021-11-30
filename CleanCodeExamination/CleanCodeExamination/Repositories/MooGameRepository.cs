using CleanCodeExamination.Interfaces;
using CleanCodeExamination.Models;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace CleanCodeExamination.Repositories
{
    public class MooGameRepository : IRepository
    {
        public IOrderedEnumerable<PlayerData> GetSortedTopList()
        {
            StreamReader input = new("resultMooGame.txt");
            List<PlayerData> results = new();
            string line;
            while ((line = input.ReadLine()) != null)
            {
                string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string name = nameAndScore[0];
                int guesses = Convert.ToInt32(nameAndScore[1]);
                PlayerData data = new PlayerData(name, guesses);
                int pos = results.IndexOf(data);
                if (pos < 0)
                {
                    results.Add(data);
                }
                else
                {
                    results[pos].Update(guesses);
                }
            }
            input.Close();
            return results.OrderBy(p => p.Average());
        }
        public void SaveGame(string name, int numGuesses)
        {
            StreamWriter output = new("resultMooGame.txt", append: true);
            output.WriteLine(name + "#&#" + numGuesses);
            output.Close();
        }
    }
}
