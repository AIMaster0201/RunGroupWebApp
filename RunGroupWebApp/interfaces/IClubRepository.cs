using RunGroupWebApp.Models;

namespace RunGroupWebApp.interfaces
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAll();
        Task<Club> GetByIdAsync(int Id);
        Task<IEnumerable<Club>> GetClubByCity(string city);

        bool Add(Club club);
        bool Update(Club club);
        bool Delete(Club club);
        bool Save();
    }
}
