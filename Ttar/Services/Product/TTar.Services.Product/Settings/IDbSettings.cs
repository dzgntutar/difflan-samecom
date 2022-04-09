namespace TTar.Services.Product.Settings
{
    public interface IDbSettings
    {
        public string CategoryCollectionName { get; set; }
        public string ProductCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DbName { get; set; }
    }
}