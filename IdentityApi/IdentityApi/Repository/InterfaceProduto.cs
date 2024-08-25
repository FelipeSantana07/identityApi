using IdentityApi.Entities;

namespace IdentityApi.Repository
{
    public interface InterfaceProduto
    {
        Task Add(ProdutoModel Objeto);
        Task Update(ProdutoModel Objeto);
        Task Delete(ProdutoModel Objeto);
        Task<ProdutoModel> GetEntityById(int id);
        Task<List<ProdutoModel>> List();

    }
}
