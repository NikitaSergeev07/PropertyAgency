<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
=======
using Microsoft.EntityFrameworkCore;
>>>>>>> Nikita
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
<<<<<<< HEAD
    public async Task<Image> Create(Image entity)
    {

=======

    public async Task<Image> Create(Image entity)
    {
>>>>>>> Nikita
        await _context.Images.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Image> GetById(Guid id)
    {
<<<<<<< HEAD
        return await _context.Images.AsNoTracking()
            .Include(e => e.Property)
            .Include(e => e.ImageUrl)
            .FirstOrDefaultAsync(p => p.Id == id);
=======
        return await _context.Images.AsNoTracking().Include(e => e.Property).FirstOrDefaultAsync(p => p.Id == id);

>>>>>>> Nikita
    }

    public async Task<List<Image>> Get()
    {
<<<<<<< HEAD
        return await _context.Images.AsNoTracking().
            Include(e => e.Property).ToListAsync();
=======
        return await _context.Images.AsNoTracking().Include(e => e.Property).ToListAsync();
>>>>>>> Nikita
    }

    public async Task<bool> Delete(Guid id)
    {
<<<<<<< HEAD
        await _context.Images.Where(p => p.Id == id)
            .Include(e => e.Property)
            .Include(e => e.ImageUrl).ExecuteDeleteAsync();
=======
        await _context.Images.Where(p => p.Id == id).Include(e => e.Property).ExecuteDeleteAsync();
>>>>>>> Nikita
        return true;
    }

    public async Task<bool> Update(Image entity)
    {
        await _context.Images
            .Where(p => p.Id == entity.Id)
            .Include(e => e.Property)
<<<<<<< HEAD
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
=======
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.ImageUrl, p => entity.ImageUrl)
                .SetProperty(p => p.PropertyId, p => entity.PropertyId));
        return true;
    }

    public async Task<List<Image>> GetByProperty(Guid Property_id)
    {
        return await _context.Images.Include(i => i.Property).Where(i => i.PropertyId == Property_id).ToListAsync();
    }
}
>>>>>>> Nikita
