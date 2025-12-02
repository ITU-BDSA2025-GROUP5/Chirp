using Chirp.Domain;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure;

public class CheepRepository : ICheepRepository
{
    private readonly CheepDbContext _dbContext;

    public CheepRepository(CheepDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> GetCheepCount()
    {
        return await _dbContext.Cheeps.AsNoTracking().CountAsync();
    }

    public async Task<List<CheepDTO>> ReadCheeps(int page)
    {
        const int pageSize = 32;
        var skip = Math.Max(0, (page - 1) * pageSize);

        var cheeps = await _dbContext.Cheeps
        .AsNoTracking()
        .OrderByDescending(c => c.TimeStamp)
        .Include(c => c.User)
        .Skip(skip)
        .Take(pageSize)
        .Select(c => new CheepDTO
        {
            CheepId = c.CheepId,
            Text = c.Text,
            User = c.User,
            Likes = c.Likes,
            TimeStamp = c.TimeStamp
        })
        .ToListAsync();
        return cheeps;
    }

    public async Task InsertNewCheepAsync(CheepDTO message)
    {
        var newCheep = new Cheep
        {
            Text = message.Text,
            UserId = message.User.Id,
            User = message.User,
            TimeStamp = message.TimeStamp,
        };
        _dbContext.Cheeps.Add(newCheep);
        await _dbContext.SaveChangesAsync();
    }


    public async Task<List<CheepDTO>> getCheepsFromUser(User user, int page)
    {
        const int pageSize = 32;
        var skip = Math.Max(0, (page - 1) * pageSize);

        var cheeps = await _dbContext.Cheeps
            .AsNoTracking()
            .OrderByDescending(c => c.TimeStamp)
            .Include(c => c.User)
            .Skip(skip)
            .Take(pageSize)
            .Where(c => c.User == user)
            .Select(c => new CheepDTO
            {
                CheepId = c.CheepId,
                Text = c.Text,
                User = c.User,
                Likes = c.Likes,
                TimeStamp = c.TimeStamp
            })
            .ToListAsync();
        return cheeps;
    }

    public async Task<List<CheepDTO>?> getCheepsFromUserId(string userId)
    {
        var cheeps = await _dbContext.Cheeps
            .AsNoTracking() // Ensures the entities are not tracked by EF Core
            .OrderByDescending(c => c.TimeStamp) // Orders cheeps by timestamp (most recent first)
            .Include(c => c.User) // Eagerly loads the User navigation property
            .Where(c => c.UserId == userId) // Filters cheeps by UserId
            .Select(c => new CheepDTO
            {
                CheepId = c.CheepId,
                Text = c.Text,
                User = c.User,
                Likes = c.Likes,
                TimeStamp = c.TimeStamp
            })
            .ToListAsync(); // Executes the query and returns the result as a list

        return cheeps;
    }

    public async Task<string> LikeCheep(User currentUser, int cheepId)
    {
        List<string> Likes = new List<string>();
        Likes.Add(currentUser.Id);

        var cheep = await _dbContext.Cheeps.FindAsync(cheepId);
        if (cheep == null)
        {
            return "Cheep not found";
        }
        if (cheep.Likes == null)
        {
            cheep.Likes = Likes;
        }
        else if (!cheep.Likes.Contains(currentUser.Id))
        {
            cheep.Likes.Add(currentUser.Id);
        }
        else
        {
            Console.WriteLine("error time");
        }
        await _dbContext.SaveChangesAsync();

        return "Success";
    }
    public async Task<string> UnLikeCheep(User currentUser, int cheepId)
    {
        var cheep = await _dbContext.Cheeps.FindAsync(cheepId);
        if (cheep == null)
        {
            return "Cheep not found";
        }
        if (cheep.Likes != null && cheep.Likes.Contains(currentUser.Id))
        {
            cheep.Likes.Remove(currentUser.Id);
            await _dbContext.SaveChangesAsync();
            return "Success";
        }
        return "User has not liked this cheep";
    }
}