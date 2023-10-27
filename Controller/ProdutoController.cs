using dotnet_test.Model;
using dotnet_test.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_test.Controller
{

    [Route("~/produtos")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _ProdutoService;
        private readonly IValidator<Produto> _ProdutoValidator;

        public ProdutoController(IProdutoService ProdutoService, IValidator<Produto> ProdutoValidator)
        {
            _ProdutoService = ProdutoService;
            _ProdutoValidator = ProdutoValidator;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _ProdutoService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var Resposta = await _ProdutoService.GetById(id);

            if (Resposta is null)
            {
                return NotFound();
            }
            return Ok(Resposta);

        }

        [HttpGet("nome/{nome}")]
        public async Task<ActionResult> GetByNome(string nome)
        {
            return Ok(await _ProdutoService.GetByNome(nome));
        }

        [HttpGet("preco/{preco}")]
        public async Task<ActionResult> GetByPreco(decimal preco)
        {
            return Ok(await _ProdutoService.GetByPreco(preco));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Produto produto)
        {
            var validarProduto = await _ProdutoValidator.ValidateAsync(produto);

            if (!validarProduto.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarProduto);

            var Resposta = await _ProdutoService.Create(produto);

            if (Resposta is null)
                return BadRequest("Produto não cadastrado!");

            return CreatedAtAction(nameof(GetById), new { Id = produto.id }, produto);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Produto produto)
        {
            if (produto.id == 0)
                return BadRequest("Id do Game inválido");

            var validarProduto = await _ProdutoValidator.ValidateAsync(produto);

            if (!validarProduto.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarProduto);
            }

            var resposta = await _ProdutoService.Update(produto);

            if (resposta is null)
                return NotFound("Produto não encontrados!");

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var BuscaProduto = await _ProdutoService.GetById(id);

            if (BuscaProduto is null)
                return NotFound("Produto não encontrado!");

            await _ProdutoService.Delete(BuscaProduto);

            return NoContent();
        }

    }
}