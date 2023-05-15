using WebApplication3.Models;

namespace WebApplication3.Interface
{
    public interface IRunnersRepos
    {
        Task <IEnumerable<AppUser>> GetAllRunners ();
        Task<AppUser> GetRunnersById (string id);
        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(AppUser user);
        bool Save();

    }
}
