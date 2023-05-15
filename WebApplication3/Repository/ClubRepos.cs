using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Interface;
using WebApplication3.Models;
using WebApplication3.ViewModel;

namespace WebApplication3.Repository
{
    public class ClubRepos : IClubRepos
    {
        private readonly AppDb _appDb;
        public ClubRepos(AppDb context) 
        {
            _appDb = context;
        }
        public bool Add(Club club)
        {
            //this code is not really saving the data its just generating the SQL cmd
            _appDb.Add(club); 
            //so that why we have to add save
            return Save();
        }

        public bool Delete(Club club)
        {
            _appDb.Remove(club);
            return Save();
        }
        //task is not returning the data is just tell you when you will get hte data
        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _appDb.Clubs.ToListAsync();
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            // we go to clubs -> address -> city 
            return await _appDb.Clubs.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        //this one will return one only the one before will return a whole list 
        public async Task<Club> GetByIdAsync(int id)
        {
            //adress is a navigation property( foreign key )when ever we use nav pro we should include it
            return await _appDb.Clubs.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        //to avoid traking the same two database context at the same time
        public async Task<Club> GetByIdAsyncNoTracking(int id)
        {
            return await _appDb.Clubs.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            // this will save changes by it self when it ends by returning 1 
            var saved =_appDb.SaveChanges();
            return saved > 0;
        }

        public bool Update(Club club)
        {
            _appDb.Update(club);
            return Save();
        }

        public Task<Club> GetIdByAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(CreateClubViewModel club)
        {
            throw new NotImplementedException();
        }
    }
}
