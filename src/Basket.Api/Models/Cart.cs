using Marten.Schema;

namespace Basket.Api.Models;

public class Cart
{
    //[Identity]
    public string UserName { get; set; }

    public List<CartItem> Items { get; set; } = default!;

    public decimal Total => Items!= null ? Items.Sum(x => x.Price*x.Quantity) : 0;

   public Cart()
   {
    
   }
    public Cart(string userName)
    {
        UserName = userName;
    }
}