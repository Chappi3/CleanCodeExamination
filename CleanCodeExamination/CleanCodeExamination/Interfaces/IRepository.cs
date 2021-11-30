using CleanCodeExamination.Models;
using System.Linq;

namespace CleanCodeExamination.Interfaces
{
    public interface IRepository
    {
        IOrderedEnumerable<PlayerData> GetSortedTopList();
        void SaveGame(string name, int numGuesses);
    }
}
