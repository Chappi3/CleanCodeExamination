using CleanCodeExamination.Models;
using System.Collections.Generic;

namespace CleanCodeExamination.Interfaces
{
    public interface IRepository
    {
        List<PlayerData> GetPlayersSortedByAverage();
        void SaveData();
        void LoadData();
        PlayerData GetPlayerByName(string name);
    }
}
