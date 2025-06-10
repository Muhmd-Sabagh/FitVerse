using System.Runtime.InteropServices;

namespace E_commerce_Khotwa.Models
{
    public class Cart_ViewModel
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProdId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
