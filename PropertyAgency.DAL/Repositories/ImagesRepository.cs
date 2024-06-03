using Microsoft.EntityFrameworkCore;
using PropertyAgency.DAL.Interfaces;
using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Repositories;

public class ImagesRepository : IImagesRepository
{
    private readonly ApplicationDbContext _context;

    public ImagesRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Image> Create(Image entity)
    {

        await _context.Images.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Image> GetById(Guid id)
    {
        return await _context.Images.AsNoTracking()
            .Include(e => e.Property)
            .Include(e => e.ImageUrl)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Image>> Get()
    {
        return await _context.Images.AsNoTracking().
            Include(e => e.Property).ToListAsync();
    }

    public async Task<bool> Delete(Guid id)
    {
        await _context.Images.Where(p => p.Id == id)
            .Include(e => e.Property)
            .Include(e => e.ImageUrl).ExecuteDeleteAsync();
        return true;
    }

    public async Task<bool> Update(Image entity)
    {
        await _context.Images
            .Where(p => p.Id == entity.Id)
            .Include(e => e.Property)
            .Include(e => e.ImageUrl)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.PropertyId, p => entity.PropertyId)
                .SetProperty(p => p.ImageUrl, p => entity.ImageUrl));
        return true;
    }
    public async Task<Image> GetByProperty(Guid PropertyId)
    {
        return await _context.Images
            .AsNoTracking()
            .Include(e => e.Property)
            .Include(e => e.ImageUrl)
            .FirstOrDefaultAsync(e => e.PropertyId == PropertyId);
    }
}