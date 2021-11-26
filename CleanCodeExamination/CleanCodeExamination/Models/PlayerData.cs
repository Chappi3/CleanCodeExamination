using System;

namespace CleanCodeExamination.Models
{
    public class PlayerData
    {
        public string Name { get; private set; }
        public int NGames { get; private set; }
        public int TotalGuess { get; private set; }

        public PlayerData(string name, int guesses)
        {
            Name = name;
            NGames = 1;
            TotalGuess = guesses;
        }
        public void Update(int guesses)
        {
            TotalGuess += guesses;
            NGames++;
        }
        public double Average()
        {
            return (double)TotalGuess / NGames;
        }
        public override bool Equals(Object p)
        {
            return Name.Equals(((PlayerData)p).Name);
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
