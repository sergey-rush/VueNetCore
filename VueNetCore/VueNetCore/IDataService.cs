using System.Data;
using System.Linq;

namespace VueNetCore
{
    public interface IDataService
    {
        void Add(string data);
        Entry[] Search(string query);

    }
}