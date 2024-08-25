using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IdentityApi.Repository;
using IdentityApi.Entities;

namespace IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly InterfaceProduto _InterfaceProduto;

        public ProdutosController(InterfaceProduto InterfaceProduto)
        {
            _InterfaceProduto = InterfaceProduto;
        }

        [HttpGet("/api/ListarTodosProdutos")]
        [Produces("application/json")]
        public async Task<object> List()
        {
            return await _InterfaceProduto.List();
        }

        [HttpPost("/api/AdicionarProdutos")]
        [Produces("application/json")]
        public async Task<object> Add(ProdutoModel produto)
        {
            try
            {
                await _InterfaceProduto.Add(produto);
            }
            catch (Exception ERRO)
            {
                
            }

            return Task.FromResult("OK");
        }

        [HttpPut("/api/AtualizarProdutos")]
        [Produces("application/json")]
        public async Task<object> Update(ProdutoModel produto)
        {
            try
            {
                await _InterfaceProduto.Update(produto);
            }
            catch (Exception ERRO)
            {

            }

            return Task.FromResult("OK");
        }


        [HttpGet("/api/ListarProdutoID")]
        [Produces("application/json")]
        public async Task<object> GetEntityById(int Id)
        {
            return await _InterfaceProduto.GetEntityById(Id);
        }

        [HttpDelete("/api/RemoverProdutos")]
        [Produces("application/json")]
        public async Task<object> Remove(int Id)
        {
            try
            {
                var produto = await _InterfaceProduto.GetEntityById(Id);

                await _InterfaceProduto.Delete(produto);
            }
            catch (Exception ERRO)
            {
                return false;
            }

            return true;
        }
    }
}
