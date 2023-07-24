using WebApplication2.Models;

namespace WebApplication2.Services.Interfaces;

public interface IMusicService
{
    public Task<int> AddAsync(Music music);
    public Task<int> AddAsync(List<Music> musics);
    public Task<List<Music>> GetAllAsync();
    public Task<Music> GetByIdAsync(int id);
    public Task<int> Delete(int id);
    public Task<Music> Update(int id, Music music);

}
