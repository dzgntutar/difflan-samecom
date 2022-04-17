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


        public CategoryService(IDbSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);

            var db = client.GetDatabase(dbSettings.DbName);

            _categoryCollection = db.GetCollection<Category>(dbSettings.CategoryCollectionName);
        }


        public async Task<Response<List<CategoryDto>>> GetAll()
        {
            var categories = await _categoryCollection.Find(f => true).ToListAsync();

            //todo: auto mapping
            return Response<List<CategoryDto>>.Success(categories.Select(x => new CategoryDto { Id = x.Id, Name = x.Name }).ToList(), 200);
        }

        public async Task<Response<CategoryDto>> GetById(string id)
        {
            var category = await _categoryCollection.Find(f => f.Id == id).SingleOrDefaultAsync();

            if (category == null)
                return Response<CategoryDto>.Fail("Category not found", 404);

            //todo: auto mapping
            return Response<CategoryDto>.Success(new CategoryDto { Id = category.Id, Name = category.Name }, 200);
        }

        public async Task<Response<CategoryDto>> Create(CategoryDto category)
        {
            //todo: auto mapping
            var newCategory = new Category { Name = category.Name };

            await _categoryCollection.InsertOneAsync(newCategory);

            //todo: auto mapping
            return Response<CategoryDto>.Success(new CategoryDto { Id = newCategory.Id, Name = newCategory.Name }, 201);
        }
    }
}
