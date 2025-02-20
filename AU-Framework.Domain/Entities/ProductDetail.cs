using AU_Framework.Domain.Abstract;

namespace AU_Framework.Domain.Entities;

public sealed class ProductDetail : BaseEntity
{
    public Guid ProductId { get; set; }
    public string? Color { get; set; }
    public string? Size { get; set; }
    public string? Material { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? Warranty { get; set; }
    public string? Specifications { get; set; }  // JSON formatında teknik özellikler
    public string? AdditionalInformation { get; set; }
    public decimal? Weight { get; set; }
    public string? WeightUnit { get; set; }
    public string? Dimensions { get; set; }  // Format: "UxGxY" (cm)
    public int? StockCode { get; set; }
    public string? Barcode { get; set; }

    // Navigation property
    public Product Product { get; set; }
} 