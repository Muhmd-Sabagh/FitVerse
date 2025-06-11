using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitVerse.Web.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 10)]
        public int Quantity { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedAt { get; set; } = new DateTime(2025, 01, 01);

        [Column(TypeName = "date")]
        public DateTime UpdatedAt { get; set; } = new DateTime(2025, 01, 01);

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        // Calculated Properties
        public decimal TotalPrice => Product?.EffectivePrice * Quantity ?? 0;
    }
}
