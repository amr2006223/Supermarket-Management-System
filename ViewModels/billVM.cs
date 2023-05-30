using Supermarket_Managment_System.Models;

namespace Supermarket_Managment_System.ViewModels
{
    public class billVM
    {
        Guid id { get; set; }
        List<products> products { get; set; }
        float totalPrice { get; set; }
    }
}
