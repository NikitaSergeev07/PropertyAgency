<<<<<<< HEAD
﻿using PropertyAgency.Domain.Entities;
=======
using PropertyAgency.Domain.Entities;
>>>>>>> Nikita

namespace PropertyAgency.DAL.Interfaces;

public interface IImagesRepository : IBaseRepository<Image>
{
<<<<<<< HEAD
    Task<Image> GetByProperty(Guid Property_id);
=======
    Task<List<Image>> GetByProperty(Guid Property_id);
>>>>>>> Nikita
}