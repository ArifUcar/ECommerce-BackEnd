using AU_Framework.Application.Features.CategoryFeatures.CreateCategory;
using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;
using AU_Framework.Persistance.Context;
using AutoMapper;

namespace AU_Framework.Persistance.Services
{
    // CategoryService, ICategoryService arayüzünü implement eder.
    // Kategori ile ilgili iş mantığını içerir ve veri katmanında kategori işlemlerini gerçekleştirir.
    public sealed class CategoryService : ICategoryService
    {
        // Uygulamanın veritabanına erişimi sağlayan DbContext
        private readonly AppDbContext _context;

        // AutoMapper kullanarak veri nesneleri arasında dönüşüm işlemi yapmamızı sağlayan IMapper
        private readonly IMapper _mapper;

        // Yapıcı metod, bağımlılık enjeksiyonu ile AppDbContext ve IMapper alınır.
        // Bu metod, veritabanı işlemleri ve veri dönüşümleri için gerekli bağımlılıkları sağlar.
        public CategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;  // AppDbContext nesnesi, veritabanına erişim sağlar.
            _mapper = mapper;    // IMapper nesnesi, veri dönüşümü için kullanılır.
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
            await _context.Set<Category>().AddAsync(category, cancellationToken);

            // Veritabanına yapılan değişiklikler kaydedilir.
            // SaveChangesAsync, tüm yapılan değişiklikleri veritabanına kalıcı olarak işler.
            // cancellationToken ile işlem iptal edilebilir.
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
