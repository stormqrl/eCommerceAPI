using eCommerceAPI.Entities;
using eCommerceAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProdutos(string tipoProduto, int? categoriaId = null)
        {
            IEnumerable<Produto> produtos;

            if (tipoProduto == "categoria" && categoriaId != null)
            {
                produtos = await _produtoRepository.ObterProdutosPorCategoriaAsync(categoriaId.Value);
            }
            else if (tipoProduto == "popular")
            {
                produtos = await _produtoRepository.ObterProdutosPopularesAsync();
            }
            else if (tipoProduto == "maisvendido")
            {
                produtos = await _produtoRepository.ObterProdutosMaisVendidosAsync();
            }
            else
            {
                return BadRequest("Tipo de produto inválido");
            }

            var dadosProduto = produtos.Select(v => new
            {
                v.Id,
                v.Nome,
                v.Preco,
                v.UrlImagem
            });

            return Ok(dadosProduto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetalheProduto(int id)
        {
            var produto = await _produtoRepository.ObterDetalheProdutoAsync(id);

            if (produto is null)
            {
                return NotFound($"Produto com id={id} não encontrado");
            }

            var dadosProduto = new
            {
                produto.Id,
                produto.Nome,
                produto.Preco,
                produto.Detalhe,
                produto.UrlImagem
            };

            return Ok(dadosProduto);
        }
    }
}
