using AutoMapper;
using MongoDB.Driver;
using Ttar.Common.Models;
using TTar.Services.Product.Models.Dto;
using TTar.Services.Product.Models.Entities;
using TTar.Services.Product.Settings;

namespace TTar.Services.Product.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Models.Entities.Product> _productCollection;
        private readonly IMapper _mapper;

        public ProductService(IDbSettings dbSettings, IMapper mapper)
        {
            _mapper = mapper;

            var client = new MongoClient(dbSettings.ConnectionString);
            var db = client.GetDatabase(dbSettings.DbName);
            _productCollection = db.GetCollection<Models.Entities.Product>(dbSettings.ProductCollectionName);
        }


        public async Task<Response<List<ProductGetDto>>> GetAll()
        {
            var products = await _productCollection.Find(f => true).ToListAsync();

            return Response<List<ProductGetDto>>.Success(_mapper.Map<List<ProductGetDto>>(products), 200);
        }

        public async Task<Response<ProductGetDto>> GetById(string id)
        {
            var product = await _productCollection.Find(f => f.Id == id).SingleOrDefaultAsync();

            if (product == null)
                return Response<ProductGetDto>.Fail("Product not found", 404);

            return Response<ProductGetDto>.Success(_mapper.Map<ProductGetDto>(product), 200);
        }

        public async Task<Response<ProductCreateDto>> Create(ProductCreateDto product)
        {
            var newproduct = _mapper.Map<Models.Entities.Product>(product);
            newproduct.CreateDate = DateTime.Now;

            await _productCollection.InsertOneAsync(newproduct);

            return Response<ProductCreateDto>.Success(_mapper.Map<ProductCreateDto>(newproduct), 201);
        }
    }
}
