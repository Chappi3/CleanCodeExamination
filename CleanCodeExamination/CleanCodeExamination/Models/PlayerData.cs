namespace CleanCodeExamination.Models
{
    public class PlayerData
    {
        public string Name { get; private set; }
        public int Games { get; private set; }
        public int Guesses { get; private set; }

        public PlayerData(string name, int guesses = 0, int games = 0)
        {
            Name = name;
            Games = games;
            Guesses = guesses;
        }

        public void Update(int guesses)
        {
            Guesses += guesses;
            Games++;
        }

        public double Average()
        {
            return (double)Guesses / Games;
        }
    }
}
