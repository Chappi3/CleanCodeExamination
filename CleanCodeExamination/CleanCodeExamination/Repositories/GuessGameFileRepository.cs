using CleanCodeExamination.Interfaces;
using CleanCodeExamination.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.IO;

namespace CleanCodeExamination.Repositories
{
    public class GuessGameFileRepository : IRepository
    {
        private List<PlayerData> _players;

        public GuessGameFileRepository()
        {
            _players = new List<PlayerData>();
        }

        public List<PlayerData> GetPlayersSortedByAverage()
        {
            return _players.OrderBy(p => p.Average()).ToList();
        }
        public void LoadData(string selectedGame)
        {
            if (File.Exists($"result{selectedGame}.txt"))
            {
                var jsonText = File.ReadAllText($"result{selectedGame}.txt");
                _players = JsonSerializer.Deserialize<List<PlayerData>>(jsonText);
            }
        }
        public void SaveData(string selectedGame)
        {
            var json = JsonSerializer.Serialize(_players);
            File.WriteAllText($"result{selectedGame}.txt", json);
        }
        public PlayerData GetPlayerByName(string name)
        {
            var player = _players.FirstOrDefault(p => p.Name.Equals(name));
            if (player is null)
            {
                return InitializeNewPlayer(name);
            }
            return player;
        }

        private PlayerData InitializeNewPlayer(string name)
        {
            PlayerData player = new(name);
            _players.Add(player);
            return player;
        }
    }
}
