using CleanCodeExamination.Interfaces;
using CleanCodeExamination.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.IO;

namespace CleanCodeExamination.Repositories
{
    public class MooGameRepository : IRepository
    {
        private List<PlayerData> _players;

        public MooGameRepository()
        {
            _players = new List<PlayerData>();
        }

        public List<PlayerData> GetPlayersSortedByAverage()
        {
            return _players.OrderBy(p => p.Average()).ToList();
        }
        public void LoadData()
        {
            if (File.Exists("resultMooGame.txt"))
            {
                var jsonText = File.ReadAllText("resultMooGame.txt");
                _players = JsonSerializer.Deserialize<List<PlayerData>>(jsonText);
            }
        }
        public void SaveData()
        {
            var json = JsonSerializer.Serialize(_players);
            File.WriteAllText("resultMooGame.txt", json);
        }
        public PlayerData GetPlayerByName(string name)
        {
            var player = _players.FirstOrDefault(p => p.Name == name);
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
