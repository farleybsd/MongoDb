using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApiMongoDb.Config;
using WebApiMongoDb.Models;

namespace WebApiMongoDb.Service
{
    public class ProdutoService
    {
        private readonly IMongoCollection<Produto> _produtoCollection;

        public ProdutoService(IOptions<ProdutoDataBaseSettings> produtoService)
        {
            var mongoCliente = new MongoClient(produtoService.Value.ConnectionString);
            var mongoDataBase = mongoCliente.GetDatabase(produtoService.Value.DataBaseName);
            _produtoCollection = mongoDataBase.GetCollection<Produto>(produtoService.Value.ProdutoCollectionName);
        }

        public async Task<List<Produto>> GetAsync() =>
            await _produtoCollection.Find( x => true).ToListAsync();

        public async Task<Produto> GetAsync(string id) =>
            await _produtoCollection.Find( x => x.Id ==id).FirstOrDefaultAsync();

        public async Task CreateAsync(Produto produto) =>
            await _produtoCollection.InsertOneAsync(produto);   

        public async Task UpdateAsync(string id, Produto produto) =>
            await _produtoCollection.ReplaceOneAsync(x => x.Id==id,produto);

        public async Task RemoveAsync(string id) =>
            await _produtoCollection.DeleteOneAsync(x => x.Id == id);
    }
}
