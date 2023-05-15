using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Interface;
using WebApplication3.Models;

namespace WebApplication3.Repository
{
    public class RunnerRepos : IRunnersRepos
    {
        private AppDb _appdb;
        public RunnerRepos(AppDb appDb)
        {
            _appdb = appDb;
        }
        public bool Add(AppUser user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(AppUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppUser>> GetAllRunners()
        {
            return await _appdb.Users.ToListAsync();
        }

        public async Task<AppUser> GetRunnersById(string id)
        {
            return await _appdb.Users.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _appdb.SaveChanges();
            return saved>0? true:false ;
        }

        public bool Update(AppUser user)
        {
            _appdb.Users.Add(user);
            return Save();
        }


    }
}
