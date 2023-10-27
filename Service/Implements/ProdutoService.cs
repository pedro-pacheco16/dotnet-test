using dotnet_test.Data;
using dotnet_test.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace dotnet_test.Service.Implements
{
    public class ProdutoService : IProdutoService
    {

        private readonly AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produtos
               .ToListAsync();
        }

        public async Task<Produto?> GetById(long id)
        {
            try
            {
                var Resposta = await _context.Produtos
                    .Include(p => p.Categoria)
                    .FirstAsync(i => i.id == id);

                return Resposta;
            }
            catch
            {
                return null;
            };
        }
        public async Task<IEnumerable<Produto>> GetByNome(string nome)
        {
            var Produto = await _context.Produtos
              .Include(p => p.Categoria)
              .Where(p => p.Nome.Contains(nome))
              .ToListAsync();

            return Produto;
        }

        public async Task<IEnumerable<Produto>> GetByPreco(decimal preco)
        {
            var produto = await _context.Produtos
            .Include(p => p.Categoria)
            .Where(p => p.Preco == preco)
            .ToListAsync();

            return produto;
        }

        public async Task<Produto?> Create(Produto produto)
        {
            if (produto.Categoria is not null)
            {
                var BuscarCategoria = await _context.Categorias.FindAsync(produto.Categoria.id);

                if (BuscarCategoria is null)
                    return null;

                produto.Categoria = BuscarCategoria;
            }

            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<Produto?> Update(Produto produto)
        {
            var ProdutoUpdate = await _context.Produtos.FindAsync(produto.id);

            if (ProdutoUpdate is null)
                return null;

            if (produto.Categoria is not null)
            {
                var buscaCategoria = await _context.Categorias.FindAsync(produto.Categoria.id);

                if (buscaCategoria == null)
                    return null;
            }

            _context.Entry(ProdutoUpdate).State = EntityState.Detached;

            _context.Entry(produto).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task Delete(Produto produto)
        {
            _context.Remove(produto);
            await _context.SaveChangesAsync();
        }
    }
}