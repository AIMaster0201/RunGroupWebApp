using RunGroupWebApp.Models;

namespace RunGroupWebApp.interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAll();
        Task<Race> GetByIdAsync(int Id);
        Task<IEnumerable<Race>> GetRaceByCity(string city);

        bool Add(Race Race);
        bool Update(Race Race);
        bool Delete(Race Race);
        bool Save();
    }
}
