using CleanCodeExamination.Models;
using System.Collections.Generic;

namespace CleanCodeExamination.Interfaces
{
    public interface IRepository
    {
        List<PlayerData> GetPlayersSortedByAverage();
        void SaveData(string selectedGame);
        void LoadData(string selectedGame);
        PlayerData GetPlayerByName(string name);
    }
}
