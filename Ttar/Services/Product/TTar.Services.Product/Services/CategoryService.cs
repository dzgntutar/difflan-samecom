using AutoMapper;
using MongoDB.Driver;
using Ttar.Common.Models;
using TTar.Services.Product.Models.Dto;
using TTar.Services.Product.Models.Entities;
using TTar.Services.Product.Settings;

namespace TTar.Services.Product.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;


        public CategoryService(IDbSettings dbSettings, IMapper mapper)
        {
            _mapper = mapper;

            var client = new MongoClient(dbSettings.ConnectionString);
            var db = client.GetDatabase(dbSettings.DbName);
            _categoryCollection = db.GetCollection<Category>(dbSettings.CategoryCollectionName);
        }


        public async Task<Response<List<CategoryDto>>> GetAll()
        {
            var categories = await _categoryCollection.Find(f => true).ToListAsync();

            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<Response<CategoryDto>> GetById(string id)
        {
            var category = await _categoryCollection.Find(f => f.Id == id).SingleOrDefaultAsync();

            if (category == null)
                return Response<CategoryDto>.Fail("Category not found", 404);

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }

        public async Task<Response<CategoryDto>> Create(CategoryDto category)
        {
            var newCategory = _mapper.Map<Category>(category);

            await _categoryCollection.InsertOneAsync(newCategory);

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(newCategory), 201);
        }


    }
}
