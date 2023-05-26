
namespace FlashDeals.Data.Entities
{
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using NuGet.Packaging.Signing;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public Double? ActualPrice { get; set; }
        public Double? LightningRate { get; set; }
        [Required]
        public Double? FinalPrice { get; set; }
        public int? TotalUnits { get; set; }
        public int? AvailableUnits { get; set; }
        public DateTime? ExpireTime { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get;set; }
    }
}
