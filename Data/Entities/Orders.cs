namespace FlashDeals.Data.Entities
{
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using NuGet.Packaging.Signing;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        [Required]
        public int? CustomerId { get; set; }
        [Required]
        public int? ProductId { get; set; }
        [Required]
        public int? RequiredUnits { get; set; }
        public string? State { get; set; } = "Pending";
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
