using WebApplication3.Models;
using WebApplication3.ViewModel;

namespace WebApplication3.Interface
{
    public interface IClubRepos
    {
        // the interface is describe a contract for a new class
        //first we get our get cmd here
        Task<IEnumerable<Club>> GetAll();
        Task<Club> GetIdByAsync(int id);
        Task<Club> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Club>> GetClubByCity(string city);
        
        
        //then we have our CRUD here
        bool Add(Club club);
        bool Update(Club club); 
        bool Delete(Club club);
        bool Save();
        Task<Club> GetByIdAsync(int id);
        void Add(CreateClubViewModel club);
    }
} 
