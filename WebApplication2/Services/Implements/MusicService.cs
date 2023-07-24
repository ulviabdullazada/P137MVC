using System.Data;
using WebApplication2.Helpers;
using WebApplication2.Models;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services.Implements;

public class MusicService : IMusicService
{
    public async Task<int> AddAsync(Music music)
    {
        return await SqlHelper.ExecuteAsync($"INSERT INTO Musics VALUES (N'{music.Name}', {music.Duration})");
    }

    public async Task<int> AddAsync(List<Music> musics)
    {
        string query = "INSERT INTO Musics VALUES";
        foreach (Music music in musics)
        {
            query += $"(N'{music.Name}', {music.Duration}),";
        }
        return await SqlHelper.ExecuteAsync(query.Substring(0,query.Length-1));
    }

    public async Task<int> Delete(int id)
    {
        await GetByIdAsync(id);
        return await SqlHelper.ExecuteAsync("DELETE Musics WHERE Id = " + id);
    }

    public async Task<List<Music>> GetAllAsync()
    {
        List<Music> list = new List<Music>();
        DataTable dt = await SqlHelper.SelectAsync("Select * from Musics");
        foreach (DataRow item in dt.Rows)
        {
            list.Add(new Music
            {
                Id = (int)item["Id"],
                Name = item["Name"]?.ToString(),
                Duration = (int)item["Duration"]
            });
        }
        return list;
    }

    public async Task<Music> GetByIdAsync(int id)
    {
        DataTable dt = await SqlHelper.SelectAsync("Select * from Musics where Id = " + id);
        if (dt.Rows.Count != 1) throw new Exception("Error");
        return new Music
        {
            Id = (int)dt.Rows[0]["Id"],
            Name = (string)dt.Rows[0]["Name"],
            Duration = (int)dt.Rows[0]["Duration"]
        };
    }

    public Task<Music> Update(int id, Music music)
    {
        throw new NotImplementedException();
    }
}
