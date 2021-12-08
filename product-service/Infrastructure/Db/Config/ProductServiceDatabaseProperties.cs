namespace product_service.Infrastructure.Db.Config
{
    public class ProductServiceDatabaseProperties
    {
        public string ConnectionString { get; set; }
        
        public string DatabaseName { get; set; }
        
        public string ProductsCollectionName { get; set; }
    }
}