namespace WebApiMongoDb.Config
{
    public class ProdutoDataBaseSettings
    {
        public string ConnectionString { get; set; } = null;
        public string DataBaseName { get; set; } = null;
        public string ProdutoCollectionName { get; set; } = null;
    }
}