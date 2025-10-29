using Chirp.Domain;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure;

public class CheepRepo : ICheepRepository
{
    private readonly CheepDbContext _dbContext;
    
    public CheepRepo(CheepDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<int> GetCheepCount()
    {
        return await _dbContext.Cheeps.AsNoTracking().CountAsync();
    }

    public async Task<List<CheepDTO>> ReadCheeps(int page)
    {
        var offset = 32;
        offset = offset * page;
            var Cheeps = await _dbContext.Cheeps.AsNoTracking()
            .Select(m => new CheepDTO
            {
                Text = m.Text,
                User = m.User,  
                TimeStamp = m.TimeStamp
            }).Skip(offset).Take(32).ToListAsync();
        return Cheeps;
    }
    
    public async Task<User?> findAuthorByName(string name)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == name);
    }
    
        public async Task<User?> findAuthorByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

    public void createNewAuthor(string name, string email)
    {
        var user = new User
        {
            Name = name,
            Email = email,
            Cheeps = new List<Cheep>()
        };
        _dbContext.Users.Add(user);
    }
    

}