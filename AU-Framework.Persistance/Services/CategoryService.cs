using AU_Framework.Application.Features.CategoryFeatures.Command.CreateCategory;
using AU_Framework.Application.Features.CategoryFeatures.Queries;
using AU_Framework.Application.Repository;
using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AU_Framework.Persistance.Services
{
    // CategoryService, ICategoryService arayüzünü implement eder.
    // Kategori ile ilgili iş mantığını içerir ve veri katmanında kategori işlemlerini gerçekleştirir.
    public sealed class CategoryService : ICategoryService
    {
        // Uygulamanın veritabanına erişimi sağlayan DbContext
        private readonly IRepository<Category> _categoryRepository;

        // AutoMapper kullanarak veri nesneleri arasında dönüşüm işlemi yapmamızı sağlayan IMapper
        private readonly IMapper _mapper;

        // Yapıcı metod, bağımlılık enjeksiyonu ile AppDbContext ve IMapper alınır.
        // Bu metod, veritabanı işlemleri ve veri dönüşümleri için gerekli bağımlılıkları sağlar.
        public CategoryService(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // Kategori oluşturma işlemi için asenkron bir metod.
        // CreateCategoryCommand, gelen kategori verisini içerir.
        public async Task CreateAsync(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            // AutoMapper kullanarak gelen CreateCategoryCommand nesnesini Category entity'sine dönüştürüyoruz.
            // request, gelen komut (kategori verileri) ile oluşturulacak Category entity'si arasındaki dönüştürmeyi sağlar.
            Category category = _mapper.Map<Category>(request);

            // Yeni kategori, veritabanına eklenmek üzere ekleniyor.
            // _context.Set<Category>() ile veritabanındaki Category tablosuna erişilir.
            // AddAsync, yeni kategori nesnesini asenkron şekilde veritabanına ekler.
            await _categoryRepository.AddAsync(category, cancellationToken);
        }

        public async Task<IList<Category>> GetAllAsync(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            // IsDeleted=false olan aktif kategorileri getir
            var activeCategories = await _categoryRepository.FindAsync(x => !x.IsDeleted, cancellationToken);
            return activeCategories.ToList();
        }
    }
}
