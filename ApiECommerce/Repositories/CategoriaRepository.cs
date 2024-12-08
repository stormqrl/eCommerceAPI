using eCommerceAPI.Context;
using eCommerceAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerceAPI.Repositories
{
    public class CategoriaRepository(AppDbContext dbContext) : ICategoriaRepository
    {
        private readonly AppDbContext dbContext = dbContext;

        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            return await dbContext.Categorias.ToListAsync();
        }
    }
}
