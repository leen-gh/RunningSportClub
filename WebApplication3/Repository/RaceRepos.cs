using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Interface;
using WebApplication3.Models;
using WebApplication3.ViewModel;

namespace WebApplication3.Repository
{
    public class RaceRepos : IRaceRepos
    {
        private readonly AppDb _appDb;
        public RaceRepos(AppDb context)
        {
            _appDb = context;

        }
        public bool Add(Race race)
        {
            //this code is not really saving the data its just generating the SQL cmd
            _appDb.Add(race);
            //so that why we have to add save
            return Save();
        }

        public bool Delete(Race race)
        {
            _appDb.Remove(race);
            return Save();
        }
        //task is not returning the data is just tell you when you will get hte data
        public async Task<IEnumerable<Race>> GetAll()
        {
            return await _appDb.Races.ToListAsync();
        }

        public async Task<IEnumerable<Race>> GetAllRaceByCity(string city)
        {
            // we go to Races -> address -> city 
            return await _appDb.Races.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        //this one will return one only the one before will return a whole list 
        public async Task<Race> GetIdByAsync(int id)
        {
            return await _appDb.Races.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Race> GetByIdAsyncNoTracking(int id)
        {
            return await _appDb.Races.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            // this will save changes by it self when it ends by returning 1 
            var saved = _appDb.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Race race)
        {
            _appDb.Update(race);
            return Save();
        }

        public void Add(CreatRaceViewModel club)
        {
            throw new NotImplementedException();
        }

        Task<Race> IRaceRepos.GetByIdAsyncNoTracking(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Race> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Race>> GetAllRacesByCity(string city)
        {
            throw new NotImplementedException();
        }
        //to avoid traking the same two database context at the same time

    }
}
