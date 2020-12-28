using System.ComponentModel.DataAnnotations;

namespace BookMyEvent.Services.ShoppingCart.DTOs
{
    public class BasketLineForUpdate
    {
        [Required]
        public int TicketAmount { get; set; }
    }
}
