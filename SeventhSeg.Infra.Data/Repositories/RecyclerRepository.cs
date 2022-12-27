using Microsoft.EntityFrameworkCore;
using SeventhSeg.Domain.Entities;
using SeventhSeg.Domain.Interfaces;
using SeventhSeg.Infra.Data.Context;

namespace SeventhSeg.Infra.Data.Repositories;

public class RecyclerRepository : IRecyclerRepository
{
    private ApplicationDbContext _recyclerContext;
    public RecyclerRepository(ApplicationDbContext recyclerContext)
    {
        _recyclerContext = recyclerContext;
    }

    public async Task<Recycler> CreateAsync(Recycler recycler)
    {
        recycler.CreatedDate = DateTime.Now;

        _recyclerContext.Add(recycler);
        await _recyclerContext.SaveChangesAsync();
        return recycler;
    }

    public async Task<Recycler> GetByIdAsync(Guid id)
    {
        return await _recyclerContext.Recycler.SingleOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Recycler>> GetRecyclerAsync()
    {
        return await _recyclerContext.Recycler.ToListAsync();
    }

    public async Task<Recycler> RemoveAsync(Recycler recycler)
    {
        _recyclerContext.Remove(recycler);
        await _recyclerContext.SaveChangesAsync();
        return recycler;
    }

    public async Task<Recycler> UpdateAsync(Recycler recycler)
    {
        recycler.UpdatedDate = DateTime.Now;
        _recyclerContext.Update(recycler);
        await _recyclerContext.SaveChangesAsync();
        return recycler;
    }

}
