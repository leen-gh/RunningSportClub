using WebApplication3.Models;
using WebApplication3.ViewModel;

namespace WebApplication3.Interface
{
    public interface IRaceRepos
    {
        Task<IEnumerable<Race>> GetAll();
        Task<Race> GetIdByAsync(int id);
        Task<Race> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Race>> GetAllRacesByCity(string city);


        //then we have our CRUD here
        bool Add(Race race);
        bool Update(Race race);
        bool Delete(Race race);
        bool Save();
        Task<Race> GetByIdAsync(int id);

        void Add(CreatRaceViewModel club);


    }
}
