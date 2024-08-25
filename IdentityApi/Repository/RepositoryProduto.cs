using IdentityApi.Config;
using IdentityApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Repository
{
    public class RepositoryProduto : InterfaceProduto
    {

        private readonly DbContextOptions<ContextBase> _OptionsBuilders;

        public RepositoryProduto()
        {
            _OptionsBuilders = new DbContextOptions<ContextBase>(); 
        }

        public async Task Add(ProdutoModel Objeto)
        {
            using (var data = new ContextBase(_OptionsBuilders))
            {
                await data.Set<ProdutoModel>().AddAsync(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(ProdutoModel Objeto)
        {
            using (var data = new ContextBase(_OptionsBuilders))
            {
                data.Set<ProdutoModel>().Remove(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task<ProdutoModel> GetEntityById(int Id)
        {
            using (var data = new ContextBase(_OptionsBuilders))
            {
                return await data.Set<ProdutoModel>().FindAsync(Id);
            }
        }

        public async Task<List<ProdutoModel>> List()
        {
            using (var data = new ContextBase(_OptionsBuilders))
            {
                return await data.Set<ProdutoModel>().ToListAsync();
            }
        }

        public async Task Update(ProdutoModel Objeto)
        {
            using (var data = new ContextBase(_OptionsBuilders))
            {
                data.Set<ProdutoModel>().Update(Objeto);
                await data.SaveChangesAsync();
            }
        }
    }
}
