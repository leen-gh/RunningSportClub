using WebApplication3.Models;

namespace WebApplication3.Interface
{
    public interface IUserRepos
    {
        Task<List<Race>> GetAllUserRaces();
        Task<List<Club>> GetAllUserClub();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetUserByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
