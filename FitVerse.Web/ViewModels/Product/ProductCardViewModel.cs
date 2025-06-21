using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitVerse.Web.ViewModels.Product
{
    public class ProductCardViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required, StringLength(255)]
        public string ImageUrl { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }

        public decimal? DiscountPercentage { get; set; }

        [NotMapped]
        public bool IsOnSale => DiscountPercentage.HasValue && DiscountPercentage > 0;

        [NotMapped]
        public decimal EffectivePrice
        {
            get
            {
                if (DiscountPercentage.HasValue && DiscountPercentage > 0)
                {
                    return Price * (1 - (DiscountPercentage.Value / 100M));
                }
                return Price;
            }
        }
    }
}
