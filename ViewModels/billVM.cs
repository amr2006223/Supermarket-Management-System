using Supermarket_Managment_System.Models;

namespace Supermarket_Managment_System.ViewModels
{
    public class billVM
    {
        public Guid BillId { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public float TotalPrice { get; set; }
    }

    public class ProductViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public bool HasOffer { get; set; }
        public float Discount { get; set; }
    }
}
