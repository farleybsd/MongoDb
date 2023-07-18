using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMongoDb.Models;
using WebApiMongoDb.Service;

namespace WebApiMongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoService _produtoServices;

        public ProdutosController(ProdutoService produtoServices)
        {
            _produtoServices = produtoServices;
        }

        [HttpGet]
        public async Task<List<Produto>> GetProdutos()
            => await _produtoServices.GetAsync();

        [HttpPost]
        public async Task<Produto> PostProduto(Produto produto)
        {
            await _produtoServices.CreateAsync(produto);

            return produto;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Produto>> Get(string id)
        {
            var produto = await _produtoServices.GetAsync(id);

            if (produto is null)
            {
                return NotFound();
            }

            return produto;
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Produto produto)
        {
            var produtoentity = await _produtoServices.GetAsync(id);

            if (produtoentity is null)
            {
                return NotFound();
            }

            produto.Id = produtoentity.Id;

            await _produtoServices.UpdateAsync(id, produto);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var produto = await _produtoServices.GetAsync(id);

            if (produto is null)
            {
                return NotFound();
            }

            await _produtoServices.RemoveAsync(id);

            return NoContent();
        }
    }
}
