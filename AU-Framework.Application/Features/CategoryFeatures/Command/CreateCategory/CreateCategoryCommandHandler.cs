using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.CategoryFeatures.Command.CreateCategory;

public sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, MessageResponse>
{
    // ICategoryService, kategori oluşturma işlemi için gerekli metodları içeren servis arayüzüdür.
    private readonly ICategoryService _categoryService;

    // Yapıcı metod (constructor), ICategoryService'yi bağımlılık enjeksiyonu ile alır.
    public CreateCategoryCommandHandler(ICategoryService categoryService)
    {
        // _categoryService, kategori işlemleri için servis olarak ayarlanır.
        _categoryService = categoryService;
    }

    // Handle metodu, gelen CreateCategoryCommand komutunu işler ve MessageResponse döner.
    public async Task<MessageResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        // Kategori oluşturma işlemi başlatılır.
        // CreateAsync metodu, ilgili kategori verilerini alır ve asenkron bir işlem başlatır.
        await _categoryService.CreateAsync(request, cancellationToken);

        // İşlem başarılı bir şekilde tamamlandıktan sonra geri dönüş mesajı döndürülür.
        return new("Kategori başarıyla eklendi");
    }
}
