using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Interface;
using WebApplication3.Models;

namespace WebApplication3.Repository
{
    public class UserRepos : IUserRepos
    {
        private readonly AppDb _appdb;
        private readonly IHttpContextAccessor _contextAccessor;

        //access to functionality direct from the web page 
        //it contains all the request data 
        //its store the request and the response info
        //such as the properties of request, request-related services, and any data to/from the request or errors, if there are any.
        //and we access the httpcontext through  the ihttpcontext interface
        public UserRepos(AppDb appdb, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _appdb = appdb;
        }
        public async Task<List<Club>> GetAllUserClub()
        {
            var currentUser = _contextAccessor.HttpContext?.User.GetUserId();
            var userClub = _appdb.Clubs.Where(r => r.AppUser.Id == currentUser.ToString());
            return userClub.ToList();
        }

        public async Task<List<Race>> GetAllUserRaces()
        {
            var currentUser = _contextAccessor.HttpContext?.User.GetUserId();
            var userRace = _appdb.Races.Where(r => r.AppUser.Id == currentUser.ToString());
            return userRace.ToList();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _appdb.Users.FindAsync(id);
        }


        public async Task<AppUser> GetUserByIdNoTracking(string id)
        {
            return await _appdb.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
        public bool Update(AppUser user)
        {
            _appdb.Users.Update(user);
            return Save();
        }
        public bool Save()
        {
            var saved = _appdb.SaveChanges();
            return saved > 0;
        }

    
    }
}
